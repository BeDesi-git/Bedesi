import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

@Injectable()
export class BusinessService {

  private apiUrl = 'https://localhost:7275/api/Business';
  private closeAutocompleteSubject = new Subject<void>();
  closeAutocomplete$ = this.closeAutocompleteSubject.asObservable();
  constructor(private http: HttpClient) { }

  searchBusinesses(keywords: string, location: string): Observable<any> {
    keywords = 'food';
    location = 'HA1';
    let rid = 'BusinessSearchRequest';
    let params = new HttpParams()
                .set('keywords', keywords)
                .set('location', location)
                .set('rid', rid);
    return this.http.get(this.apiUrl, { params });
  }

  getBusinessDetails(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  rateBusiness(id: number, rating: number, feedback: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/rate`, { rating, feedback });
  }

  closeAutocomplete() {
    this.closeAutocompleteSubject.next();
  }
}
