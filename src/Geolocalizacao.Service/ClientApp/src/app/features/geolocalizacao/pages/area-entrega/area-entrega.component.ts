import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { GeolocalizacaoApiService } from '../../api/geolocalizacao.api';


@Component({
  selector: 'app-area-entrega',
  templateUrl: './area-entrega.component.html',
  styleUrls: ['./area-entrega.component.css']
})
export class AreaEntregaComponent implements OnInit, AfterViewInit {

  @ViewChild('map_canvas') mapElement;

  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  geolocalizacoes = new Array();
  areasSelecionadas = new Array();

  infoWindow: google.maps.InfoWindow;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private alertService: AlertService,
    private service: GeolocalizacaoApiService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      id: [null],
      idPerfil: ['', Validators.required],
      idUsuario: [null],
      nome: ['', Validators.required],
      habilitado: [true],
      email: ['', [Validators.required, Validators.email]]      
    });

  }

  ngAfterViewInit() {
    const map = new google.maps.Map(
      this.mapElement.nativeElement as HTMLElement,
      {
        center: { lat: -21.68402401939593, lng: -43.38459126689779 },
        zoom: 13,
        mapTypeId: google.maps.MapTypeId.ROADMAP
      }
    );
    this.infoWindow = new google.maps.InfoWindow();

    var params = {
      cidade: "Juiz de Fora",
      estado: "MG"
    };
    this.service.obterPorLocal(params)
      .subscribe(data => {
        this.geolocalizacoes = data;

        this.setPolignArray(map);
      });

    
  }

  get f() { return this.form.controls; }


  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }
    this.loading = true;

  }


  setPolignArray(map: google.maps.Map) {
    this.geolocalizacoes.forEach(area => {

      const coordenadas = area.geojson.coordenadas[0];
      let coordenadasFormatada = new Array();

      coordenadas.forEach(latLong =>
      {
        const coordenadaFormatada = { lat: latLong[1], lng: latLong[0] }
        coordenadasFormatada.push(coordenadaFormatada);
      });

      let polygon = new google.maps.Polygon({
        paths: coordenadasFormatada,
        strokeColor: "#000",
        strokeOpacity: 0.9,
        strokeWeight: 2,
        fillColor: "#DDD",
        fillOpacity: 0.35,
        map: map
      });

      polygon.addListener("click", c => {
        

        console.log(this.areasSelecionadas.filter(c => c.displayName == area.displayName).length)
        
        if (this.areasSelecionadas.filter(c => c.displayName == area.displayName).length > 0) {
          polygon.set('fillColor', '#ddd');
          

          this.areasSelecionadas = this.areasSelecionadas.filter(c => c.displayName != area.displayName);
        }
        else {
          polygon.set('fillColor', '#FFC107');

          this.areasSelecionadas.push(area);
        }



        //this.infoWindow.setContent(area.displayName);
        //this.infoWindow.setPosition(c.latLng);

        //this.infoWindow.open(map);
      })
    });
  }
}
