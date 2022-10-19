import { AreaEntregaGeolocalizacaoDTO } from "./area-entrega-geolocalizacao.dto";

export interface AreaEntregaDTO {
  id: string;
  nome: string;
  cor: string;
  valor: number;
  tempo: number;

  areaEntregaGeolocalizacao: AreaEntregaGeolocalizacaoDTO[];
}
