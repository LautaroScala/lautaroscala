import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Person } from './person';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class LoginService implements OnInit {
  public token = '';

  constructor(private http : HttpClient){

  }

  register(persona : Person):void{
    this.http.post<Person>(`${environment.api}/account/register`, persona).subscribe(data => console.log(data));
  }

  login(user: User){
    return this.http.post(`${environment.api}/account/login`, user)
      .subscribe((data) => {
        localStorage.setItem('currentUser', JSON.stringify(data));
        console.log(localStorage.getItem('currentUser'));
      });
  }

  logout():boolean{
    localStorage.removeItem('currentUser');
    return true;
  }

  ngOnInit(): void {
  }

}
