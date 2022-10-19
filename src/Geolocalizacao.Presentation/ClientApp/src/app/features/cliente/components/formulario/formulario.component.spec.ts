import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { FormularioComponent } from './formulario.component';

let component: FormularioComponent;
let fixture: ComponentFixture<FormularioComponent>;

describe('formuario-cliente component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [FormularioComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
      fixture = TestBed.createComponent(FormularioComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
