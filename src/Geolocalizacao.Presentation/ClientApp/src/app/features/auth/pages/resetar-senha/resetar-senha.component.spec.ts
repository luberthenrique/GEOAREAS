import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetarSenhaComponent } from './resetar-senha.component';

describe('RegisterComponent', () => {
  let component: ResetarSenhaComponent;
  let fixture: ComponentFixture<ResetarSenhaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ResetarSenhaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetarSenhaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
