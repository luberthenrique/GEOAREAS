import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardChild } from './core/guards/auth.guard.child';

import { AccessDeniedComponent } from './layout/error/access-denied/access-denied.component';
import { ForbiddenComponent } from './layout/error/forbidden/forbidden.component';
import { NotFoundComponent } from './layout/error/not-found/not-found.component';

const routes: Routes = [
  {
    path: 'error/accessdenied',
    component: AccessDeniedComponent
  },
  {
    path: 'error/forbidden',
    component: ForbiddenComponent
  },
  {
    path: 'error/notfound',
    component: NotFoundComponent
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
