import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetarSenhaConfirmarComponent } from './resetar-senha-confirmar.component';

describe('ResetarSenhaConfirmarComponent', () => {
  let component: ResetarSenhaConfirmarComponent;
  let fixture: ComponentFixture<ResetarSenhaConfirmarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ResetarSenhaConfirmarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetarSenhaConfirmarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
