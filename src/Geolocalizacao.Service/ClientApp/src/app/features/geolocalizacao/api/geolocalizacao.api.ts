import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { GeolocalizacaoHttpService } from '../../../core/http/geolocalizacao/geolocalizacao-http.service';
import { Localizacao } from '../models/localizacao.model';

@Injectable({
  providedIn: 'root'
})
export class GeolocalizacaoApiService {

  constructor(private serviceHttp: GeolocalizacaoHttpService) { }

  obterPorLocal(params: any): Observable<Localizacao[]>{
    return this.serviceHttp.obterPorLocal(params)
      .pipe(map((objs) => objs.map((obj) => Localizacao.fromDTO(obj))));
  }
}
