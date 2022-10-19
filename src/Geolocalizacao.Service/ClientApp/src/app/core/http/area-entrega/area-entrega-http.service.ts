import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { HttpClientService } from '../http-client.service';
import { AreaEntregaDTO } from './area-entrega.dto';

@Injectable({
  providedIn: 'root'
})
export class AreaEntregaHttpService extends HttpClientService {
  path = 'area-entrega';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  obterTodos() {
    return this.get<AreaEntregaDTO[]>(this.path);
  }

  obterPorId(id: string) {
    return this.getById<AreaEntregaDTO>(id, this.path);
  }

  inserir(params) {
    return this.post<AreaEntregaDTO>(this.path, params);
  }

  alterar(id, params) {
    return this.put<AreaEntregaDTO>(id, this.path, params);
  }

  deletar(id: string) {
    return this.delete(id, this.path);
  }
}
