"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/* tslint:disable:no-unused-variable */
var testing_1 = require("@angular/core/testing");
var edit_usuario_component_1 = require("./edit-usuario.component");
describe('EditUsuarioComponent', function () {
    var component;
    var fixture;
    beforeEach((0, testing_1.waitForAsync)(function () {
        testing_1.TestBed.configureTestingModule({
            declarations: [edit_usuario_component_1.EditUsuarioComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(edit_usuario_component_1.EditUsuarioComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', function () {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=edit-usuario.component.spec.js.map