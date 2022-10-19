import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPerfilUsuarioComponent } from './list-perfil-usuario.component';

describe('ListPerfilUsuarioComponent', () => {
  let component: ListPerfilUsuarioComponent;
  let fixture: ComponentFixture<ListPerfilUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListPerfilUsuarioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListPerfilUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
