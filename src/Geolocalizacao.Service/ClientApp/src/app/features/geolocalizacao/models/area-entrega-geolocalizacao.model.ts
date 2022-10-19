import { AreaEntregaGeolocalizacaoDTO } from "../../../core/http/area-entrega/area-entrega-geolocalizacao.dto";
import { AreaEntregaGeolocalizacaoCoordenada } from "./area-entrega-geolocalizacao-coordenada.model";

export class AreaEntregaGeolocalizacao{
  id: string;
  idAreaEntrega: string;
  idPlace: number;
  latitude: string;
  longitude: string;
  displayName: string;
  area: string;
  cidade: string;
  uF: string;

  areaEntregaGeolocalizacaoCoordenada: AreaEntregaGeolocalizacaoCoordenada[];

  public constructor(model){
    model = model || {};
    this.id = model.id || null;
    this.idAreaEntrega = model.idAreaEntrega || null;
    this.idPlace = model.idPlace || null;
    this.latitude = model.latitude || null;
    this.longitude = model.longitude || null;
    this.displayName = model.displayName || null;
    this.area = model.area || null;
    this.cidade = model.cidade || null;
    this.uF = model.uF || null;
    this.areaEntregaGeolocalizacaoCoordenada = model.areaEntregaGeolocalizacaoCoordenada || null;
  }

  static fromDTO(dto: AreaEntregaGeolocalizacaoDTO): AreaEntregaGeolocalizacao{
    return new AreaEntregaGeolocalizacao(dto);
  }
}
