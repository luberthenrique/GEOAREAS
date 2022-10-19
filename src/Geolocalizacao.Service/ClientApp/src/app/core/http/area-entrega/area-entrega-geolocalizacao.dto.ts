import { AreaEntregaGeolocalizacaoCoordenadaDTO } from "./area-entrega-geolocalizacao-coordenada.dto";

export interface AreaEntregaGeolocalizacaoDTO {
  id: string;
  idAreaEntrega: string;
  idPlace: number;
  latitude: string;
  longitude: string;
  displayName: string;
  area: string;
  cidade: string;
  uF: string;

  areaEntregaGeolocalizacaoCoordenada: AreaEntregaGeolocalizacaoCoordenadaDTO[];
}
