import { TestBed } from '@angular/core/testing';
import { SetoresCensitariosApiService } from './setores-censitarios.api';

describe('SetoresCensitariosApiService', () => {
  let service: SetoresCensitariosApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SetoresCensitariosApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
