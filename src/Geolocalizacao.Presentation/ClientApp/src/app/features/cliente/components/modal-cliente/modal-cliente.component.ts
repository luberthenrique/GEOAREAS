import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';

import { AlertService } from '../../../../shared/services/alert/alert.service';
import { ClienteApiService } from '../../api/cliente.api';

@Component({
  selector: 'app-modal-cliente',
  templateUrl: './modal-cliente.component.html',
  styleUrls: ['./modal-cliente.component.css']
})
/** formuario-cliente component*/
export class ModalClienteComponent implements OnInit {
  loading = false;
  submitted = false;

  /** formuario-cliente ctor */
  constructor(
    public activeModal: NgbActiveModal,
    private alertService: AlertService,
    private service: ClienteApiService,
    private toastr: ToastrService) {

  }

  ngOnInit() {}

  save(cliente) {
    this.service.inserir(cliente)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Cliente adicionado com sucesso.');
          this.activeModal.close(cliente);
        },
        error => {
          this.loading = false;
          if (error.status === 400) {
            this.toastr.warning('Não foi possível adicionar cliente. Verifique as mensagens.');
            if (error.error['']) {
              this.alertService.error(error.error['']);
            }
            else if (error.error.errors) {
              Object.keys(error.error.errors).forEach(key => {
                this.alertService.error(error.error.errors[key]);
              });
            }
          }
          else {
            this.toastr.error('Ocorreu um erro ao adicionar cliente.');
          }
        });
  }
}
