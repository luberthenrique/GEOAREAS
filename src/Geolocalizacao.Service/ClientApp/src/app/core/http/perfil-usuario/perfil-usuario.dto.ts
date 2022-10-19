export interface PerfilUsuarioDTO{
  id: string;
  nome: string;
  isAdmin: boolean;

  claims: any;
  perfisVinculados: string[];
}
