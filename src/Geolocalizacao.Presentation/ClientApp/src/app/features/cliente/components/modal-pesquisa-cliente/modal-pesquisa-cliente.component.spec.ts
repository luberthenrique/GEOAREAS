import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ModalPesquisaClienteComponent } from './modal-pesquisa-cliente.component';

let component: ModalPesquisaClienteComponent;
let fixture: ComponentFixture<ModalPesquisaClienteComponent>;

describe('modal-pesquisa-cliente component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [ModalPesquisaClienteComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
      fixture = TestBed.createComponent(ModalPesquisaClienteComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
