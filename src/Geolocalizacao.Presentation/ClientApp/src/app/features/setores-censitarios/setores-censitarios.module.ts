import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'src/app/shared/shared.module';
import { SetoresCensitariosRoutingModule } from './setores-censitarios-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ListSetoresCensitariosComponent } from './pages/list-setores-censitarios/list-setores-censitarios.component';
import { ModalCarregarArquivoComponent } from './components/modal-carregar-arquivo/modal-carregar-arquivo.component';

@NgModule({
  declarations: [
    ListSetoresCensitariosComponent,
    ModalCarregarArquivoComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    SetoresCensitariosRoutingModule    
  ]
})
export class SetoresCensitariosModule { }
