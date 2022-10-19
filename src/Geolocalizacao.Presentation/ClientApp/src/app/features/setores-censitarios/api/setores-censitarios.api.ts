import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { SetoresCensitariosHttpService } from '../../../core/http/setores-censitarios/setores-censitarios-http.service';

@Injectable({
  providedIn: 'root'
})
export class SetoresCensitariosApiService {

  constructor(private serviceHttp: SetoresCensitariosHttpService) { }

  obterTodos(): Observable<any[]>{
    return this.serviceHttp.obterTodos()
    .pipe(map((objs) => objs));
  }
  inserir(params) {
    return this.serviceHttp.inserir(params)
      .pipe(map(areas => {
        return areas;
      }));
  }
}
