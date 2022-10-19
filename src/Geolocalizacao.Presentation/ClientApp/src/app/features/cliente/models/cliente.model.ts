import { ClienteDTO } from "../../../core/http/cliente/cliente.dto";
import { ApiCliente } from "./api-cliente.model";

export class Cliente{
  id: string;
  cnpj: string;
  inscricaoMunicipal: string;
  razaoSocial: string;
  observacao: string;

  logradouro: string;
  numero: string;
  complemento: string;
  bairro: string;
  cidade: string;
  uf: string;
  cep: string

  telefone1: string;
  telefone2: string;
  email: string;
  datahoraInclusao: Date;

  apis: ApiCliente[];

  public constructor(model){
    model = model || {};
    this.id = model.id || null;
    this.cnpj = model.cnpj || null;
    this.inscricaoMunicipal = model.inscricaoMunicipal || null;
    this.razaoSocial = model.razaoSocial || null;
    this.observacao = model.observacao || null;
    this.logradouro = model.logradouro || null;
    this.numero = model.numero || null;
    this.complemento = model.complemento || null;
    this.bairro = model.bairro || null;
    this.cidade = model.cidade || null;
    this.uf = model.uf || null;
    this.cep = model.cep || null;

    this.telefone1 = model.telefone1 || null;
    this.telefone2 = model.telefone2 || null;
    this.email = model.email || null;
    this.datahoraInclusao = model.datahoraInclusao || null;

    this.apis = model.apis || null;
  }

  static fromDTO(dto: ClienteDTO): Cliente{
    return new Cliente(dto);
  }
}
