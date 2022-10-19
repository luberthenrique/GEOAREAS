import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditClienteComponent } from './pages/edit-cliente/edit-cliente.component';
import { NewClienteComponent } from './pages/new-cliente/new-cliente.component';

import { LayoutComponent } from '../../layout/layout/layout.component';
import { AuthGuardChild } from 'src/app/core/guards/auth.guard.child';
import { ListClienteComponent } from './pages/list-cliente/list-cliente.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: ListClienteComponent }
    ],
    canActivate: [
      AuthGuardChild
    ]
  },
  {
    path: 'list',
    component: LayoutComponent,
    children: [
      { path: '', component: ListClienteComponent }
    ],
    canActivate: [
      AuthGuardChild
    ]
  },
  {
    path: 'new',
    component: LayoutComponent,
    children: [
      { path: '', component: NewClienteComponent }
    ],
    canActivate: [
      AuthGuardChild
    ]
  },
  {
    path: 'edit',
    component: LayoutComponent,
    children: [
      { path: '', component: EditClienteComponent }
    ],
    canActivate: [
      AuthGuardChild
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ClienteRoutingModule { }
