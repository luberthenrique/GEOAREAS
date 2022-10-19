import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from '../../layout/layout/layout.component';
import { AuthGuardChild } from 'src/app/core/guards/auth.guard.child';
import { ListSetoresCensitariosComponent } from './pages/list-setores-censitarios/list-setores-censitarios.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: ListSetoresCensitariosComponent }
    ],
    canActivate: [
      AuthGuardChild
    ]
  },
  {
    path: 'list',
    component: LayoutComponent,
    children: [
      { path: '', component: ListSetoresCensitariosComponent }
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

export class SetoresCensitariosRoutingModule { }
