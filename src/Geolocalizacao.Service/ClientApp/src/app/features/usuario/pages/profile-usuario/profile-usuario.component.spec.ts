import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileUsuarioComponent } from './profile-usuario.component';

describe('ProfileUsuarioComponent', () => {
  let component: ProfileUsuarioComponent;
  let fixture: ComponentFixture<ProfileUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileUsuarioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
