import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, LOCALE_ID, NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ToastrModule } from 'ngx-toastr';
import { AccessDeniedComponent } from '../layout/error/access-denied/access-denied.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { LoaderInterceptor } from './interceptors/loader.interceptor';

import { CommonModule, registerLocaleData } from '@angular/common';
import { NotFoundComponent } from '../layout/error/not-found/not-found.component';
import { CoreRoutingModule } from './core.routing';
import { MenuComponent } from '../layout/menu/menu.component';
import { ForbiddenComponent } from '../layout/error/forbidden/forbidden.component';
import { CacheInterceptor } from './interceptors/cache.interceptor';
import localePt from '@angular/common/locales/pt';
import { LayoutModule } from '../layout/layout.module';
import { LayoutComponent } from '../layout/layout/layout.component';
import { SharedModule } from '../shared/shared.module';

registerLocaleData(localePt);

@NgModule({
  imports: [
    CommonModule,
    RouterModule, 
    HttpClientModule,
    ToastrModule.forRoot(),
    NgbModule,
    SharedModule,
    LayoutModule,
    CoreRoutingModule
  ],
  declarations: [
    AccessDeniedComponent,
    NotFoundComponent,
    ForbiddenComponent,
    //LayoutComponent,
    MenuComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
    //{ provide: HTTP_INTERCEPTORS, useClass: CacheInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'pt-BR' },
  ]
})
export class CoreModule {
  constructor(){
  }
 }

