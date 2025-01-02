import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiBaseUrl + '/Auth';
  private loggedIn = new BehaviorSubject<boolean>(false);
  loggedInStatus$ = this.loggedIn.asObservable();

  constructor(private http: HttpClient) { }

   setLoggedIn(status: boolean): void {
    this.loggedIn.next(status);
  }


  register(data: any): Observable<any> {
    data.rid = 'RegisterRequest';
    return this.http.post<any>(`${this.apiUrl}/register`, data);
  }

  login(data: any): Observable<any> {
    data.rid = 'LoginRequest';
    return this.http.post<any>(`${this.apiUrl}/login`, data).pipe(
      tap((res) => {
        localStorage.setItem('token', res.result.token);
        localStorage.setItem('isBusinessOwner', res.result.userDetails.role == 'BusinessOwner'? 'Y' : 'N')
        this.setLoggedIn(true); 
      })
    );
  }

  forgotPassword(data: any): Observable<any> {
    data.rid = 'ForgotPasswordRequest';
    return this.http.post(`${this.apiUrl}/forgot-password`, data);
  }

  resetPassword(data: any): Observable<any> {
    data.rid = 'ResetPasswordRequest';
    return this.http.post(`${this.apiUrl}/reset-password`, data);
  }

  isLoggedIn(): Observable<boolean> {
    const token = localStorage.getItem('token');
    const isValid = this.validateToken(token);
    if (!isValid) {
      localStorage.removeItem('token');
    }
    this.loggedIn.next(isValid);
    return this.loggedIn.asObservable();
  }

  // Validate the token
  private validateToken(token: string | null): boolean {
    if (!token) return false;

    try {
      const payload = this.decodeToken(token);

      // Check expiration
      const currentTime = Math.floor(Date.now() / 1000); // Current time in seconds
      return payload.exp && payload.exp > currentTime;
    } catch (error) {
      console.error('Invalid token:', error);
      return false;
    }
  }

  // Decode the token
  private decodeToken(token: string): any {
    const payloadPart = token.split('.')[1];
    return JSON.parse(atob(payloadPart));
  }

  // Get the username from the token or storage
  getUsername(): string {
    // Example: Decode JWT token to extract username
    const token = localStorage.getItem('token');
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.username || 'User';
    }
    return '';
  }

  isUserBusinessOwner(): boolean {
    const isBusinessOwner = localStorage.getItem('isBusinessOwner');
    if (isBusinessOwner) {
      return isBusinessOwner === 'Y';
    }
    return false;
  }

  // Perform logout
  logout(): void {
    localStorage.removeItem('token'); // Clear token
    this.setLoggedIn(false); // Update logged-in status
  }
}

