import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Loan } from '../loan';
import { LoansService } from '../loans.service';

@Component({
  selector: 'app-prestamos',
  templateUrl: './prestamos.component.html',
  styleUrls: ['./prestamos.component.css']
})
export class PrestamosComponent implements OnInit {
  public element_data : Loan[] = [];
  public displayedColumns = ['id','loandate','returndate','returned'];

  constructor( private loanService: LoansService, private http:HttpClient, private route:Router) { }
  fetch(){
    this.loanService.getMyLoans(JSON.parse(localStorage.getItem('currentUser')!).userId)
    .subscribe(data => {
      this.element_data = data
    })
  }
  getToken():boolean{
    if (localStorage.getItem('currentUser')){
      return true
    }
    return false;
  }
  updateTable(){
    this.fetch()
  }
  ngOnInit(): void {
    this.fetch()
  }

}
