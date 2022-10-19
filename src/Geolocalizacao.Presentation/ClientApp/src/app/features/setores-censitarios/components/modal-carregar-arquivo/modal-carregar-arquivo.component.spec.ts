import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { ModalCarregarArquivoComponent } from './modal-carregar-arquivo.component';

let component: ModalCarregarArquivoComponent;
let fixture: ComponentFixture<ModalCarregarArquivoComponent>;

describe('modal-obter-regiao component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ ModalCarregarArquivoComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(ModalCarregarArquivoComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
