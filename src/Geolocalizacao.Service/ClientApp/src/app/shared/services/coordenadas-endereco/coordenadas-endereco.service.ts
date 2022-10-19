import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CoordenadasEnderecoService {
  constructor(private http: HttpClient) { }

  consultarEndereco(endereco: string): any {

    // Verifica se campo cep possui valor informado.
    if (endereco !== '') {
      // Valida o formato do CEP.
      return this.http.get(`https://nominatim.openstreetmap.org/search.php?q=${endereco}&polygon_geojson=1&format=json`);
    }

    return of({});
  }
}
