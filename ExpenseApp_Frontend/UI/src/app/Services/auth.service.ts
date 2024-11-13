import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, Subject } from 'rxjs';
import { User } from '../models/expense.model';
import { TokenApiModel } from '../models/token-api.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  
 
  private baseUrl: string = 'https://localhost:7144/api/Auth/';
 
   userStatus: Subject<string> = new Subject();
 // userStatus: BehaviorSubject<string> = new BehaviorSubject<string>(this.getUserStatus());
  private userPayload:any;
  private jwtHelper = new JwtHelperService();
  constructor(
    // private jwt: JwtHelperService,
    private http:HttpClient,
     private router: Router) { 
      this.userPayload = this.decodedToken();
  }
  private getUserStatus(): string {
    return localStorage.getItem('userStatus') || 'loggedOff';
  }
  signUp(userobj:any): Observable<any>{
    return this.http.post<any>(`${this.baseUrl}register`,userobj)
  }
  login(userobj:any): Observable<any>{
    return this.http.post<any>(`${this.baseUrl}login`,userobj)
  }
  logOut() {
    // localStorage.removeItem('access_token');
    // // this.userInfo = null;
    // this.userStatus.next('loggedOff');
    // this.userStateService.updateTokens(0);
    // this.userStatus.next('loggedOff');
    localStorage.clear();
    this.userStatus.next('loggedOff');
    this.router.navigate(['login'])
  }

  // signOut(){
  //   localStorage.clear();
  //   this.userStatus.next('loggedOff');
  //   this.router.navigate(['login'])
  // }
  

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }
  storeRefreshToken(tokenValue: string){
    localStorage.setItem('refreshToken', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }
  getRefreshToken(){
    return localStorage.getItem('refreshToken')
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (token) {
      // Check if the token is expired
      return !this.jwtHelper.isTokenExpired(token);
    }
    return false;
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(jwtHelper.decodeToken(token))
    return jwtHelper.decodeToken(token)
  }

  getfullNameFromToken(){
    if(this.userPayload)
    return this.userPayload.name;
  }

  // getRoleFromToken(){
  //   if(this.userPayload)
  //   return this.userPayload.role;
  // }

  renewToken(tokenApi : TokenApiModel){
    return this.http.post<any>(`${this.baseUrl}refresh`, tokenApi)
  }


 getRoleFromToken(): string | null {
  if (this.userPayload) {
    return this.userPayload.role;
  }
  return null;
}
getCurrentUserId(): number {
  const tokenPayload = this.decodedToken();
  return tokenPayload.nameid; 
}

// Get all users
getUsers(): Observable<any[]> {
  return this.http.get<any[]>(`${this.baseUrl}users`);
}

// Update a user
updateUser(userId: string, userData: any): Observable<any> {
  return this.http.put(`${this.baseUrl}users/${userId}`, userData);
}

// Delete a user
deleteUser(userId: string): Observable<any> {
  return this.http.delete(`${this.baseUrl}users/${userId}`);
}
}
