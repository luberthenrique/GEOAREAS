import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class SetoresCensitariosHttpService extends HttpClientService {
  path = 'setores-censitarios';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  obterTodos() {
    return this.get<any[]>(this.path);
  }

  inserir(params) {
    return this.post<any>(this.path, params);
  }
}
