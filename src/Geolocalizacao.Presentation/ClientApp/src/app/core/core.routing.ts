import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from './guards';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('../features/home/home.module').then(x => x.HomeModule),
    canActivate: [
      AuthGuard
    ]
  },
  {
    path: 'auth',
    loadChildren: () => import('../features/auth/auth.module').then(x => x.AuthModule)
  },
  {
    path: 'cliente',
    loadChildren: () => import('../features/cliente/cliente.module').then(x => x.ClienteModule),
    canActivate: [
      AuthGuard
    ]
  },
  {
    path: 'perfilusuario',
    loadChildren: () => import('../features/perfil-usuario/perfil-usuario.module').then(x => x.PerfilUsuarioModule),
    canActivate: [
      AuthGuard
    ]
  },
  {
    path: 'setores-censitarios',
    loadChildren: () => import('../features/setores-censitarios/setores-censitarios.module').then(x => x.SetoresCensitariosModule),
    canActivate: [
      AuthGuard
    ]
  },
  {
    path: 'usuario',
    loadChildren: () => import('../features/usuario/usuario.module').then(x => x.UsuarioModule),
    canActivate: [
      AuthGuard
    ]
  }  
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
