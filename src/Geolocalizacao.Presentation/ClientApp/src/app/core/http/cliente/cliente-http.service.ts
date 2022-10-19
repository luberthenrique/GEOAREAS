import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { HttpClientService } from '../http-client.service';
import { ApiClienteDTO } from './api-cliente.dto';
import { ClienteDTO } from './cliente.dto';

@Injectable({
  providedIn: 'root'
})
export class ClienteHttpService extends HttpClientService {
  path = 'cliente';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  obterTodos() {
    return this.get<ClienteDTO[]>(this.path);
  }

  pesquisar(texto) {
    return this.get<ClienteDTO[]>(`${this.path}/pesquisar?texto=${texto}`);
  }

  obterPorId(id: string) {
    return this.getById<ClienteDTO>(id, this.path);
  }

  obterPorCpfCnpj(cpfCnpj: string) {
    return this.get<ClienteDTO>(`${this.path}/cpfCnpj/${cpfCnpj}`);
  }

  inserir(params) {
    return this.post<ClienteDTO>(this.path, params);
  } 

  alterar(id, params) {
    return this.put<ClienteDTO>(id, this.path, params);
  }

  deletar(id: string) {
    return this.delete(id, this.path);
  }

  cadastrarApi(id, params) {
    return this.post<any>(`${this.path}/${id}/api`, params);
  }

  obterApis(id) {
    return this.get<ApiClienteDTO[]>(`${this.path}/${id}/api`);
  }

  deletarApi(id: string) {
    return this.delete(id, `${this.path}/api`);
  }
}
