import { Component, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { formatDate } from 'src/app/core/util/util';
import { AddAircraftSightRequest } from 'src/app/models/add-aircraft-sight-request.model';
import { AircraftService } from 'src/app/services/aircraft.service';
import { ToastService } from 'src/app/services/toast-service';

export interface AircraftSight {
  Id?: number;
  make: string;
  model: string;
  registeration: string;
  location: string;
  dateTime: Date;
}

@Component({
  selector: 'app-aircraft-add',
  templateUrl: './aircraft-add.component.html',
  styleUrls: ['./aircraft-add.component.css'],
})
export class AircraftAddComponent {
  aircraftForm!: FormGroup;
  post: any = '';

  titleAlert: string = 'This field is required';
  model!: NgbDateStruct;

  constructor(
    private formBuilder: FormBuilder,
    private aircraftService: AircraftService,
    private toastService: ToastService,
    private router: Router
  ) {}

  ngOnInit() {
    this.createForm();
    this.setChangeValidate();
  }

  createForm() {
    this.aircraftForm = this.formBuilder.group({
      make: [
        null,
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(128),
        ],
      ],
      model: [
        null,
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(128),
        ],
      ],
      location: [
        null,
        [
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(255),
        ],
      ],
      registeration: [null, [Validators.required, this.checkRegisteration]],
      date: [null, [Validators.required]],
      time: { hour: 13, minute: 0 },
    });
  }

  setChangeValidate() {
    this.aircraftForm.get('validate')?.valueChanges.subscribe((validate) => {
      if (validate == '1') {
        this.aircraftForm
          .get('name')
          ?.setValidators([Validators.required, Validators.minLength(3)]);
        this.titleAlert = 'You need to specify at least 3 characters';
      } else {
        this.aircraftForm.get('name')?.setValidators(Validators.required);
      }
      this.aircraftForm.get('name')?.updateValueAndValidity();
    });
  }

  checkRegisteration(control: any) {
    let enteredRegisteration = control.value;
    let registerationCheck = /^([a-zA-Z]{1,2}-[a-zA-Z]{1,5})$/;
    return !registerationCheck.test(enteredRegisteration) &&
      enteredRegisteration
      ? { requirements: true }
      : null;
  }

  getErrorPassword() {
    return this.aircraftForm.get('password')?.hasError('required')
      ? 'Field is required (at least eight characters, one uppercase letter and one number)'
      : this.aircraftForm.get('password')?.hasError('requirements')
      ? 'Password needs to be at least eight characters, one uppercase letter and one number'
      : '';
  }

  getErrorRegisteration() {
    return this.aircraftForm.get('registeration')?.hasError('required')
      ? 'Field is required (at least eight characters, one uppercase letter and one number)'
      : this.aircraftForm.get('registeration')?.hasError('requirements')
      ? 'Invalid registeration format.Format: 1-5 characters for suffix after a 1-2 character prefix, separated by a hyphen'
      : '';
  }

  onSubmit(post: any) {
    debugger;

    var d = new Date(
      post.date.year,
      post.date.month - 1,
      post.date.day,
      post.time.hour,
      post.time.minute,
      0
    );
    if (d.getTime() > Date.now()) {
      this.toastService.show(
        'Error : Date must be a valid datetime in the past',
        { classname: 'bg-danger text-light', delay: 2000 }
      );
      return;
    } else {
      var data: AddAircraftSightRequest = {
        make: post.make,
        model: post.model,
        registration: post.registeration,
        location: post.location,
        dateTime: formatDate(
          new Date(
            post.date.year,
            post.date.month - 1,
            post.date.day,
            post.time.hour,
            post.time.minute,
            0
          )
        ),
      };

      this.aircraftService.addAircraft(data).subscribe({
        next: (res) => {
          this.toastService.show('success', {
            classname: 'bg-success text-light',
            delay: 2000,
          });
          this.router.navigate(['aircrafts']);
        },
        error: (error) => {
          this.toastService.show('error', {
            classname: 'bg-danger text-light',
            delay: 2000,
          });
        },
      });
    }
  }
}
