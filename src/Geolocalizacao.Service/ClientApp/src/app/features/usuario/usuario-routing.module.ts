import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EditUsuarioComponent } from './pages/edit-usuario/edit-usuario.component';
import { ListUsuarioComponent } from './pages/list-usuario/list-usuario.component';
import { NewUsuarioComponent } from './pages/new-usuario/new-usuario.component';

import { AuthGuard } from '../../core/guards';
import { AuthGuardChild } from 'src/app/core/guards/auth.guard.child';
import { ProfileUsuarioComponent } from './pages/profile-usuario/profile-usuario.component';
import { LayoutComponent } from '../../layout/layout/layout.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: ListUsuarioComponent }
    ],
    canActivate: [
      AuthGuardChild
    ],
    data: {
      allowedRoles: ['Administrador']
    }
  },
  {
    path: 'list',
    component: LayoutComponent,
    children: [
      { path: '', component: ListUsuarioComponent }
    ],
    canActivate: [
      AuthGuardChild
    ],
    data: {
      allowedRoles: ['Administrador']
    }
  },
  {
    path: 'new',
    component: LayoutComponent,
    children: [
      { path: '', component: NewUsuarioComponent }
    ],
    canActivate: [
      AuthGuardChild
    ],
    data: {
      allowedRoles: ['Administrador']
    }
  },
  {
    path: 'edit',
    component: LayoutComponent,
    children: [
      { path: '', component: EditUsuarioComponent }
    ],
    canActivate: [
      AuthGuardChild
    ],
    data: {
      allowedRoles: ['Administrador']
    }
  },
  {
    path: 'profile',
    component: LayoutComponent,
    children: [
      { path: '', component: ProfileUsuarioComponent }
    ],
    data: {
      allowedRoles: ['Administrador', 'Revisor', 'Cliente']
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class UsuarioRoutingModule { }
