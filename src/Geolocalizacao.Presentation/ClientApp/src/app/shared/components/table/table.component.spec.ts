import { TestBed, ComponentFixture, ComponentFixtureAutoDetect, waitForAsync } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { TableComponent } from './table.component';

let component: TableComponent;
let fixture: ComponentFixture<TableComponent>;

describe('table component', () => {
    //beforeEach(waitForAsync(() => {
    //    TestBed.configureTestingModule({
    //        declarations: [ TableComponent ],
    //        imports: [ BrowserModule ],
    //        providers: [
    //            { provide: ComponentFixtureAutoDetect, useValue: true }
    //        ]
    //    });
    //    fixture = TestBed.createComponent(TableComponent);
    //    component = fixture.componentInstance;
    //}));

    //it('should do something', waitForAsync(() => {
    //    expect(true).toEqual(true);
    //}));
});
