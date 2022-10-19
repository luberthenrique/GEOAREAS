import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';
import { DialogDeleteComponent } from '../../../../shared/components/dialog-delete/dialog-delete.component';

import { AlertService } from '../../../../shared/services/alert/alert.service';
import { ClienteApiService } from '../../api/cliente.api';

@Component({
  selector: 'app-gerenciamento-api',
  templateUrl: './gerenciamento-api.component.html',
  styleUrls: ['./gerenciamento-api.component.css']
})
/** formuario-cliente component*/
export class GerenciamentoApiComponent implements OnInit {
  form: FormGroup;
  submitted = false;
  
  @Output() salvar = new EventEmitter<any>();
  @Input() public editar: boolean = false;

  @Input() apis: any[] = new Array();
  idCliente;

  /** formuario-cliente ctor */
  constructor(
    private service: ClienteApiService,
    private formBuilder: FormBuilder,
    private router: Router,
    private alertService: AlertService,
    private toastr: ToastrService,
    private modalService: NgbModal) {

  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      id: [null],
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(300)]]
    });

    this.idCliente = window.localStorage.getItem('editClienteId');

    this.obterApis();
    
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    const api = {
      id: this.f.id.value,
      idCliente: this.idCliente,
      nome: this.f.nome.value
    };

    this.service.cadastrarApi(this.idCliente, api)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('API adicionada com sucesso.');
          this.obterApis();
        },
        error => {
          this.obterApis();
          if (error.status === 400) {
            this.toastr.warning('Não foi possível adicionar API. Verifique as mensagens.');

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
          }
          else {
            this.toastr.error('Ocorreu um erro ao adicionar API.');
          }
        });
  }

  private obterApis() {
    this.service.obterApis(this.idCliente).subscribe(data => {
      this.apis = data
      console.log(this.apis)
    });
    
  }

  deleteApi(api): void {
    const modal = this.modalService.open(DialogDeleteComponent, { centered: true });
    modal.componentInstance.data = { titulo: 'API', identificacao: api.nome, id: api.id };

    modal.result.then((data) => {
      this.service.deletarApi(api.id)
        .pipe(first())
        .subscribe(
          () => {
            this.toastr.success('API excluída com sucesso.');
            this.ngOnInit();
          },
          error => {
            if (error.status === 400) {
              this.toastr.warning('Não foi possível excluir API. Verifique as mensagens.');
              this.alertService.error(error.error['']);
            }
            else {
              this.toastr.error('Ocorreu um erro ao excluir API.');
            }

          });

    }, (reason) => {
      // on dismiss
    });
  }
}
