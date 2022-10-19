import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AreaEntregaHttpService } from '../../../core/http/area-entrega/area-entrega-http.service';
import { AreaEntrega } from '../models/area-entrega.model';
import { Localizacao } from '../models/localizacao.model';

@Injectable({
  providedIn: 'root'
})
export class AreaEntregaApiService {

  constructor(private serviceHttp: AreaEntregaHttpService) { }

  obterTodos(): Observable<AreaEntrega[]> {
    return this.serviceHttp.obterTodos()
      .pipe(map((objs) => objs.map((obj) => AreaEntrega.fromDTO(obj))));
  }

  obterPorId(id): Observable<AreaEntrega> {
    return this.serviceHttp.obterPorId(id)
      .pipe(map((obj) => AreaEntrega.fromDTO(obj)));
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

  deletar(id) {
    return this.serviceHttp.deletar(id)
      .pipe(map(x => {
        return x;
      }));
  }
}
