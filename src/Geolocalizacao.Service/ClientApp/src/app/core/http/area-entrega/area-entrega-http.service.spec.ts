import { TestBed } from '@angular/core/testing';

import { AreaEntregaHttpService } from './area-entrega-http.service';

describe('AreaEntregaHttpService', () => {
  let service: AreaEntregaHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AreaEntregaHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
