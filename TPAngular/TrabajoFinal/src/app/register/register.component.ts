import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { LoginService } from '../login.service';
import { Person } from '../person';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registerForm = this.fb.group({
    Name: ['', [Validators.required, Validators.minLength(5)]],
    Phone: ['', [Validators.required]],
    Email: ['', [Validators.required, Validators.email]],
    Username: ['', Validators.required],
    Password: ['', Validators.required],
  });
  constructor(private fb: FormBuilder, private service: LoginService) {}
  getToken():boolean{
    if (localStorage.getItem('currentUser')){
      return true
    }
    return false;
  }
  onSubmit(): void {
    if (this.registerForm.valid) {
      let registro = this.registerForm.value;
      this.service.register(
        new Person(
          registro.Name!,
          registro.Phone!,
          registro.Email!,
          registro.Username!,
          registro.Password!
        )
      );
    }
  }
}
