"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/* tslint:disable:no-unused-variable */
var testing_1 = require("@angular/core/testing");
var new_usuario_component_1 = require("./new-usuario.component");
describe('NewUsuarioComponent', function () {
    var component;
    var fixture;
    beforeEach((0, testing_1.waitForAsync)(function () {
        testing_1.TestBed.configureTestingModule({
            declarations: [new_usuario_component_1.NewUsuarioComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(new_usuario_component_1.NewUsuarioComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=new-usuario.component.spec.js.map