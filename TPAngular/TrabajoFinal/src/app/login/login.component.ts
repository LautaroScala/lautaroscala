import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, UrlSerializer } from '@angular/router';
import { LoginService } from '../login.service';
import { User } from '../user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm = this.fb.group({
    Username:['',Validators.required],
    Password:['',Validators.required]
  })
  constructor(private fb: FormBuilder, private service: LoginService, private router: Router) {}

  onSubmit(): void {
    let short = this.loginForm.value
    let user: User = {
      Username: short.Username!,
      Password: short.Password!
    }
    this.service.login(user);
    this.router.navigate(['/']);
  }
  
}
