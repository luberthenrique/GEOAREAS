import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';

import { AlertService } from 'src/app/shared/services/alert/alert.service';

import { ClienteApiService } from '../../api/cliente.api';

@Component({
  selector: 'app-edit-cliente',
  templateUrl: './edit-cliente.component.html',
  styleUrls: ['./edit-cliente.component.css']
})
export class EditClienteComponent implements OnInit {
  loading = false;
  submitted = false;

  contatos = new Array();
  enderecos = new Array();

  constructor(
    private router: Router,
    private alertService: AlertService,
    private service: ClienteApiService,
    private toastr: ToastrService) { }

  ngOnInit() { }

  save(cliente) {
    this.service.alterar(cliente.id, cliente)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Cliente atualizado com sucesso.');
          this.router.navigate(['/cliente']);
        },
        error => {
          this.loading = false;
          if (error.status === 400) {
            this.toastr.warning('Não foi possível atualizar cliente. Verifique as mensagens.');
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
            this.toastr.error('Ocorreu um erro ao atualizar cliente.');
          }
        });
  }
}
