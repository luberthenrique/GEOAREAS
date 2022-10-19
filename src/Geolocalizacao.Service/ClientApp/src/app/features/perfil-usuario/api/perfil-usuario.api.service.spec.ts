import { TestBed } from '@angular/core/testing';

import { PerfilUsuarioApiService } from './perfil-usuario.api';

describe('PerfilApiService', () => {
  let service: PerfilUsuarioApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PerfilUsuarioApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
