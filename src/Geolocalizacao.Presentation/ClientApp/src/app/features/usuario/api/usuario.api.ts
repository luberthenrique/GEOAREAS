import { UsuarioHttpService } from '../../../core/http/usuario/usuario-http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Usuario } from '../models/usuario.model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioApiService {

  constructor(private serviceHttp: UsuarioHttpService) { }

  obterTodos(): Observable<Usuario[]>{
    return this.serviceHttp.obterTodos()
    .pipe(map((objs) => objs.map((obj) => Usuario.fromDTO(obj))));
  }

  obterPorId(id): Observable<Usuario> {
    return this.serviceHttp.obterPorId(id)
      .pipe(map((obj) => Usuario.fromDTO(obj)));
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

  updateData(id, params) {
    return this.serviceHttp.updateData(id, params)
        .pipe(map(data => {
            return data;
        }));
  }

  updatePicture(id, params) {
    return this.serviceHttp.updatePicture(id, params)
        .pipe(map(data => {
            return data;
        }));
  }

  deletar(id: string) {
      return this.serviceHttp.deletar(id)
          .pipe(map(obj => {
              // auto logout if the logged in user deleted their own record
              return obj;
          }));
  }
}
