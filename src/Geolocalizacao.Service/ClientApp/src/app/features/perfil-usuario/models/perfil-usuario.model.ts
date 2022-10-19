import { PerfilUsuarioDTO } from 'src/app/core/http/perfil-usuario/perfil-usuario.dto';

export class PerfilUsuario{
  id: string;
  nome: string;
  isAdmin: boolean;

  claims: any;
  perfisVinculados: string[];

  private constructor(model){
    model = model || {};
    this.id = model.id || null;
    this.nome = model.nome || null;
    this.isAdmin = model.isAdmin || false;
    this.claims = model.claims;
    this.perfisVinculados = model.perfisVinculados || [];
  }

  static fromDTO(dto: PerfilUsuarioDTO): PerfilUsuario{
    return new PerfilUsuario(dto);
  }
}
