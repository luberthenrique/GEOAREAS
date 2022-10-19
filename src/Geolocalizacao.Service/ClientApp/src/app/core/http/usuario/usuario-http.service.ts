import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { HttpClientService } from './../http-client.service';
import { UsuarioDTO } from './usuario.dto';

@Injectable({
  providedIn: 'root'
})
export class UsuarioHttpService extends HttpClientService {
  path = 'usuario';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  obterTodos() {
    return this.get<UsuarioDTO[]>(this.path);
  }

  obterPorId(id: string) {
    return this.getById<UsuarioDTO>(id, this.path);
  }

  inserir(params) {
    return this.post<UsuarioDTO>(this.path, params);
  }

  alterar(id, params) {
    return this.put<UsuarioDTO>(id, this.path, params);
  }

  updateData(id, params) {
    return this.put(id, `${this.path}/data`, params);
  }

  updatePicture(id, params) {
    return this.put(id, `${this.path}/picture`, params);
  }

  deletar(id: string) {
    return this.delete(id, this.path);
  }
}
