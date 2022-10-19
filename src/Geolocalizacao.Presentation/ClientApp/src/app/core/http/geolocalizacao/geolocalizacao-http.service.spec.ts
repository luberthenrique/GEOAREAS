import { TestBed } from '@angular/core/testing';

import { GeolocalizacaoHttpService } from './geolocalizacao-http.service';

describe('GeolocalizacaoHttpService', () => {
  let service: GeolocalizacaoHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeolocalizacaoHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
