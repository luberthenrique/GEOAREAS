import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'src/app/shared/shared.module';
import { ClienteRoutingModule } from './cliente-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ListClienteComponent } from './pages/list-cliente/list-cliente.component';
import { NewClienteComponent } from './pages/new-cliente/new-cliente.component';
import { EditClienteComponent } from './pages/edit-cliente/edit-cliente.component';
import { FormularioComponent } from './components/formulario/formulario.component';
import { ModalClienteComponent } from './components/modal-cliente/modal-cliente.component';
import { ModalPesquisaClienteComponent } from './components/modal-pesquisa-cliente/modal-pesquisa-cliente.component';
import { GerenciamentoApiComponent } from './components/gerenciamento-api/gerenciamento-api.component';


@NgModule({
  declarations: [
    ListClienteComponent,
    NewClienteComponent,
    EditClienteComponent,
    FormularioComponent,
    ModalClienteComponent,
    ModalPesquisaClienteComponent,
    GerenciamentoApiComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ClienteRoutingModule    
  ],
  exports: [
    ModalClienteComponent,
    ModalPesquisaClienteComponent
  ]
})
export class ClienteModule { }
