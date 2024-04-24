import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';

import  jwt_decode, { jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {
 
  const cookieService=inject(CookieService);
  const authService=inject(AuthService);

  const router=inject(Router);

  const user=authService.getUser();
  //check for the JWT Token
  let token=cookieService.get('Authorization');
  if (token&&user) {
  
    token=token.replace('Bearer ','');
    const decoded_token:any= token= jwtDecode(token);
  
    //check if token exprired

    const expiration_date=decoded_token .exp*1000;
    const current_time=new Date().getTime();

    if(expiration_date<current_time)
      {
        authService.logout();

        return router.createUrlTree(['/login'],{queryParams:{returnUrl:state.url}});

      }

      else{
        //token is still valid.
        
        if(user.roles.includes('Writer'))
          {
            return true;
          }

          else 
          {
            alert('Unauthorized');
            return false;
          }

      }


  }
  else {
    authService.logout();

    return router.createUrlTree(['/login'],{queryParams:{returnUrl:state.url}});

  }


};
