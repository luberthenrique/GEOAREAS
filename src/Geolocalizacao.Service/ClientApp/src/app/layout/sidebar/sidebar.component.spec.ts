import { TestBed, ComponentFixture, ComponentFixtureAutoDetect, waitForAsync } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { SidebarComponent } from './sidebar.component';

let component: SidebarComponent;
let fixture: ComponentFixture<SidebarComponent>;

describe('sidebar component', () => {
    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            declarations: [ SidebarComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(SidebarComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', waitForAsync(() => {
        expect(true).toEqual(true);
    }));
});
