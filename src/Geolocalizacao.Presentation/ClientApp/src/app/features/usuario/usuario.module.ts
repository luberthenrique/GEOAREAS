import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListUsuarioComponent } from './pages/list-usuario/list-usuario.component';
import { NewUsuarioComponent } from './pages/new-usuario/new-usuario.component';
import { EditUsuarioComponent } from './pages/edit-usuario/edit-usuario.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UsuarioRoutingModule } from './usuario-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProfileUsuarioComponent } from './pages/profile-usuario/profile-usuario.component';


@NgModule({
  declarations: [
    ListUsuarioComponent,
    NewUsuarioComponent,
    EditUsuarioComponent,
    ProfileUsuarioComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    UsuarioRoutingModule
  ]
})
export class UsuarioModule { }
