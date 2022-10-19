import { ApiClienteDTO } from "./api-cliente.dto";

export interface ClienteDTO {
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

  apis: ApiClienteDTO[];
}
