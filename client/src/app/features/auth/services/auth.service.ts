import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { LoginDto } from '../models/login.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { environment } from '../../../../environments/environment.development';
import { User } from '../models/user.model';
import { ThisReceiver } from '@angular/compiler';
import { CookieService } from 'ngx-cookie-service';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user=new BehaviorSubject< User | undefined>(undefined);
  localStorage:undefined | Storage=undefined;

  constructor(private http:HttpClient,private cookieService:CookieService,@Inject(DOCUMENT) private document: Document) { 
   this.localStorage = document.defaultView?.localStorage;

  }



  login(reguest:LoginDto):Observable<LoginResponse>
  {
   return  this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/auth/login`,
    {
      email:reguest.email,
      password:reguest.password
    });
  }


  setUser(user: User): void {
    this.$user.next(user);

    if (this.localStorage) {
      this.localStorage.setItem('user-email', user.email);
      localStorage.setItem('user-roles', user.roles.join(','));

    }

  }

  user():Observable< User | undefined >
  {
    return this.$user.asObservable();
  }

  logout():void
  {
    if(this.localStorage)
    this.localStorage.clear();
    this.cookieService.delete("Authorization","/");
    this.$user.next(undefined);
  }

  getUser(): User | undefined {


    if (this.localStorage) {

      const email = this.localStorage.getItem('user-email');
      const roles = this.localStorage.getItem('user-roles');
  
      if(email && roles)
        {
          
          const user:User= 
          {
            email:email,
            roles:roles.split(',')
          }
          this.$user.next(user);
          return user;
        }

    }

      return undefined;

  }

}
