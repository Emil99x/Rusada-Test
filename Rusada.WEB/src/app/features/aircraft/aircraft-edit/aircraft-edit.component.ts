import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AddAircraftSightRequest, UpdateAircraftSightRequest } from 'src/app/models/add-aircraft-sight-request.model';
import { AircraftService } from 'src/app/services/aircraft.service';
import { ToastService } from 'src/app/services/toast-service';

@Component({
  selector: 'app-aircraft-edit',
  templateUrl: './aircraft-edit.component.html',
  styleUrls: ['./aircraft-edit.component.css'],
})
export class AircraftEditComponent implements OnInit {

  airCraftId!: string;
  aircraftForm!: FormGroup;
  url :any = '';
  imageFile!: File | null;

  constructor(
     private activatedRoute: ActivatedRoute,
     private aircraftService: AircraftService,
     private formBuilder: FormBuilder,
     private toastService : ToastService,
     private router: Router
  ) {
    this.createForm();
  }
  createForm() {
    this.aircraftForm = this.formBuilder.group({
      'make': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(128)]],
      'model': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(128)]],
      'location': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(255)]],
      'registration': [null, [Validators.required, this.checkRegisteration]],
      'date': [null, [Validators.required]],
      'time': '',
      'image': '',

    });
  }

  ngOnInit(): void {
    this.airCraftId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.aircraftService.getAircraftById( this.airCraftId).subscribe({
      next: (res) => {
        let date =new Date(res.dateTime);
        this.aircraftForm.setValue({
          make :res.make,
          model :res.model,
          registration :res.registration,
           location :res.location,
           date :{year :date.getFullYear() , month:date.getMonth()+1 , day : date.getDay()+1},
           time:{hour : date.getHours() , minute: date.getMinutes() , second :0},
           image :''
        });
        this.url = res.imagePath;
        console.log(res);
      },
    });
  }
  removeImg(){
    this.url = '';
    this.imageFile = null;
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

  onSelectFile(event:any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url
      this.imageFile=event.target.files[0];

      reader.onload = (event) => { // called once readAsDataURL is completed
        this.url = event.target?.result;
      }
    }
  }

  onSubmit(post:any) {

    var data: UpdateAircraftSightRequest = {
      make: post.make,
      model: post.model,
      registration: post.registration,
      location: post.location,
      id:+this.airCraftId ,
      dateTime: new Date(post.date.year, post.date.month-1, post.date.day,  post.time.hour,  post.time.minute, 0),
    };

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
    } 
    
    this.aircraftService.updateAircraft(data,this.imageFile).subscribe({
      next: (res)=>{ 
        this.toastService.show('success', { classname: 'bg-success text-light', delay: 2000 });
        this.router.navigate(['aircrafts']);
      },
      error: (error) =>{ this.toastService.show('error', { classname: 'bg-danger text-light', delay: 2000 });}
    });
  }
}
