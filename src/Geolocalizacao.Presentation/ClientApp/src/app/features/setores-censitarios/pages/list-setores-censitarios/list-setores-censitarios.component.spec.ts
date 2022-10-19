import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSetoresCensitariosComponent } from './list-setores-censitarios.component';

describe('ListSetoresCensitariosComponent', () => {
  let component: ListSetoresCensitariosComponent;
  let fixture: ComponentFixture<ListSetoresCensitariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListSetoresCensitariosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListSetoresCensitariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
