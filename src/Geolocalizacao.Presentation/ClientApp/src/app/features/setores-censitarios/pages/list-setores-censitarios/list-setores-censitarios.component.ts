import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { ToastrService } from 'ngx-toastr';

import { DialogDeleteComponent } from 'src/app/shared/components/dialog-delete/dialog-delete.component';
import { SetoresCensitariosApiService } from '../../api/setores-censitarios.api';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalCarregarArquivoComponent } from '../../components/modal-carregar-arquivo/modal-carregar-arquivo.component';

@Component({
  selector: 'app-list-setores-censitarios',
  templateUrl: './list-setores-censitarios.component.html',
  styleUrls: ['./list-setores-censitarios.component.css']
})
export class ListSetoresCensitariosComponent implements OnInit {

  setores = new Array();
  selecao: any;

  pag: Number = 1;
  qtdPorPagina: Number = 5;

  constructor(
    private service: SetoresCensitariosApiService,
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private alertService: AlertService,
    private modalService: NgbModal
  ) { }


  ngOnInit(): void {
    this.carregarSetoresCensitarios();
  }

  carregarSetoresCensitarios() {
    this.service.obterTodos().subscribe(data => this.setores = data);
  }

  selectRow(row){
    this.selecao = row;
  }

  carregarArquivo() {
    const modal = this.modalService.open(ModalCarregarArquivoComponent, { centered: true, size: 'lg' });

    modal.result.then((data) => {
      if (data) {
        this.carregarSetoresCensitarios();
      }
    }, (reason) => {

    });
  }
}
