"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Usuario = void 0;
var Usuario = /** @class */ (function () {
    function Usuario(model) {
        model = model || {};
        this.id = model.id || null;
        this.idImagem = model.idImagem || null;
        this.idPerfil = model.idPerfil || null;
        this.idFilial = model.idFilial || null;
        this.idUsuario = model.idUsuario || null;
        this.nome = model.nome || null;
        this.habilitado = model.habilitado || null;
        this.email = model.email || null;
        this.senha = model.senha || null;
        this.lembrarMe = model.lembrarMe || null;
        this.confirmacaoSenha = model.confirmacaoSenha || null;
        this.token = model.token || null;
        this.imagem = model.imagem || null;
        this.senhaCadastrada = model.senhaCadastrada || false;
        this.claims = model.claims || false;
        this.perfilUsuario = model.perfilUsuario || null;
    }
    Usuario.fromDTO = function (dto) {
        return new Usuario(dto);
    };
    return Usuario;
}());
exports.Usuario = Usuario;
//# sourceMappingURL=usuario.model.js.map