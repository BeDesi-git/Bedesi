import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class LocationService {
  private apiUrl = 'https://localhost:7275/api/Location';

  constructor(private http: HttpClient) {}

  getLocationSuggestions(startsWith: string): Observable<any> {
    if (startsWith.trim() != "") {
      let rid = 'AreaSearchRequest';
      let params = new HttpParams()
        .set('startswith', startsWith.trim())
        .set('rid', rid);
      return this.http.get(this.apiUrl, { params });
    }
    return EMPTY;
  }

  getAllAreas(): Observable<any> {
    let rid = 'AllAreaRequest';
    let params = new HttpParams()
      .set('rid', rid);
    return this.http.get(this.apiUrl + '/GetAllLocations', { params });
  }
}
