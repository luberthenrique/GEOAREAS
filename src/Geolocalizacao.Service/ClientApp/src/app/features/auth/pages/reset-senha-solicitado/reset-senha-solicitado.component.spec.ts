import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetSenhaSolicitadoComponent } from './reset-senha-solicitado.component';

describe('ResetSenhaSolicitadoComponent', () => {
  let component: ResetSenhaSolicitadoComponent;
  let fixture: ComponentFixture<ResetSenhaSolicitadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResetSenhaSolicitadoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetSenhaSolicitadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
