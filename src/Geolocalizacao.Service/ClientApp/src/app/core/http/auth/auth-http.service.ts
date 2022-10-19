import { HttpClientService } from '../http-client.service';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

import { map } from 'rxjs/operators';
import { LoginDTO } from './login.dto';
import { Usuario } from 'src/app/features/usuario/models/usuario.model';

@Injectable({
  providedIn: 'root'
})
export class AuthHttpService extends HttpClientService {
  path = 'identity';
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  login(params) {
    return this.post<Usuario>(`${this.path}/login`, params);
  }

  loginFacebook(params) {
    return this.post<Usuario>(`${this.path}/auth/facebook`, params);
  }

  logout() {
    return this.post(`${this.path}/logout`)
      .pipe();
  }

  register(params) {
    return this.post<Usuario>(`${this.path}/register`, params);
  }  

  resendConfirmation(params) {
    return this.post<Usuario>(`${this.path}/resend/confirmation`, params);
  }

  confirmEmail(params) {
    return this.post<Usuario>(`${this.path}/confirm/email`, params);
  }

  addPassword(id, params) {
    return this.put(id, `${this.path}/password/add`, params);
  }

  changePassword(id, params) {
    return this.put(id, `${this.path}/password/change`, params);
  }

  resetPassword(params) {
    return this.post<Usuario>(`${this.path}/password/reset`, params);
  }

  resetPasswordConfirm(id, params) {
    return this.put<Usuario>(id, `${this.path}/password/reset/confirm`, params);
  }
}
