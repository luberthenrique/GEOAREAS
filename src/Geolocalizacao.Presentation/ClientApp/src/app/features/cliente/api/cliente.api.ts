import { ClienteHttpService } from '../../../core/http/cliente/cliente-http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Cliente } from '../models/cliente.model';
import { ApiCliente } from '../models/api-cliente.model';

@Injectable({
  providedIn: 'root'
})
export class ClienteApiService {

  constructor(private serviceHttp: ClienteHttpService) { }

  obterTodos(): Observable<Cliente[]>{
    return this.serviceHttp.obterTodos()
    .pipe(map((objs) => objs.map((obj) => Cliente.fromDTO(obj))));
  }

  pesquisar(texto): Observable<Cliente[]> {
    return this.serviceHttp.pesquisar(texto)
      .pipe(map((objs) => objs.map((obj) => Cliente.fromDTO(obj))));
  }

  obterPorId(id): Observable<Cliente> {
    return this.serviceHttp.obterPorId(id)
      .pipe(map((obj) => Cliente.fromDTO(obj)));
  }

  obterPorCpfCnpj(cpfCnpj: string): Observable<Cliente> {
    return this.serviceHttp.obterPorCpfCnpj(cpfCnpj)
      .pipe(map((obj) => Cliente.fromDTO(obj)));
  }

  inserir(params) {
    return this.serviceHttp.inserir(params)
        .pipe(map(cliente => {
            return cliente;
        }));
  }  

  alterar(id, params) {
    return this.serviceHttp.alterar(id, params)
        .pipe(map(x => {
            return x;
        }));
  }

  deletar(id: string) {
      return this.serviceHttp.deletar(id)
          .pipe(map(obj => {
              return obj;
          }));
  }

  cadastrarApi(id, params) {
    return this.serviceHttp.cadastrarApi(id, params)
      .pipe(map(cliente => {
        return cliente;
      }));
  }

  obterApis(id) {
    return this.serviceHttp.obterApis(id)
      .pipe(map((objs) => objs.map((obj) => ApiCliente.fromDTO(obj))));
  }

  deletarApi(id) {
    return this.serviceHttp.deletarApi(id)
      .pipe(map(obj => {
        return obj;
      }));
  }
}
