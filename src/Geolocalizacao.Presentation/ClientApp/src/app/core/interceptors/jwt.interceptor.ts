import { Inject, Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthApiService } from 'src/app/features/auth/api/auth.api';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authApiService: AuthApiService,
    @Inject('BASE_URL') private baseUrl: string  ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to the api url
        const user = this.authApiService.currentUserValue;
      const isLoggedIn = user && user.token;

      const isApiUrl = request.url.startsWith(this.baseUrl);

      if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${user.token}`
                }
            });
        }

        return next.handle(request);
    }
}
