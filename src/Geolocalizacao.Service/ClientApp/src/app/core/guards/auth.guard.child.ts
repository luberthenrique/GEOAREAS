import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthApiService } from 'src/app/features/auth/api/auth.api';

@Injectable({ providedIn: 'root' })
export class AuthGuardChild implements CanActivate {
    constructor(
        private router: Router,
        private authApiService: AuthApiService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
      const user = this.authApiService.currentUserValue;      

      var url = state.url.slice(1);
      var urlParams = url.split('/');

      if (url === '' && user.token !== null){
        return true;
      }

      
      //if (route.data.allowedRoles && route.data.allowedRoles.find(c=> c === user.claims)){
      //  return true;
      //}
      
      var claims = user.claims.filter(c => c.type.toLowerCase() === urlParams[0]);

       if (urlParams.length === 1 && claims.length > 0){
         return true;
       }
       else if (urlParams.length > 1 && claims.length > 0 ){
         var param = urlParams[1].replace('new', 'post').replace('edit', 'put');
         if  (claims.filter(c=> (c.value as string).toLowerCase() === param).length > 0){
           return true;
         }
       }

      this.router.navigate(['/error/forbidden']);

      return true;
    }
}
