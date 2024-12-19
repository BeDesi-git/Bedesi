import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PostcodeService {
  private baseUrl = 'https://api.postcodes.io'; // Use Postcodes.io or another API

  constructor(private http: HttpClient) { }


  getNearbyPostcodes(postcode: string, radius: number): Observable<any> {
    let outcode = this.extractOutcode(postcode);
    const endpoint = `${this.baseUrl}/outcodes/${outcode}/nearest`;
    const params = {
      radius: (radius * 1000).toString(),
      limit: 100,
    }; 

    return this.http.get(endpoint, { params });
    
  }

  extractOutcode(postcode: string): string | null {
    const outcodeRegex = /^[A-Z]{1,2}[0-9][A-Z0-9]?/i; // Matches the outcode part of a UK postcode
    const match = postcode.match(outcodeRegex);
    return match ? match[0].toUpperCase() : null;
  }
}
