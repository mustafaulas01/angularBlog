import { HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Inject, inject } from '@angular/core';





export const authInterceptor: HttpInterceptorFn = (req, next) => {


  const shouldInterceptRequest=req.urlWithParams.indexOf('addAuth=true',0)>-1?true:false;

  if (shouldInterceptRequest) {
    const cookieService = inject(CookieService);

    const authRequest = req.clone({
      setHeaders: {
        'Authorization': cookieService.get('Authorization'),
        'content-type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Access-Control-Allow-Methods':'POST,GET,PUT,PATCH,DELETE,OPTIONS',
        'Access-Control-Allow-Credentials':'true'
      }
    });


    return next(authRequest);
  }

  return next(req);

};


