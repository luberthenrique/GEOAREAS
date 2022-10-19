import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import { GroupByPipe } from './group-by-pipe.component';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    GroupByPipe,
  ],
  exports: [
    GroupByPipe,
  ],
})
export class GroupByPipeModule { }
