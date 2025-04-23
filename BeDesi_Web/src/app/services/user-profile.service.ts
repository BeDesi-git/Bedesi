import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserProfileService {
  private apiUrl = environment.apiBaseUrl + '/Profile';

  private httpOptions = {
    headers: new HttpHeaders({
      'Custom-Header': 'CustomHeaderValue',
      'Content-Type': 'application/json'
    })
  };
  constructor(private http: HttpClient) { }


  getUserProfile(): Observable<any> 
  {
    let rid = 'GetUserProfileRequest';
    let token = localStorage.getItem('token') || ''
    let params = new HttpParams()
      .set('rid', rid)
      .set('token', token);
    
    return this.http.get(this.apiUrl + '/GetUserProfile', { params, ...this.httpOptions });
  }

  updateUserProfile(updatedProfile: any) {
    updatedProfile.rid = 'UpdateProfile';
    return this.http.put(this.apiUrl + '/UpdateProfile', updatedProfile, this.httpOptions);
  }
}
