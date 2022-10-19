import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';

import { AlertService } from 'src/app/shared/services/alert/alert.service';

import { ClienteApiService } from '../../api/cliente.api';



@Component({
  selector: 'app-new-cliente',
  templateUrl: './new-cliente.component.html',
  styleUrls: ['./new-cliente.component.css']
})
export class NewClienteComponent implements OnInit {
  loading = false;
  submitted = false;

  contatos = new Array();
  enderecos = new Array();

  constructor(
    private router: Router,
    private alertService: AlertService,
    private service: ClienteApiService,
    private toastr: ToastrService) { }

  ngOnInit() {
  }


  save(cliente) {
    this.alertService.clear();

    this.service.inserir(cliente)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Cliente adicionado com sucesso.');
          this.router.navigate(['/cliente']);
        },
        error => {
          this.loading = false;
          if (error.status === 400) {
            this.toastr.warning('Não foi possível adicionar cliente. Verifique as mensagens.');                       

            if (error.error.errors) {
              Object.keys(error.error.errors).forEach(key => {
                this.alertService.error(error.error.errors[key]);
              });
            }
            else if (error.error) {
              Object.keys(error.error).forEach(key => {
                this.alertService.error(error.error[key]);
              });
            }
            //console.log(error.error)
            //if (error.error['']) {
            //  this.alertService.error(error.error['']);
            //}
            //else if (error.error.errors) {
            //  Object.keys(error.error.errors).forEach(key => {
            //    this.alertService.error(error.error.errors[key]);
            //  });
            //}
          }
          else {
            this.toastr.error('Ocorreu um erro ao adicionar cliente.');
          }
        });
  }
}
