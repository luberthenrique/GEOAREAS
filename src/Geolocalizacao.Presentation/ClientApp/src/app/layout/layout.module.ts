import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavbarLayoutComponent } from './navbar-layout/navbar-layout.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    NavbarLayoutComponent,
    SidebarComponent,
    FooterComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  exports: [
  ]
})
export class LayoutModule { }
