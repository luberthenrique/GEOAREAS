import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SharedModule } from 'src/app/shared/shared.module';

import { NewAreaEntregaComponent } from './pages/new-area-entrega/new-area-entrega.component';
import { EditAreaEntregaComponent } from './pages/edit-area-entrega/edit-area-entrega.component';
import { ListGeolocalizacaoComponent } from './pages/list-geolocalizacao/list-geolocalizacao.component';
import { GeolocalizacaoRoutingModule } from './geolocalizacao-routing.module';


@NgModule({
  declarations: [
    ListGeolocalizacaoComponent,
    NewAreaEntregaComponent,
    EditAreaEntregaComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    GeolocalizacaoRoutingModule
  ]
})
export class GeolocalizacaoModule { }
