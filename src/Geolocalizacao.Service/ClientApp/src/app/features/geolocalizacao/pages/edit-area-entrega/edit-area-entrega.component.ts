import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { AreaEntregaApiService } from '../../api/area-entrega.api';
import { GeolocalizacaoApiService } from '../../api/geolocalizacao.api';
import { AreaEntregaGeolocalizacaoCoordenada } from '../../models/area-entrega-geolocalizacao-coordenada.model';
import { AreaEntregaGeolocalizacao } from '../../models/area-entrega-geolocalizacao.model';
import { AreaEntrega } from '../../models/area-entrega.model';
import { Localizacao } from '../../models/localizacao.model';


@Component({
  selector: 'app-edit-area-entrega',
  templateUrl: './edit-area-entrega.component.html',
  styleUrls: ['./edit-area-entrega.component.css']
})
export class EditAreaEntregaComponent implements OnInit, AfterViewInit {

  @ViewChild('map_canvas') mapElement;

  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  geolocalizacoes = new Array();
  areasSelecionadas: AreaEntregaGeolocalizacao[] = new Array();

  infoWindow: google.maps.InfoWindow;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private alertService: AlertService,
    private service: AreaEntregaApiService,
    private geolocalizacaoService: GeolocalizacaoApiService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      id: [null],
      nome: ['', Validators.required],
      cor: ['#ff0000', Validators.required],
      valor: [0, Validators.required],
      tempo: [0, Validators.required],
    });

    const areaId = window.localStorage.getItem('editArea-EntregaId');
    this.service.obterPorId(areaId)
      .subscribe(data => {
        this.form.setValue({
          id: data.id,
          nome: data.nome,
          cor: data.cor,
          valor: data.valor,
          tempo: data.tempo
        });

        this.areasSelecionadas = data.areaEntregaGeolocalizacao;        
      });

    this.f.cor.valueChanges.subscribe(x => {
      if (x.length == 7) {
        this.carregarMapa();
      }
    });
  }

  ngAfterViewInit() {    
    this.carregarMapa();    
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

    const areaEntrega = {
      id: this.f.id.value,
      nome: this.f.nome.value,
      cor: this.f.cor.value,
      valor: this.f.valor.value,
      tempo: this.f.tempo.value,
      areaEntregaGeolocalizacao: this.areasSelecionadas,
    };

    this.service.alterar(this.f.id.value, areaEntrega)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Área de entrega adicionada com sucesso.');
          this.router.navigate(['/area-entrega']);
        },
        error => {
          this.loading = false;
          if (error.status === 400) {
            this.toastr.warning('Não foi possível adicionar área de entrega. Verifique as mensagens.');
            this.alertService.error(error.error['']);
          }
          else {
            this.toastr.error('Ocorreu um erro ao adicionar área de entrega.');
          }
        });

  }

  carregarMapa() {
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
    this.geolocalizacaoService.obterPorLocal(params)
      .subscribe(data => {
        this.geolocalizacoes = data;
        this.setPolignArray(map);
      });
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

      let cor = '#FFF';
      if (this.areasSelecionadas.find(c => c.displayName == area.displayName)) {
        cor = this.f.cor.value;
      }

      

      let polygon = new google.maps.Polygon({
        paths: coordenadasFormatada,
        strokeColor: "#000",
        strokeOpacity: 0.9,
        strokeWeight: 2,
        fillColor: cor,
        fillOpacity: 0.7,
        map: map
      });

      polygon.addListener("click", c => {
                
        if (this.areasSelecionadas.filter(c => c.displayName == area.displayName).length > 0) {
          polygon.set('fillColor', '#ddd');
          

          this.areasSelecionadas = this.areasSelecionadas.filter(c => c.displayName != area.displayName);
        }
        else {
          polygon.set('fillColor', this.f.cor.value);

          const coordenadas: AreaEntregaGeolocalizacaoCoordenada[] = new Array();
          
          area.geojson.coordenadas[0].forEach(d => {
            coordenadas.push(new AreaEntregaGeolocalizacaoCoordenada({
              latitude: d[1],
              longitude: d[0]
            }));
          });
          
          const geolocalizacao = new AreaEntregaGeolocalizacao(
            {
              idPlace: area.idPlace,
              latitude: area.lat,
              longitude: area.lon,
              displayName: area.displayName,
              area: area.area,
              cidade: area.cidade,
              uF: area.uf,
              areaEntregaGeolocalizacaoCoordenada: coordenadas
            });
          console.log(geolocalizacao)
          this.areasSelecionadas.push(geolocalizacao);
        }

        //this.infoWindow.setContent(area.displayName);
        //this.infoWindow.setPosition(c.latLng);

        //this.infoWindow.open(map);
      })
    });
  }
}
