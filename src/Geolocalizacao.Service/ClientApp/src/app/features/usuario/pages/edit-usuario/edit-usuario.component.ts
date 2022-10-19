import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { ValidatorFn } from '@angular/forms';

import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { ToastrService } from 'ngx-toastr';

import { UsuarioApiService } from '../../api/usuario.api';
import { PerfilUsuarioApiService } from '../../../perfil-usuario/api/perfil-usuario.api';

@Component({
  selector: 'app-edit-usuario',
  templateUrl: './edit-usuario.component.html',
  styleUrls: ['./edit-usuario.component.css']
})
export class EditUsuarioComponent implements OnInit {

  form: FormGroup;
  loading = false;
  submitted = false;

  perfisUsuario = new Array();
  usuarios = new Array();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private alertService: AlertService,
    private service: UsuarioApiService,
    private toastr: ToastrService,
    private perfilUsuarioService: PerfilUsuarioApiService) { }

  ngOnInit() {

    this.form = this.formBuilder.group({
      id: [null],
      idPerfil: ['', Validators.required],
      idUsuario: [null],
      nome: ['', Validators.required],
      habilitado: [true],
      email: ['', [Validators.required, Validators.email]]
    });

    const equipeId = window.localStorage.getItem('editUsuarioId');
    this.service.obterPorId(equipeId)
      .subscribe(data => {
        this.form.setValue({
          id: data.id,
          idPerfil: data.idPerfil,
          idUsuario: data.idUsuario,
          nome: data.nome,
          habilitado: data.habilitado,
          email: data.email
        });
      });

    this.perfilUsuarioService.obterTodos()
      .subscribe(data => {
        this.perfisUsuario = data;
      });

    this.service.obterTodos()
      .subscribe(data => {
        this.usuarios = data;
      });
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
    this.loading = true;

    const usuario = {
      id: this.f.id.value,
      idPerfil: this.f.idPerfil.value,
      idUsuario: this.f.idUsuario.value,
      nome: this.f.nome.value,
      habilitado: this.f.habilitado.value,
      email: this.f.email.value
    };

    this.service.alterar(usuario.id, usuario)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Usuário atualizado com sucesso.');
          this.router.navigate(['/usuario']);
        },
        error => {

          this.loading = false;
          if (error.status === 400) {
            this.toastr.warning('Não foi possível atualizar usuário. Verifique as mensagens.');
            this.alertService.error(error.error['']);
          }
          else {
            this.toastr.error('Ocorreu um erro ao atualizar usuário.');
          }
        });
  }
}
