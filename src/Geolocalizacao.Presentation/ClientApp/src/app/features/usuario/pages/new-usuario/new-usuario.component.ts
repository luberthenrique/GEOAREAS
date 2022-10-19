import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';
import { UsuarioApiService } from '../../api/usuario.api';
import { PerfilUsuarioApiService } from '../../../perfil-usuario/api/perfil-usuario.api';
import { AlertService } from '../../../../shared/services/alert/alert.service';

@Component({
  selector: 'app-new-usuario',
  templateUrl: './new-usuario.component.html',
  styleUrls: ['./new-usuario.component.css']
})
export class NewUsuarioComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

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

    this.service.inserir(usuario)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Usuário adicionado com sucesso.');
          this.router.navigate(['/usuario']);
        },
        error => {
          this.loading = false;          
          if (error.status === 400) {            
            this.toastr.warning('Não foi possível adicionar usuário. Verifique as mensagens.');
            this.alertService.error(error.error['']);
          }
          else {
            this.toastr.error('Ocorreu um erro ao adicionar usuário.');
          }
        });
  }

}
