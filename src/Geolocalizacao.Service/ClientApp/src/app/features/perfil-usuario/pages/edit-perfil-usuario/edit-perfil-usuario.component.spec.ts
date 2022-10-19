import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPerfilUsuarioComponent } from './edit-perfil-usuario.component';

describe('EditPerfilUsuarioComponent', () => {
  let component: EditPerfilUsuarioComponent;
  let fixture: ComponentFixture<EditPerfilUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPerfilUsuarioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPerfilUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
