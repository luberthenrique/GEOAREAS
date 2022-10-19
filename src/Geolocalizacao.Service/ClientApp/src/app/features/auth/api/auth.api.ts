import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable  } from 'rxjs';
import { Injectable, OnDestroy } from '@angular/core';
import { Usuario } from '../../usuario/models/usuario.model';
import { AuthHttpService } from 'src/app/core/http/auth/auth-http.service';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService implements OnDestroy {

  private currentUserSubject = new BehaviorSubject<Usuario>(new Usuario({}));
  private currentUser: Observable<Usuario>;

  constructor(private serviceHttp: AuthHttpService) {
    this.currentUserSubject = new BehaviorSubject<Usuario>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
   }

  login(params): Observable<Usuario> {
    return this.serviceHttp
      .login(params)
      .pipe(map(user => {
        this.currentUserSubject.next(user);
        localStorage.setItem('currentUser', JSON.stringify(user));
        
        return user;
      }));
  }

  loginFacebook(params): Observable<Usuario> {
    return this.serviceHttp
      .loginFacebook(params)
      .pipe(map(user => {
        this.currentUserSubject.next(user);
        localStorage.setItem('currentUser', JSON.stringify(user));

        return user;
      }));
  }

  register(params) {
    return this.serviceHttp.register(params)
        .pipe(map(cliente => {
            return cliente;
        }));
  } 

  resendConfirmation(params) {
    return this.serviceHttp.resendConfirmation(params)
      .pipe(map(cliente => {
        return cliente;
      }));
  }

  confirmEmail(params) {
    return this.serviceHttp.confirmEmail(params)
        .pipe(map(cliente => {
            return cliente;
        }));
  }

  logout() {

    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    
    return this.serviceHttp.logout();
  }

  addPassword(id, params) {
    return this.serviceHttp.addPassword(id, params)
        .pipe(map(data => {
            return data;
        }));
  }

  changePassword(id, params) {
    return this.serviceHttp.changePassword(id, params)
        .pipe(map(data => {
            return data;
        }));
  }

  resetPassword(params) {
    return this.serviceHttp.resetPassword(params)
      .pipe(map(data => {
        return data;
      }));
  }

  resetPasswordConfirm(id, params) {
    return this.serviceHttp.resetPasswordConfirm(id, params)
      .pipe(map(data => {
        return data;
      }));
  }

  public get currentUserValue(): Usuario {
    return this.currentUserSubject?.value;
  }
  
  ngOnDestroy(): void {
    window.removeEventListener('storage', this.storageEventListener.bind(this));
  }
  private storageEventListener(event: StorageEvent) {
    if (event.storageArea === localStorage) {
      if (event.key === 'logout-event') {
        this.currentUserSubject.next(null);
      }

    }
  }

}
