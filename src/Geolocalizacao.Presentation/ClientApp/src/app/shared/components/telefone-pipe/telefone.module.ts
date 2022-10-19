import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import { TelefonePipe } from './telefone.pipe';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    TelefonePipe,
  ],
  exports: [
    TelefonePipe,
  ],
})
export class TelefonePipeModule { }
