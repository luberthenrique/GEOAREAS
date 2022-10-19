import { PerfilUsuarioHttpService } from '../../../core/http/perfil-usuario/perfil-usuario-http.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PerfilUsuario } from '../models/perfil-usuario.model';

@Injectable({
  providedIn: 'root'
})
export class PerfilUsuarioApiService {

  constructor(private serviceHttp: PerfilUsuarioHttpService) { }

  obterTodos(): Observable<PerfilUsuario[]>{
    return this.serviceHttp.obterTodos()
      .pipe(map((objs) => objs.map((obj) => PerfilUsuario.fromDTO(obj))));
  }

  obterPerfisDaHierarquia(params): Observable<PerfilUsuario[]>{
    return this.serviceHttp.obterPerfisHierarquia(params)
      .pipe(map((objs) => objs.map((obj) => PerfilUsuario.fromDTO(obj))));
  }

  obterPorId(id): Observable<PerfilUsuario> {
    return this.serviceHttp.obterPorId(id)
      .pipe(map((obj) => PerfilUsuario.fromDTO(obj)));
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

  deletar(id: number) {
      return this.serviceHttp.deletar(id)
          .pipe(map(obj => {
              // auto logout if the logged in user deleted their own record
              return obj;
          }));
  }

}
