import { GeolocalizacaoDTO } from "../../../core/http/geolocalizacao/geolocalizacao.dto";

export class Geolocalizacao{
  type: string;
  coordenadas: any;

  public constructor(model){
    model = model || {};
    this.type = model.type || null;
    this.coordenadas = model.coordenadas || null;
  }

  static fromDTO(dto: GeolocalizacaoDTO): Geolocalizacao{
    return new Geolocalizacao(dto);
  }
}
