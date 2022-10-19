import { PerfilUsuarioDTO } from "../perfil-usuario/perfil-usuario.dto";

export interface UsuarioDTO {
  id: string;
  idImagem: string;
  idPerfil: string;
  idUsuario: string;
  nome: string;
  habilitado: boolean;
  email: string;
  senha: string;

  lembrarMe: string;
  confirmacaoSenha: string;
  token: string;

  imagem: any;
  senhaCadastrada: boolean;

  perfilUsuario: PerfilUsuarioDTO;
  claims: any;
}
