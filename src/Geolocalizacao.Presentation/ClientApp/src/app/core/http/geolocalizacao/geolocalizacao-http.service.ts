import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

import { HttpClientService } from '../http-client.service';
import { LocalizacaoDTO } from './localizacao.dto';

@Injectable({
  providedIn: 'root'
})
export class GeolocalizacaoHttpService extends HttpClientService {
  path = 'geolocalizacao';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(http, baseUrl);
  }

  obterPorLocal(params) {
    return this.get<LocalizacaoDTO[]>(this.path, { params: params});
  }

  obterCidadesPorRaio(params) {
    return this.get<any[]>(this.path, { params: params });
  }
}
