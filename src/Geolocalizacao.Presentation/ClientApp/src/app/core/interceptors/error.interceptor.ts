import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthApiService } from 'src/app/features/auth/api/auth.api';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
      private authApiService: AuthApiService,
      private router: Router) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      var route = this.router.routerState.snapshot;
        return next.handle(request).pipe(catchError(err => {
          
            if (err.status === 401) {
                // efetuar login
                this.authApiService.logout();

                //this.router.navigate(['/auth/login'], { queryParams: { returnUrl: route }});
              this.router.navigate(['/auth/login']);
            }
            if (err.status === 403) {
              // sem autorização para o menu
              this.router.navigate(['/error/forbidden']);
          }
            else if (err.status === 404){
              this.router.navigate(['/error/notfound']);
            }

            // Erro validação bad request
            return throwError(err);

        }));
    }
}
