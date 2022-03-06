import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { API_URL, LOGIN_URL, REGISTER_ADMIN_URL, REGISTER_URL, PROFILE_URL } from 'src/environments/environment';
import { last, map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { Profile } from '../models/user/profile';
import { User } from '../models/user/user';
import { Register } from '../models/user/register';
import { UserRole } from '../common/userRole';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  static currentUser: any;
  
  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
   }
  
  public get currentUserName(): String {
    if (this.currentUserSubject.value != null){
      var user = this.currentUserSubject.value;
      return user.lastName + ' ' + user.firstName;
    }
    else {
      return null;
    }
  }

  public currentUserValue(): User{
    return this.currentUserSubject.value;
  }

  public isAuthenticated(): boolean{
    return this.currentUserSubject.value != null;
  }

  public isAdmin(): boolean{
    if(this.isAuthenticated() === true){
      return this.currentUserSubject.value.role === UserRole.Admin;
    }else{
      return false;
    }
  }

  login(email: string, password: String){
    return this.http.post(API_URL + LOGIN_URL, { email, password }).pipe(
      map((user: User) => {
        sessionStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      })
    );
  }

  logout() {
    sessionStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  register(model: Register){

    return this.http.post(API_URL + REGISTER_URL, model).pipe(
      map((result: boolean) =>{
        return result;
      })
    );
  }

  registerAdmin(model: Register){

    return this.http.post(API_URL + REGISTER_ADMIN_URL, model).pipe(
      map((result: boolean) =>{
        return result;
      })
    );
  }

  public getProfile(){
    return this.http.get(API_URL + PROFILE_URL).pipe(
      map((data : Profile) => {
        return data;
      })
    );
  }

  editProfile(model: Profile){

    return this.http.post(API_URL + PROFILE_URL, model).pipe(
      map((result: boolean) =>{
        return result;
      })
    );
  }
}
