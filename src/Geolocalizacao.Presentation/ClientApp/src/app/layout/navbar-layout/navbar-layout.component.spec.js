"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var platform_browser_1 = require("@angular/platform-browser");
var navbar_layout_component_1 = require("./navbar-layout.component");
var component;
var fixture;
describe('navbar-layout component', function () {
    beforeEach((0, testing_1.waitForAsync)(function () {
        testing_1.TestBed.configureTestingModule({
            declarations: [navbar_layout_component_1.NavbarLayoutComponent],
            imports: [platform_browser_1.BrowserModule],
            providers: [
                { provide: testing_1.ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = testing_1.TestBed.createComponent(navbar_layout_component_1.NavbarLayoutComponent);
        component = fixture.componentInstance;
    }));
    it('should do something', (0, testing_1.waitForAsync)(function () {
        expect(true).toEqual(true);
    }));
});
//# sourceMappingURL=navbar-layout.component.spec.js.map