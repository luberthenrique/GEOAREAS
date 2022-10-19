import { LocalizacaoDTO } from "../../../core/http/geolocalizacao/localizacao.dto";
import { Geolocalizacao } from "./geolocalizacao.model";

export class Localizacao{
  id: string;
  idPlace: number;
  licence: string;
  osmType: string;
  osmId: number;
  doundingBox: string[];
  lat: string;
  lon: string;
  displayName: string;
  class: string;
  type: string;
  importance: number;
  icon: string;

  area: string;
  cidade: string;
  uf: string;

  geojson: Geolocalizacao;

  public constructor(model){
    model = model || {};
    this.id = model.id || null;
    this.idPlace = model.idPlace || null;
    this.licence = model.licence || null;
    this.osmType = model.osmType || null;
    this.osmId = model.osmId || null;
    this.doundingBox = model.doundingBox || null;
    this.lat = model.lat || null;
    this.lon = model.lon || null;
    this.displayName = model.displayName || null;
    this.class = model.class || null;
    this.type = model.type || null;
    this.importance = model.importance || null;
    this.icon = model.icon || null;
    this.area = model.area || null;
    this.cidade = model.cidade || null;
    this.uf = model.uf || null;
    this.geojson = model.geojson || null;
  }

  static fromDTO(dto: LocalizacaoDTO): Localizacao{
    return new Localizacao(dto);
  }
}
