import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AguardandoConfirmacaoComponent } from './aguardando-confirmacao.component';

describe('AguardandoConfirmacaoComponent', () => {
  let component: AguardandoConfirmacaoComponent;
  let fixture: ComponentFixture<AguardandoConfirmacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AguardandoConfirmacaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AguardandoConfirmacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
