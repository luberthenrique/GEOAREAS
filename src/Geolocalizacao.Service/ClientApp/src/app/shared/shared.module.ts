import { LoaderComponent } from './components/loader/loader.component';
import { AlertComponent } from './components/alert/alert.component';
import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorMsgComponent } from './components/error/error-msg.component';
import { BotoesGridComponent } from './components/botoes-grid/botoes-grid.component';
import { DialogDeleteComponent } from './components/dialog-delete/dialog-delete.component';
import { CpfCnpjPipeModule } from './components/cpf-cnpj-pipe/cpf-cnpj.module';
import { NgxSummernoteModule } from 'ngx-summernote';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SectionComponent } from './components/section/section.component';
import { TelefonePipeModule } from './components/telefone-pipe/telefone.module';
import { NgxMaskModule } from 'ngx-mask';

@NgModule({
  imports: [
    CommonModule,
    CpfCnpjPipeModule,
    NgxSummernoteModule,
    NgxPaginationModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    TelefonePipeModule,
    NgxMaskModule.forRoot()
  ],
  declarations: [
    AlertComponent,
    LoaderComponent,
    ErrorMsgComponent,
    DialogDeleteComponent,
    BotoesGridComponent,
    SectionComponent

  ],
  exports: [
    // Componentes
    AlertComponent,
    BotoesGridComponent,
    CpfCnpjPipeModule,
    SectionComponent,
    NgxSummernoteModule,
    NgxPaginationModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    TelefonePipeModule,
    NgxMaskModule
  ],
  providers: [
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    return {
      ngModule: SharedModule
    };
  }
}
