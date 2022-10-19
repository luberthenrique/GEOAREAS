import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPerfilUsuarioComponent } from './new-perfil-usuario.component';

describe('NewPerfilUsuarioComponent', () => {
  let component: NewPerfilUsuarioComponent;
  let fixture: ComponentFixture<NewPerfilUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewPerfilUsuarioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPerfilUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
