import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { CoordenadasEnderecoService } from '../../../../shared/services/coordenadas-endereco/coordenadas-endereco.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild('map_canvas') mapElement;

  map: google.maps.Map;
  localizacoesSelecionadas = new Array();
  coordenadas = [];
  constructor(private coordenadasEnderecoService: CoordenadasEnderecoService) { }

  ngOnInit(): void {

  }

  //ngAfterViewInit() {
  //  const infowindow = new google.maps.InfoWindow();

  //  const map = new google.maps.Map(
  //    this.mapElement.nativeElement as HTMLElement,
  //    {
  //      center: { lat: -21.68402401939593, lng: -43.38459126689779 },
  //      zoom: 13,
  //      mapTypeId: google.maps.MapTypeId.ROADMAP
  //    }
  //  );

  //  this.buscarCordenadas('Santa Terezinha, Juiz de Fora', map);
  //  //this.buscarCordenadas('Grajaú, Juiz de Fora', map);
  //  this.buscarCordenadas('São Benedito, Juiz de Fora', map);
  //  //this.buscarCordenadas('Vitorino Braga, Juiz de Fora', map);
  //  this.buscarCordenadas('Linhares, Juiz de Fora', map);

  //}

  //placeToAddress(place) {
  //  var address = {
  //    StreetNumber: { long_name: '', short_name: '' },
  //    StreetName: { long_name: '', short_name: '' },
  //    DistrictName: { long_name: '', short_name: '' },
  //    City: { long_name: '', short_name: '' },
  //    State: { long_name: '', short_name: '' },
  //    Zip: { long_name: '', short_name: '' },
  //    Country: { long_name: '', short_name: '' }
  //  };

  //  place.address_components.forEach(function (c) {
  //    switch (c.types[0]) {
  //      case 'street_number':
  //        address.StreetNumber = c;
  //        break;
  //      case 'route':
  //        address.StreetName = c;
  //        break;
  //      case 'administrative_area_level_2': case 'locality':
  //        address.City = c;
  //        break;
  //      case 'administrative_area_level_1':
  //        address.State = c;
  //        break;
  //      case 'political':
  //        address.DistrictName = c;
  //        break;
  //      case 'sublocality_level_1':
  //        address.DistrictName = c;
  //        break;
  //      case 'postal_code':
  //        address.Zip = c;
  //        break;
  //      case 'country':
  //        address.Country = c;
  //        break;
  //      /*
  //      *   . . . 
  //      */
  //    }
  //  });

  //  return address;
  //}


  //buscarCordenadas(endereco: string, map: google.maps.Map) {
  //  this.coordenadasEnderecoService.consultarEndereco(endereco).subscribe(
  //    data => {
  //      if (data.length > 0) {
  //        const localizacao = {
  //          nome: endereco,
  //          coordenadas: data[0].geojson.coordinates
  //        }

  //        this.localizacoesSelecionadas.push(localizacao);          
  //        let coordenadas;

  //        this.localizacoesSelecionadas.forEach(local => {
  //          coordenadas = new Array();
            
  //          if (local.coordenadas[0].length > 1) {
  //            local.coordenadas[0].forEach(coodenada => {                
  //              const coord = { lat: coodenada[1], lng: coodenada[0] };
  //              coordenadas.push(coord);
  //            });
  //          }
  //          else {
  //            const coord = { lat: local.coordenadas[1], lng: local.coordenadas[0] };
  //            coordenadas.push(coord);
  //          }
  //        });
  //        this.coordenadas.push(coordenadas);

  //        if (endereco == 'Linhares, Juiz de Fora') {
  //          this.setPolignArray(map);
  //        }

          
  //      }

  //    }
  //  );
  //}

  //setPolignArray(map: google.maps.Map) {    
  //  this.coordenadas.forEach(coord => {

  //    let polygon = new google.maps.Polygon({
  //      paths: coord.slice(0, coord.length -1),
  //      strokeColor: "#000",
  //      strokeOpacity: 0.8,
  //      strokeWeight: 2,
  //      fillColor: "#FFC107",
  //      fillOpacity: 0.35,
  //      map: map,
  //    });

  //    polygon.addListener("click", c => {
  //      polygon.setMap(null);

  //      polygon = new google.maps.Polygon({
  //        paths: coord.slice(0, coord.length - 1),
  //        strokeColor: "#000",
  //        strokeOpacity: 0.8,
  //        strokeWeight: 2,
  //        fillColor: "#DDD",
  //        fillOpacity: 0.35,
  //        map: map,
  //      });
  //    })
  //  });
    
    
    


  //}
}
