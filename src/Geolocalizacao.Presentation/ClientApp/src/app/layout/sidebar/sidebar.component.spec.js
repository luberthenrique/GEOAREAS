"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var platform_browser_1 = require("@angular/platform-browser");
var sidebar_component_1 = require("./sidebar.component");
var component;
var fixture;
describe('sidebar component', function () {
    beforeEach((0, testing_1.waitForAsync)(function () {
        testing_1.TestBed.configureTestingModule({
            declarations: [sidebar_component_1.SidebarComponent],
            imports: [platform_browser_1.BrowserModule],
            providers: [
                { provide: testing_1.ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = testing_1.TestBed.createComponent(sidebar_component_1.SidebarComponent);
        component = fixture.componentInstance;
    }));
    it('should do something', (0, testing_1.waitForAsync)(function () {
        expect(true).toEqual(true);
    }));
});
//# sourceMappingURL=sidebar.component.spec.js.map