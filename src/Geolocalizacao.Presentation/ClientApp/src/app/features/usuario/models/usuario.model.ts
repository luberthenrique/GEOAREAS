import { UsuarioDTO } from '../../../core/http/usuario/usuario.dto';
import { PerfilUsuario } from '../../perfil-usuario/models/perfil-usuario.model';

export class Usuario{
  id: string;
  idImagem: string;
  idPerfil: string;
  idFilial: string;
  idUsuario: string;
  nome: string;
  habilitado: boolean;
  email: string;
  senha: string;

  lembrarMe: string;
  confirmacaoSenha: string;
  token: string;

  imagem: any;
  perfilUsuario: PerfilUsuario;
  senhaCadastrada: boolean;

  claims: any;

  public constructor(model){
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

  static fromDTO(dto: UsuarioDTO): Usuario{
    return new Usuario(dto);
  }
}
