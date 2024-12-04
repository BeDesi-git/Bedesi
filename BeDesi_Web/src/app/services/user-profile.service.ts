import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { UpdateProfileRequest } from '../model/update-profile-request';

@Injectable({
  providedIn: 'root',
})
export class UserProfileService {
  private apiUrl = environment.apiBaseUrl + '/Profile';

  constructor(private http: HttpClient) { }


  getUserProfile(token: string): Observable<any> 
  {
    let rid = 'GetUserProfileRequest';
    let params = new HttpParams()
      .set('rid', rid)
      .set('token', token);
    
    return this.http.get(this.apiUrl + '/GetUserProfile', { params });
  }

  updateUserProfile(updatedProfile: UpdateProfileRequest) {
    updatedProfile.rid = 'UpdateProfile';
    return this.http.put(this.apiUrl + '/UpdateProfile', updatedProfile);
  }
}
