import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavbarLayoutComponent } from './navbar-layout/navbar-layout.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    NavbarLayoutComponent,
    SidebarComponent,
    FooterComponent    
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavbarLayoutComponent,
    SidebarComponent,
    FooterComponent
  ]
})
export class LayoutModule { }
