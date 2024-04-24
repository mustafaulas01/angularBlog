import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { LoginDto } from '../models/login.model';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  model: LoginDto

  constructor(private authService:AuthService,private cookiService:CookieService,private router:Router) {
    this.model = {
      email: '',
      password: ''
    }
  }


  onFormsSubmit():void {
    this.authService.login(this.model).subscribe({
      next:(response)=> {   
        //set Auth Cookie
        this.cookiService.set('Authorization',`Bearer ${response.token}`,
          undefined,'/',undefined,true,'Strict'
        );

        //set the user

        this.authService.setUser(
          {
            email:response.email,
            roles:response.roles
          }
        );
        
       this.router.navigateByUrl('/');
      }

    });
  }
}
