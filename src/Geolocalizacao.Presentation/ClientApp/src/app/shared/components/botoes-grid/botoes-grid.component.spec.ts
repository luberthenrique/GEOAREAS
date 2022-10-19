import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BotoesGridComponent } from './botoes-grid.component';

describe('BotoesGridComponent', () => {
  let component: BotoesGridComponent;
  let fixture: ComponentFixture<BotoesGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BotoesGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BotoesGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
