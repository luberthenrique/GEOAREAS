import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { ToastrService } from 'ngx-toastr';

import { DialogDeleteComponent } from 'src/app/shared/components/dialog-delete/dialog-delete.component';
import { ClienteApiService } from '../../api/cliente.api';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-list-cliente',
  templateUrl: './list-cliente.component.html',
  styleUrls: ['./list-cliente.component.css']
})
export class ListClienteComponent implements OnInit {
  clientes: any;
  clienteAtual = null;
  selecao: any;

  pag: Number = 1;
  qtdPorPagina: Number = 5;

  constructor(
    private service: ClienteApiService,
    private router: Router,
    private toastr: ToastrService,
    private alertService: AlertService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.carregarClientes();
  }

  private carregarClientes() {
    this.service.obterTodos()
      .subscribe(data => {
        this.clientes = data;
      });
  }

  selectRow(row){
    this.selecao = row;
  }

  edit(row){
    window.localStorage.removeItem('editClienteId');
    window.localStorage.setItem('editClienteId', row.id.toString());
    this.router.navigate(['cliente/edit']);
  }

  confirmDelete(): void {
    const modal = this.modalService.open(DialogDeleteComponent, { centered: true });
    modal.componentInstance.data = { titulo: 'cliente', identificacao: this.selecao.nome, id: this.selecao.id };

    modal.result.then((data) => {
      this.service.deletar(this.selecao.id)
        .pipe(first())
        .subscribe(
          () => {
            this.toastr.success('Cliente excluído com sucesso.');
            this.ngOnInit();
          },
          error => {
            if (error.status === 400) {
              this.toastr.warning('Não foi possível excluir cliente. Verifique as mensagens.');
              this.alertService.error(error.error['']);
            }
            else {
              this.toastr.error('Ocorreu um erro ao excluir cliente.');
            }

          });

    }, (reason) => {
      // on dismiss
    });  
  }

}
