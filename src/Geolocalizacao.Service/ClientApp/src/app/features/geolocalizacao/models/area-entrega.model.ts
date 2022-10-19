import { AreaEntregaDTO } from "../../../core/http/area-entrega/area-entrega.dto";
import { AreaEntregaGeolocalizacao } from "./area-entrega-geolocalizacao.model";

export class AreaEntrega{
  id: string;
  nome: string;
  cor: string;
  valor: number;
  tempo: number;

  areaEntregaGeolocalizacao: AreaEntregaGeolocalizacao[];

  public constructor(model){
    model = model || {};
    this.id = model.id || null;
    this.nome = model.nome || null;
    this.cor = model.cor || null;
    this.valor = model.valor || 0;
    this.tempo = model.tempo || 0;
    this.areaEntregaGeolocalizacao = model.areaEntregaGeolocalizacao || null;
  }

  static fromDTO(dto: AreaEntregaDTO): AreaEntrega{
    return new AreaEntrega(dto);
  }
}
