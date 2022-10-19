import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';

import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { DialogDeleteComponent } from '../../../../shared/components/dialog-delete/dialog-delete.component';
import { AreaEntregaApiService } from '../../api/area-entrega.api';
import { GeolocalizacaoApiService } from '../../api/geolocalizacao.api';
import { AreaEntregaGeolocalizacao } from '../../models/area-entrega-geolocalizacao.model';
import { AreaEntrega } from '../../models/area-entrega.model';


@Component({
  selector: 'app-list-geolocalizacao',
  templateUrl: './list-geolocalizacao.component.html',
  styleUrls: ['./list-geolocalizacao.component.css']
})
export class ListGeolocalizacaoComponent implements OnInit, AfterViewInit {

  @ViewChild('map_canvas') mapElement;

  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  selecao: any;

  areasEntrega: AreaEntrega[] = new Array();
  areasSelecionadas = new Array();
  geolocalizacoes = new Array();

  infoWindow: google.maps.InfoWindow;

  constructor(
    private service: GeolocalizacaoApiService,
    private router: Router,
    private toastr: ToastrService,
    private alertService: AlertService,
    private modalService: NgbModal
    ) { }

  ngOnInit() {

  }

  ngAfterViewInit() {
    this.carregarMapa();
  }

  get f() { return this.form.controls; }

  selectRow(row) {
    this.selecao = row;
  }

  edit(row) {
    window.localStorage.removeItem('editAreaEntregaId');
    window.localStorage.setItem('editAreaEntregaId', row.id.toString());
    this.router.navigate(['area-entrega/edit']);
  }

  confirmDelete(): void {
    const modal = this.modalService.open(DialogDeleteComponent, { centered: true });
    modal.componentInstance.data = { titulo: 'usuário', identificacao: this.selecao.nome, id: this.selecao.id };

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
    this.service.obterPorLocal(params)
      .subscribe(data => {
        this.geolocalizacoes = data;

        this.setPolignArray(map);
      });
  }

  setPolignArray(map: google.maps.Map) {
    this.geolocalizacoes.forEach(area => {

      const coordenadas = area.geojson.coordenadas[0];
      let coordenadasFormatada = new Array();

      coordenadas.forEach(latLong => {
        const coordenadaFormatada = { lat: latLong[1], lng: latLong[0] }
        coordenadasFormatada.push(coordenadaFormatada);
      });

      let cor = '#FFF';
      if (this.areasSelecionadas.find(c => c.displayName == area.displayName)) {
        cor = this.f.cor.value;
        console.log(cor);
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

        //this.infoWindow.setContent(area.displayName);
        //this.infoWindow.setPosition(c.latLng);

        //this.infoWindow.open(map);
      })
    });
  }
}
