import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FetchThingsService } from '../fetch-things.service';
import { ICategories } from '../icategories';
import { IThing } from '../ithing';
import { LoanDto } from '../loan-dto';
import { LoansService } from '../loans.service';

@Component({
  selector: 'app-lista-prestamos',
  templateUrl: './lista-prestamos.component.html',
  styleUrls: ['./lista-prestamos.component.css'],
})
export class ListaPrestamosComponent implements OnInit {
  public getJsonValue: IThing[] = [];
  public categorias: ICategories[] = [];
  public element_data: IThing[] = [];
  public displayedColumns: string[] = [
    'id',
    'desc',
    'creationDate',
    'categoryId',
    'available',
  ];
  constructor(
    private fetchservice: FetchThingsService,
    private http: HttpClient,
    private loanService: LoansService,
    private route: Router
  ) {}
  fetch() {
    this.fetchservice.getAvailableThings().subscribe((data) => {
      this.getJsonValue = data;
    });
    this.fetchservice.getCategories().subscribe(data => {
      this.categorias = data;
    });
    
  }
  getToken():boolean{
    if (localStorage.getItem('currentUser')){
      return true
    }
    return false;
  }
  updateTable(){
    this.element_data = [];
      for (const thing of this.getJsonValue) {
        if (thing.available){
          this.element_data.push(thing)
      }
    }
  }
  requestLoan(id:number){
    let loandto : LoanDto = {
      ThingId: id,
      PersonId: JSON.parse(localStorage.getItem('currentUser')!).userId
    }
    this.loanService.requestNewLoan(loandto).subscribe((data) => console.log (data))
    this.fetch()
  }
  getCategory(id: number):string {
    for (const cat of this.categorias) {
      if (cat.id == id){
        return cat.desc;
      }
    }
    return '';
  }

  ngOnInit(): void {
    this.fetch();
  }
}
