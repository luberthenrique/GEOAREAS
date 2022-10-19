import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { GerenciamentoApiComponent } from './gerenciamento-api.component';

let component: GerenciamentoApiComponent;
let fixture: ComponentFixture<GerenciamentoApiComponent>;

describe('formuario-cliente component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
          declarations: [GerenciamentoApiComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
      fixture = TestBed.createComponent(GerenciamentoApiComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
