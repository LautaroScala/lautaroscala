import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Loan } from './loan';
import { LoanDto } from './loan-dto';

@Injectable({
  providedIn: 'root'
})
export class LoansService {
  headers = {headers: {Authorization: `Bearer ${JSON.parse(localStorage.getItem('currentUser')!).token}`}};

  constructor(private http:HttpClient) { }

  getMyLoans(personid:number): Observable<Loan[]>{
    return this.http.get<Loan[]>(`${environment.api}/loan/person/${personid}`, this.headers);
  }

  requestNewLoan(body:LoanDto):Observable<Loan[]>{
    return this.http.post<Loan[]>(`${environment.api}/loan`,body,this.headers)
  }
}
