import { Component, Inject, Input} from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

export interface DialogData {
  id: number;
  titulo: string;
  identificacao: string;
}

@Component({
  selector: 'app-dialog-delete',
  templateUrl: './dialog-delete.component.html',
  styleUrls: ['./dialog-delete.component.css']
})
export class DialogDeleteComponent {
  @Input() data: any;

  constructor(public activeModal: NgbActiveModal) {}

  onNoClick(): void {
  }

}
