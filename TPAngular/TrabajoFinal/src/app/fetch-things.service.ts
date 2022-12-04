import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICategories } from './icategories';
import { IThing } from './ithing';

@Injectable({
  providedIn: 'root',
})
export class FetchThingsService {
  headers = {headers: {Authorization: `Bearer ${JSON.parse(localStorage.getItem('currentUser')!).token}`}};
  constructor(private http: HttpClient) {}

  getAvailableThings(): Observable<IThing[]> {
    return this.http
      .get<IThing[]>(`${environment.api}/things`, this.headers);
  }

  getCategories():Observable<ICategories[]> {
    return this.http
      .get<ICategories[]>(`${environment.api}/category`, this.headers);
  }
}
