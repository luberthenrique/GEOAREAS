import { Component, OnInit } from '@angular/core';

import { AuthApiService } from '../../features/auth/api/auth.api';
import { Usuario } from '../../features/usuario/models/usuario.model';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css']
})
/** sidebar component*/
export class SidebarComponent implements OnInit {

  usuario: Usuario;

    /** sidebar ctor */
  constructor(private authService: AuthApiService) {

  }

  ngOnInit() {
    this.usuario = this.authService.currentUserValue;

    if (!this.usuario) {
      this.usuario = JSON.parse(localStorage.getItem('currentUser'));
    }
  }

  isAllowed(claim: string): boolean {
    return this.usuario.claims.filter(c => (c.type as string).toLowerCase() === claim).length > 0;
  }
}
