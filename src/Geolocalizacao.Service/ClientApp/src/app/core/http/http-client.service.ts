import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { map } from 'rxjs/operators';
import { Inject } from '@angular/core';

interface HttpResponse<T>{
  data: T;
  code: number;
  message: number;
  erros: any[];
}

export class HttpClientService{

  constructor(protected http: HttpClient,
    protected baseUrl: string) { }

  get<T>(
    path: string,
    options?: { headers?: HttpHeaders, params: HttpParams }
  ): Observable<T>{
    return this.http
    .get<T>(`${this.baseUrl}api/${path}`, options);
  }

  getById<T>(
    id: string,
    path: string,
  ) {
    return this.http.get<T>(`${this.baseUrl}api/${path}/${id}`);
  }

  post<T>(
    path: string,
    options?: { headers?: HttpHeaders, params: HttpParams }
  ) {
    return this.http.post<T>(`${this.baseUrl}api/${path}`, options);
  }

  put<T>(
    id: string,
    path: string,
    options?: { headers?: HttpHeaders, params: HttpParams }) {
    return this.http.put<T>(`${this.baseUrl}api/${path}/${id}`, options)
        .pipe(map(x => {
            return x;
        }));
  }

  delete(
    id: string,
    path: string) {
      return this.http.delete(`${this.baseUrl}api/${path}/${id}`)
          .pipe(map(x => {
              // auto logout if the logged in user deleted their own record
              return x;
          }));
  }
}
