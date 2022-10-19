import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';
import { AuthApiService } from 'src/app/features/auth/api/auth.api';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { UsuarioApiService } from '../../api/usuario.api';
import { Usuario } from '../../models/usuario.model';

@Component({
  selector: 'app-profile-usuario',
  templateUrl: './profile-usuario.component.html',
  styleUrls: ['./profile-usuario.component.css']
})
export class ProfileUsuarioComponent implements OnInit {
  usuario: Usuario;

  formCadastroSenha: FormGroup;
  formAlteracaoSenha: FormGroup;  
  formAlteracaoDados: FormGroup;
  
  formCadastroSenhaSubmitted = false;
  formAlteracaoSenhaSubmitted = false;
  formAlteracaoDadosSubmitted = false;

  loading = false;

  constructor(
    private usuarioApiService: UsuarioApiService, 
    private authApiService: AuthApiService,
    private router: Router,
    private alertService: AlertService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal) { }

  ngOnInit(): void {

    this.formCadastroSenha = this.formBuilder.group({
      novaSenha: ['', [Validators.required, Validators.minLength(6)]],
      confirmacaoSenha: ['', [Validators.required]
      ]
    });

    this.formAlteracaoSenha = this.formBuilder.group({
      senhaAtual: ['', Validators.required],
      novaSenha: ['', [Validators.required, Validators.minLength(6)]],
      confirmacaoSenha: ['', Validators.required]
    });

    this.formAlteracaoDados = this.formBuilder.group({
      nome: ['', Validators.required],
      idImagem: [null]
    });

    this.usuarioApiService.obterPorId(this.authApiService.currentUserValue.id)
      .subscribe(data => {
        this.usuario = data;

        this.formAlteracaoDadosConntrols.nome.setValue(data.nome);
      });
  }

  get formCadastroSenhaConntrols() { return this.formCadastroSenha.controls; }
  get formAlteracaoSenhaConntrols() { return this.formAlteracaoSenha.controls; }
  get formAlteracaoDadosConntrols() { return this.formAlteracaoDados.controls; }


  submitFormCadastroSenha(){
    this.formCadastroSenhaSubmitted = true;
    this.loading = true;

    // reset alerts on submit
    this.alertService.clear();

    if (this.formCadastroSenhaConntrols.novaSenha && this.formCadastroSenhaConntrols.novaSenha.value !== this.formCadastroSenhaConntrols.confirmacaoSenha.value) {
      this.formCadastroSenhaConntrols.confirmacaoSenha.setErrors({ confirmacaoSenha: true });
      return;
    }
    // stop here if form is invalid
    if (this.formCadastroSenha.invalid) {
      return;
    }

    const obj = {
      id: this.usuario.id,
      novaSenha: this.formCadastroSenhaConntrols.novaSenha.value,
      confirmacaoSenha: this.formCadastroSenhaConntrols.confirmacaoSenha.value
    };

    this.authApiService.addPassword(this.usuario.id, obj)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Senha adicionada com sucesso.');
          this.formCadastroSenhaConntrols.novaSenha.setValue('');
          this.formCadastroSenhaConntrols.confirmacaoSenha.setValue('');

          this.formCadastroSenhaSubmitted = false;
        },
        error => {
          if (error.status === 400) {
            this.toastr.warning('Não foi possível adicionar senha. Verifique as mensagens.');
            if (error.error['']){
              this.alertService.error(error.error['']);
            }
            else if(error.error.errors){
              Object.keys(error.error.errors).forEach(key => {
                this.alertService.error(error.error.errors[key]);
              });
            }
          }
          else {
            this.toastr.error('Ocorreu um erro ao adicionar senha.');
          }
        }).add(
          () => {
            this.loading = false;
          }
        );;
  }

  submitFormAlteracaoSenha(){
    this.formAlteracaoSenhaSubmitted = true;
    this.loading = true;

    // reset alerts on submit
    this.alertService.clear();

    if (this.formAlteracaoSenhaConntrols.novaSenha && this.formAlteracaoSenhaConntrols.novaSenha.value !== this.formAlteracaoSenhaConntrols.confirmacaoSenha.value) {
      this.formAlteracaoSenhaConntrols.confirmacaoSenha.setErrors({ confirmacaoSenha: true });
      return;
    }

    // stop here if form is invalid
    if (this.formAlteracaoSenha.invalid) {
      return;
    }

    const obj = {
      id: this.usuario.id,
      senhaAtual: this.formAlteracaoSenhaConntrols.senhaAtual.value,
      novaSenha: this.formAlteracaoSenhaConntrols.novaSenha.value,
      confirmacaoSenha: this.formAlteracaoSenhaConntrols.confirmacaoSenha.value
    };

    this.authApiService.changePassword(this.usuario.id, obj)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Senha alterada com sucesso.');
          this.formAlteracaoSenhaConntrols.senhaAtual.setValue('');
          this.formAlteracaoSenhaConntrols.novaSenha.setValue('');
          this.formAlteracaoSenhaConntrols.confirmacaoSenha.setValue('');

          this.formAlteracaoSenhaSubmitted = false;
        },
        error => {
          if (error.status === 400) {
            this.toastr.warning('Não foi possível alterar senha. Verifique as mensagens.');
            if (error.error['']){
              this.alertService.error(error.error['']);
            }
            else if(error.error.errors){
              Object.keys(error.error.errors).forEach(key => {
                this.alertService.error(error.error.errors[key]);
              });
            }
          }
          else {
            this.toastr.error('Ocorreu um erro ao alterar senha.');
          }
        }).add(
          () => {
            this.loading = false;
          }
        );;
  }

  submitFormAlteracaoDados(){
    this.formAlteracaoDadosSubmitted = true;
    this.loading = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.formAlteracaoDados.invalid) {
      return;
    }

    const obj = {
      id: this.usuario.id,
      nome: this.formAlteracaoDadosConntrols.nome.value,
      idImagem: this.formAlteracaoDadosConntrols.idImagem.value
    };

    this.usuarioApiService.updateData(this.usuario.id, obj)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Dados alterada com sucesso.');
          this.authApiService.logout();
          this.router.navigate(['/auth/login']);
        },
        error => {
          if (error.status === 400) {
            this.toastr.warning('Não foi possível alterar dados. Verifique as mensagens.');

            if (error.error['']){
              this.alertService.error(error.error['']);
            }
            else if(error.error.errors){
              Object.keys(error.error.errors).forEach(key => {
                this.alertService.error(error.error.errors[key]);
              });
            }
          }
          else {
            this.toastr.error('Ocorreu um erro ao alterar dados.');
          }
        }).add(
          () => {
            this.loading = false;
          }
        );;
  }
}
