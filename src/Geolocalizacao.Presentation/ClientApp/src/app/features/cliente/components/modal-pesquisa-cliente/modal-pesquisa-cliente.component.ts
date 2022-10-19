import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, distinctUntilChanged, first } from 'rxjs/operators';

import { AlertService } from '../../../../shared/services/alert/alert.service';
import { ClienteApiService } from '../../api/cliente.api';

@Component({
  selector: 'app-modal-pesquisa-cliente',
  templateUrl: './modal-pesquisa-cliente.component.html',
  styleUrls: ['./modal-pesquisa-cliente.component.css']
})
/** formuario-cliente component*/
export class ModalPesquisaClienteComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;

  clientes = new Array();
  selecao: any;

  pag: Number = 1;
  qtdPorPagina: Number = 5;

  /** formuario-cliente ctor */
  constructor(
    private formBuilder: FormBuilder,
    public activeModal: NgbActiveModal,
    private alertService: AlertService,
    private service: ClienteApiService,
    private toastr: ToastrService) {

  }
  get f() { return this.form.controls; }

  ngOnInit() {
    this.form = this.formBuilder.group({
      texto: ['']
    });

    this.f.texto.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged()
    ).subscribe(value => {
      if (value.length >= 3) {
        this.pesquisarCliente(value)
      }
      else {
        this.clientes = new Array();
      }      
    });
  }

  pesquisarCliente(texto) {
    this.service.pesquisar(texto).subscribe(data => this.clientes = data);
  }

  selectRow(row) {
    this.selecao = row;
  }
  confirmarSelecao() {
    this.activeModal.close(this.selecao);
  }

}
