import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { HttpClientService } from '../http-client.service';
import { PerfilUsuarioDTO } from './perfil-usuario.dto';

@Injectable({
  providedIn: 'root'
})
export class PerfilUsuarioHttpService extends HttpClientService{
  path = 'perfilusuario';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  obterTodos() {
    return this.get<PerfilUsuarioDTO[]>(this.path);
  }

  obterPorId(id) {
    return this.getById<PerfilUsuarioDTO>(id, this.path);
  }

  obterPerfisHierarquia(params) {
    return this.get<PerfilUsuarioDTO[]>(`${this.path}/ListarPerfisHierarquia/${params}`);
  }

  inserir(params) {
    return this.post<PerfilUsuarioDTO>(this.path, params);
  }

  alterar(id, params) {
    return this.put<PerfilUsuarioDTO>(id, this.path, params);
  }

  deletar(id) {
    return this.delete(id, this.path);
  }
}
