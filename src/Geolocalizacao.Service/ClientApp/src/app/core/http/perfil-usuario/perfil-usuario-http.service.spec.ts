import { TestBed } from '@angular/core/testing';

import { PerfilUsuarioHttpService } from './perfil-usuario-http.service';

describe('PerfilUsuarioHttpService', () => {
  let service: PerfilUsuarioHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PerfilUsuarioHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
