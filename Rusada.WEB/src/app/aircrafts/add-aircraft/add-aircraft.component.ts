import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable }  from 'rxjs';

@Component({
  selector: 'app-add-aircraft',
  templateUrl: './add-aircraft.component.html',
  styleUrls: ['./add-aircraft.component.css']
})
export class AddAircraftComponent {
  
  formGroup!: FormGroup;
  titleAlert: string = 'This field is required';
  post: any = '';

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.createForm();
    this.setChangeValidate()
  }

  createForm() {
    this.formGroup = this.formBuilder.group({
      'make': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(128)]],
      'model': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(128)]],
      'location': [null, [Validators.required, Validators.minLength(1), Validators.maxLength(255)]],
      'registeration': [null, [Validators.required, this.checkRegisteration]],
      'validate': ''
    });
  }

  setChangeValidate() {
    this.formGroup.get('validate')?.valueChanges.subscribe(
      (validate) => {
        if (validate == '1') {
          this.formGroup.get('name')?.setValidators([Validators.required, Validators.minLength(3)]);
          this.titleAlert = "You need to specify at least 3 characters";
        } else {
          this.formGroup.get('name')?.setValidators(Validators.required);
        }
        this.formGroup.get('name')?.updateValueAndValidity();
      }
    )
  }

  checkRegisteration(control:any) {
    let enteredRegisteration = control.value
    let registerationCheck = /^([a-zA-Z]{1,2}-[a-zA-Z]{1,5})$/;
    return (!registerationCheck.test(enteredRegisteration) && enteredRegisteration) ? { 'requirements': true } : null;
  }

  getErrorPassword() {
    return this.formGroup.get('password')?.hasError('required') ? 'Field is required (at least eight characters, one uppercase letter and one number)' :
      this.formGroup.get('password')?.hasError('requirements') ? 'Password needs to be at least eight characters, one uppercase letter and one number' : '';
  }

  getErrorRegisteration() {
    return this.formGroup.get('registeration')?.hasError('required') ? 'Field is required (at least eight characters, one uppercase letter and one number)' :
      this.formGroup.get('registeration')?.hasError('requirements') ? 'Invalid registeration format.Format: 1-5 characters for suffix after a 1-2 character prefix, separated by a hyphen' : '';
  }

  onSubmit(post:any) {
    this.post = post;
  }
}
