import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthApiService } from '../../features/auth/api/auth.api';
import { Usuario } from '../../features/usuario/models/usuario.model';

@Component({
    selector: 'app-navbar-layout',
    templateUrl: './navbar-layout.component.html',
    styleUrls: ['./navbar-layout.component.css']
})
/** navbar-layout component*/
export class NavbarLayoutComponent implements OnInit{
    /** navbar-layout ctor */
  constructor(
    private authService: AuthApiService,
    private router: Router  ) {

  }
  usuario: Usuario;

  ngOnInit() {
    this.usuario = this.authService.currentUserValue;

    if (!this.usuario) {
      this.usuario = JSON.parse(localStorage.getItem('currentUser'));
    }
  }

  logoff() {

    //this.socialAuthService.signOut();

    this.authService.logout().subscribe(
      () => {
        this.router.navigate(['auth/login']);
      },
      (error) => {
        console.log(error);
      });
  }

  login() {
    this.router.navigate(['auth/login']);
  }
}
