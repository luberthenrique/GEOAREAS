import { UsuarioDTO } from "../usuario/usuario.dto";

export interface ApiClienteDTO {
  id: string;
  idCliente: string;
  nome: string;
  apiKey: string;
  secretKey: string;
  idUsuarioInclusao: string;
  dataHoraInclusao: Date;
  idUsuarioAlteracao: string;
  dataHoraAlteracao: Date;

  usuarioInclusao: UsuarioDTO;
  usuarioAlteracao: UsuarioDTO;
}
