import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from '../../layout/layout/layout.component';
import { EditAreaEntregaComponent } from './pages/edit-area-entrega/edit-area-entrega.component';
import { ListGeolocalizacaoComponent } from './pages/list-geolocalizacao/list-geolocalizacao.component';
import { NewAreaEntregaComponent } from './pages/new-area-entrega/new-area-entrega.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: ListGeolocalizacaoComponent }
    ],
    canActivate: [
      //AuthGuardChild
    ],
    data: {
      allowedRoles: ['Administrador']
    }
  },
  {
    path: 'new',
    component: LayoutComponent,
    children: [
      { path: '', component: NewAreaEntregaComponent }
    ],
    canActivate: [
      //AuthGuardChild
    ],
    data: {
      allowedRoles: ['Administrador']
    }
  },
  {
    path: 'edit',
    component: LayoutComponent,
    children: [
      { path: '', component: EditAreaEntregaComponent }
    ],
    canActivate: [
      //AuthGuardChild
    ],
    data: {
      allowedRoles: ['Administrador']
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class GeolocalizacaoRoutingModule { }
