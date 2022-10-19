import { ApiClienteDTO } from "../../../core/http/cliente/api-cliente.dto";
import { Usuario } from "../../usuario/models/usuario.model";

export class ApiCliente{
  id: string;
  idCliente: string;
  nome: string;
  apiKey: string;
  secretKey: string;
  idUsuarioInclusao: string;
  dataHoraInclusao: Date;
  idUsuarioAlteracao: string;
  dataHoraAlteracao: Date;

  usuarioInclusao: Usuario;
  usuarioAlteracao: Usuario;
  public constructor(model) {
    model = model || {};
    this.id = model.id || null;
    this.idCliente = model.idCliente || null;
    this.nome = model.nome || null;
    this.apiKey = model.apiKey || null;
    this.secretKey = model.secretKey || null;
    this.idUsuarioInclusao = model.idUsuarioInclusao || null;
    this.dataHoraInclusao = model.dataHoraInclusao || null;
    this.idUsuarioAlteracao = model.idUsuarioAlteracao || null;
    this.dataHoraAlteracao = model.dataHoraAlteracao || null;

    this.usuarioInclusao = model.usuarioInclusao || null;
    this.usuarioAlteracao = model.usuarioAlteracao || null;
  }
  static fromDTO(dto: ApiClienteDTO): ApiCliente{
    return new ApiCliente(dto);
  }
}
