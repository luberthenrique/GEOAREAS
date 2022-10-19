import { AreaEntregaGeolocalizacaoCoordenadaDTO } from "../../../core/http/area-entrega/area-entrega-geolocalizacao-coordenada.dto";

export class AreaEntregaGeolocalizacaoCoordenada{
  id: string;
  idAreaEntregaGeolocalizacao: string;
  latitude: number;
  longitude: number;

  public constructor(model){
    model = model || {};
    this.id = model.id || null;
    this.idAreaEntregaGeolocalizacao = model.idAreaEntregaGeolocalizacao || null;
    this.latitude = model.latitude || null;
    this.longitude = model.longitude || null;
  }

  static fromDTO(dto: AreaEntregaGeolocalizacaoCoordenadaDTO): AreaEntregaGeolocalizacaoCoordenada{
    return new AreaEntregaGeolocalizacaoCoordenada(dto);
  }
}
