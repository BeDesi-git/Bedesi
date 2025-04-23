import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root',
})
export class LocationService {
  private apiUrl = environment.apiBaseUrl + '/Location';
  private httpOptions = {
    headers: new HttpHeaders({
      'Custom-Header': 'CustomHeaderValue',
      'Content-Type': 'application/json'
    })
  };
  constructor(private http: HttpClient) {}

  getLocationSuggestions(startsWith: string): Observable<any> {
    if (startsWith.trim() != "") {
      let rid = 'AreaSearchRequest';
      let params = new HttpParams()
        .set('startswith', startsWith.trim())
        .set('rid', rid);
      return this.http.get(this.apiUrl, { params, ...this.httpOptions });
    }
    return EMPTY;
  }

  getAllAreas(): Observable<any> {
    let rid = 'AllAreaRequest';
    let params = new HttpParams()
      .set('rid', rid);
    return this.http.get(this.apiUrl + '/GetAllLocations', { params, ...this.httpOptions });
  }

  getAllOutcodes(): Observable<any> {
    let rid = 'AllOutcodesRequest';
    let params = new HttpParams()
      .set('rid', rid);
    return this.http.get(this.apiUrl + '/GetAllOutcodes', { params, ...this.httpOptions });
  }
}
