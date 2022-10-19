import { TestBed } from '@angular/core/testing';
import { ClienteHttpService } from './cliente-http.service';

describe('FilialHttpService', () => {
  let service: ClienteHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClienteHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
