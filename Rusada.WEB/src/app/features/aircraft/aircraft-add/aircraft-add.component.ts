
import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { AddAircraftSightRequest } from 'src/app/models/add-aircraft-sight-request.model';
import { AircraftService } from 'src/app/services/aircraft.service';

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
  styleUrls: ['./aircraft-add.component.css']
})
export class AircraftAddComponent {
  aircraftForm!: FormGroup;
  post: any = '';
  // url :any = '';
  titleAlert: string = 'This field is required';
	model!: NgbDateStruct;

  constructor(private formBuilder: FormBuilder, private aircraftService: AircraftService) { }

  ngOnInit() {
    this.createForm();
    this.setChangeValidate()
  }


  createForm() {
    this.aircraftForm = this.formBuilder.group({
      'make': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(128)]],
      'model': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(128)]],
      'location': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(255)]],
      'registeration': [null, [Validators.required, this.checkRegisteration]],
      'date': [null, [Validators.required]],
      'time': '',
      // 'image': '',
      // 'validate': ''
    });
  }

  // onSelectFile(event:any) {
  //   if (event.target.files && event.target.files[0]) {
  //     var reader = new FileReader();

  //     reader.readAsDataURL(event.target.files[0]); // read file as data url

  //     reader.onload = (event) => { // called once readAsDataURL is completed
  //       this.url = event.target?.result;
  //     }
  //   }
  // }

  setChangeValidate() {
    this.aircraftForm.get('validate')?.valueChanges.subscribe(
      (validate) => {
        if (validate == '1') {
          this.aircraftForm.get('name')?.setValidators([Validators.required, Validators.minLength(3)]);
          this.titleAlert = "You need to specify at least 3 characters";
        } else {
          this.aircraftForm.get('name')?.setValidators(Validators.required);
        }
        this.aircraftForm.get('name')?.updateValueAndValidity();
      }
    )
  }

  checkRegisteration(control:any) {
    let enteredRegisteration = control.value
    let registerationCheck = /^([a-zA-Z]{1,2}-[a-zA-Z]{1,5})$/;
    return (!registerationCheck.test(enteredRegisteration) && enteredRegisteration) ? { 'requirements': true } : null;
  }

  getErrorPassword() {
    return this.aircraftForm.get('password')?.hasError('required') ? 'Field is required (at least eight characters, one uppercase letter and one number)' :
      this.aircraftForm.get('password')?.hasError('requirements') ? 'Password needs to be at least eight characters, one uppercase letter and one number' : '';
  }

  getErrorRegisteration() {
    return this.aircraftForm.get('registeration')?.hasError('required') ? 'Field is required (at least eight characters, one uppercase letter and one number)' :
      this.aircraftForm.get('registeration')?.hasError('requirements') ? 'Invalid registeration format.Format: 1-5 characters for suffix after a 1-2 character prefix, separated by a hyphen' : '';
  }

  onSubmit(post:any) {
    console.log(post)
    debugger;
    var data: AddAircraftSightRequest = {
      make: post.make,
      model: post.model,
      registration: post.registeration,
      location: post.location,
      dateTime: new Date(post.date.year, post.date.month-1, post.date.day,  post.time.hour,  post.time.minute, 0),
    };
    console.log(data,"this is the data")
    this.aircraftService.addAircraft(data).subscribe({
      next: (res)=>{console.log("sucess")},
      error: (error) =>{console.log("error")}
    });
  }
}
