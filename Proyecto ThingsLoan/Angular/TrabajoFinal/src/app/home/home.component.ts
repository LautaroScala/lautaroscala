import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor (private http: HttpClient, private service: LoginService, private router: Router) {
  
  }
  getToken():boolean{
    if (localStorage.getItem('currentUser')){
      return true
    }
    return false;
  }
  async logout() {
    await this.service.logout();
    this.router.navigate(['/']);
  }
}
