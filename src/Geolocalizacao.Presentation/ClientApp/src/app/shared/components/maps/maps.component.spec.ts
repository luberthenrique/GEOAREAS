import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { MapsComponent } from './maps.component';

let component: MapsComponent;
let fixture: ComponentFixture<MapsComponent>;

describe('maps component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ MapsComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(MapsComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
