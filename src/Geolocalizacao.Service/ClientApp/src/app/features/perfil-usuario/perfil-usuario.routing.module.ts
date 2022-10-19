import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ListPerfilUsuarioComponent } from './pages/list-perfil-usuario/list-perfil-usuario.component';
import { NewPerfilUsuarioComponent } from './pages/new-perfil-usuario/new-perfil-usuario.component';
import { EditPerfilUsuarioComponent } from './pages/edit-perfil-usuario/edit-perfil-usuario.component';
import { LayoutComponent } from 'src/app/layout/layout/layout.component';
import { AuthGuard } from 'src/app/core/guards';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: ListPerfilUsuarioComponent }
    ]
  },
  {
    path: 'index',
    component: LayoutComponent,
    children: [
      { path: '', component: ListPerfilUsuarioComponent }
    ]
  },
  {
  path: 'new',
  component: LayoutComponent,
  children: [
    { path: '', component: NewPerfilUsuarioComponent }
  ],
  canActivate: [
    AuthGuard
  ]
  },
  {
    path: 'edit',
    component: LayoutComponent,
    children: [
      { path: '', component: EditPerfilUsuarioComponent }
    ],
    canActivate: [
      AuthGuard
    ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PerfilUsuarioRoutingModule { }
