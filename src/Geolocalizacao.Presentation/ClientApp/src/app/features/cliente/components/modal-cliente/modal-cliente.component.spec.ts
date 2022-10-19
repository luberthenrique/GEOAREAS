import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ModalClienteComponent } from './modal-cliente.component';

let component: ModalClienteComponent;
let fixture: ComponentFixture<ModalClienteComponent>;

describe('formuario-cliente component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [ModalClienteComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
      fixture = TestBed.createComponent(ModalClienteComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
