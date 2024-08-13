import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginModel } from 'src/app/interfaces/Auth/LoginModel ';
import { AuthenticatedResponse } from 'src/app/interfaces/Auth/AuthenticatedResponse';
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/Auth';
  private jwtToken: string | null = null;
  // constructor(private http: HttpClient) { }
  // getCliente(id: number) {
  //   const url = `${this.apiUrl}/${id}`;
  //   return this.http.get<any>(url);
  // }
  
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject(null);

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {}
  //constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>('/login', { username, password }).pipe(
      tap(response => {
        this.storeTokens(response.token, response.refreshToken);
      }),
      catchError(error => throwError(error))
    );
  }

  refreshToken(): Observable<any> {
    return this.http.post<any>('/api/auth/refresh', { refreshToken: this.getRefreshToken() }).pipe(
      tap(response => {
        this.storeTokens(response.token, response.refreshToken);
      }),
      catchError(error => {
        this.logout();
        return throwError(error);
      })
    );
  }

  private storeTokens(token: string, refreshToken: string) {
    localStorage.setItem('token', token);
    localStorage.setItem('refreshToken', refreshToken);
  }

  getToken(): string | null {
    return localStorage.getItem('jwt');
  }

  getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
  }

  AuthUser(credentials: LoginModel) : Observable<any> {
    return this.http.post<AuthenticatedResponse>(`${this.apiUrl}/login`, credentials, {
            headers: new HttpHeaders({ "Content-Type": "application/json"})
          });
  }

  storeToken(token: string) {
    localStorage.setItem('jwt', token);
  }

  removeToken() {
    localStorage.removeItem('jwt');
  }

  getUserIdFromToken(): string | null {
    this.jwtToken = this.getToken();

    if (!this.jwtToken) {
      return null;
    }

    try {
      const decodedToken: any = jwtDecode(this.jwtToken);
      return decodedToken.userId || null;
    } 
    catch (error) {
      console.error('Erro ao decodificar o token JWT:', error);
      return null;
    }
  }

}