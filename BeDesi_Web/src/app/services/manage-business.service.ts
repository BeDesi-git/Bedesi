import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Business } from '../model/business-model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ManageBusinessService {
  private apiUrl = environment.apiBaseUrl + '/ManageBusiness';

  constructor(private http: HttpClient) { }

  addBusiness(business: Business): Observable<any> {
    const addBusinessRequest = {
      rid: 'AddBusinessRequest',
      token: localStorage.getItem('token'),
      business: business
    };
    return this.http.post<any>(`${this.apiUrl}/AddBusiness`, addBusinessRequest);
  }

  updateBusiness(business: Business): Observable<any> {
    const updateBusinessRequest = {
      rid: 'UpdateBusinessRequest',
      token: localStorage.getItem('token'),
      business: business
    };
    return this.http.post<any>(`${this.apiUrl}/UpdateBusiness`, updateBusinessRequest);
  }

  getUserBusiness(): Observable<any> {
    let rid = 'GetUserBusinessRequest';
    let token = localStorage.getItem('token') || ''
    let params = new HttpParams()
      .set('rid', rid)
      .set('token', token);

    return this.http.get(this.apiUrl + '/GetUserBusiness', { params });
  }
}
