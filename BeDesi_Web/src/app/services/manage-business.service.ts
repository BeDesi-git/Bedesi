import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Business } from '../model/business-model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ManageBusinessService {
  private apiUrl = environment.apiBaseUrl + '/ManageBusiness';

  private httpOptions = {
    headers: new HttpHeaders({
      'Custom-Header': 'CustomHeaderValue',
      'Content-Type': 'application/json'
    })
  };
  constructor(private http: HttpClient) { }

  addBusiness(business: Business): Observable<any> {
    var userToken = localStorage.getItem('token')
    const addBusinessRequest = {
      rid: 'AddBusinessRequest',
      token: userToken ? userToken : "",
      business: business
    };
    return this.http.post<any>(`${this.apiUrl}/AddBusiness`, addBusinessRequest, this.httpOptions);
  }

  updateBusiness(business: Business): Observable<any> {
    const updateBusinessRequest = {
      rid: 'UpdateBusinessRequest',
      token: localStorage.getItem('token'),
      business: business
    };
    return this.http.post<any>(`${this.apiUrl}/UpdateBusiness`, updateBusinessRequest, this.httpOptions);
  }

  getUserBusiness(): Observable<any> {
    let rid = 'GetUserBusinessRequest';
    let token = localStorage.getItem('token') || ''
    let params = new HttpParams()
      .set('rid', rid)
      .set('token', token);

    
    return this.http.get(this.apiUrl + '/GetUserBusiness', { params, ...this.httpOptions });
  }

  checkBusinessName(businessName: string): Observable<any> {
    const params = new HttpParams()
      .set('rid', 'CheckBusinessNameRequest')
      .set('businessName', businessName);    

    return this.http.get(`${this.apiUrl}/CheckBusinessName`, { params, ...this.httpOptions });
  }
}
