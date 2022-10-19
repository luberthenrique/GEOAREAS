import { GeolocalizacaoDTO } from "./geolocalizacao.dto";

export interface LocalizacaoDTO {
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

  geojson: GeolocalizacaoDTO
}
