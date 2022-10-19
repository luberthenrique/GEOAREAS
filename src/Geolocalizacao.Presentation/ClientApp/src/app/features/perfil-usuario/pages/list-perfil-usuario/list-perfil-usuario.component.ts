import { Router } from '@angular/router';
import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
import { DialogDeleteComponent } from 'src/app/shared/components/dialog-delete/dialog-delete.component';
import { first } from 'rxjs/operators';
import { PerfilUsuarioApiService } from '../../api/perfil-usuario.api';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-list-perfil-usuario',
  templateUrl: './list-perfil-usuario.component.html',
  styleUrls: ['./list-perfil-usuario.component.css']
})
export class ListPerfilUsuarioComponent implements OnInit {
  perfisUsuario: any;
  perfilUsuarioAtual = null;
  selecao: any;

  pag: Number = 1;
  qtdPorPagina: Number = 5;

  constructor(
    private service: PerfilUsuarioApiService,
    private router: Router,
    private toastr: ToastrService,
    private alertService: AlertService,
    private modalService: NgbModal
  ) { }


  ngOnInit(): void {
    this.selecao = null;

    this.perfisUsuario = [];
    this.service.obterTodos()
      .subscribe( data => {        
        this.perfisUsuario = data;
      });
  }

  selectRow(row){
    this.selecao = row;
  }

  edit(row){
    window.localStorage.removeItem('editPerfilUsuarioId');
    window.localStorage.setItem('editPerfilUsuarioId', row.id.toString());
    this.router.navigate(['perfilusuario/edit']);
  }

  confirmDelete(): void {
    const modal = this.modalService.open(DialogDeleteComponent, { centered: true });
    modal.componentInstance.data = { titulo: 'perfil de usuário', identificacao: this.selecao.nome, id: this.selecao.id };

    modal.result.then((data) => {
      this.service.deletar(this.selecao.id)
        .pipe(first())
        .subscribe(
          () => {
            this.toastr.success('Perfil de usuário excluído com sucesso.');
            this.ngOnInit();
          },
          error => {
            if (error.status === 400) {
              this.toastr.warning('Não foi possível excluir perfil de usuário. Verifique as mensagens.');
              this.alertService.error(error.error['']);
            }
            else {
              this.toastr.error('Ocorreu um erro ao excluir perfil de usuário.');
            }

          })

    }, (reason) => {
      // on dismiss
    });
  }

  carregarPerfis() {
    this.service.obterTodos()
      .subscribe(
        data => {
          this.perfisUsuario = data;
        },
        error => {
          console.log(error);
        });
  }
}
