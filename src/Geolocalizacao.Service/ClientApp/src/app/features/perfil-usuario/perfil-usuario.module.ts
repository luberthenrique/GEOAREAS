import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PerfilUsuarioRoutingModule } from './perfil-usuario.routing.module';
import { ListPerfilUsuarioComponent } from './pages/list-perfil-usuario/list-perfil-usuario.component';
import { NewPerfilUsuarioComponent } from './pages/new-perfil-usuario/new-perfil-usuario.component';
import { EditPerfilUsuarioComponent } from './pages/edit-perfil-usuario/edit-perfil-usuario.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ListPerfilUsuarioComponent,
    NewPerfilUsuarioComponent,
    EditPerfilUsuarioComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    PerfilUsuarioRoutingModule
  ]
})
export class PerfilUsuarioModule { }
