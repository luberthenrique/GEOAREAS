import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { ToastrService } from 'ngx-toastr';

import { DialogDeleteComponent } from 'src/app/shared/components/dialog-delete/dialog-delete.component';
import { UsuarioApiService } from '../../api/usuario.api';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-list-usuario',
  templateUrl: './list-usuario.component.html',
  styleUrls: ['./list-usuario.component.css']
})
export class ListUsuarioComponent implements OnInit {
  usuarios: any;
  usuarioAtual = null;
  selecao: any;
  dataSource: any;

  pag: Number = 1;
  qtdPorPagina: Number = 5;

  constructor(
    private service: UsuarioApiService,
    private router: Router,
    private toastr: ToastrService,
    private alertService: AlertService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.selecao = null;

    this.usuarios = [];
    this.service.obterTodos()
      .subscribe( data => {
        this.usuarios = data;
      });
  }

  selectRow(row){
    this.selecao = row;
  }

  edit(row){
    window.localStorage.removeItem('editUsuarioId');
    window.localStorage.setItem('editUsuarioId', row.id.toString());
    this.router.navigate(['usuario/edit']);
  }

  confirmDelete(): void {
    const modal = this.modalService.open(DialogDeleteComponent, { centered: true });
    modal.componentInstance.data = { titulo: 'usuário', identificacao: this.selecao.nome, id: this.selecao.id };

    modal.result.then((data) => {
      this.service.deletar(this.selecao.id)
        .pipe(first())
        .subscribe(
          () => {
            this.toastr.success('Usuário excluído com sucesso.');
            this.ngOnInit();
          },
          error => {
              if (error.status === 400){
                this.toastr.warning('Não foi possível excluir usuário. Verifique as mensagens.');
                this.alertService.error(error.error['']);
              }
              else{
                this.toastr.error('Ocorreu um erro ao excluir usuário.');
              }

          });

    }, (reason) => {
      // on dismiss
    });    
  }
}
