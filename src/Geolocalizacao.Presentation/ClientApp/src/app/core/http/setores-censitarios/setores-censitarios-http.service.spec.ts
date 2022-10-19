import { TestBed } from '@angular/core/testing';
import { SetoresCensitariosHttpService } from './setores-censitarios-http.service';

describe('FilialHttpService', () => {
  let service: SetoresCensitariosHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SetoresCensitariosHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
