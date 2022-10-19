import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AlertService } from '../../../../shared/services/alert/alert.service';
import { AuthApiService } from '../../api/auth.api';

@Component({
    selector: 'app-reenviar-codigo-email',
    templateUrl: './reenviar-codigo-email.component.html',
    styleUrls: ['./reenviar-codigo-email.component.css']
})
/** reenviar-codigo-email component*/
export class ReenviarCodigoEmailComponent {
  form: FormGroup;
  submitted = false;
  loading = false;

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthApiService,
    private alertService: AlertService,
    private toastr: ToastrService) {

  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    // reset alerts on submit
    this.alertService.clear();

    if (this.f.senha && this.f.senha.value !== this.f.confirmacaoSenha.value) {
      this.f.confirmacaoSenha.setErrors({ confirmacaoSenha: true });
      return;
    }

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    const user = {
      email: this.f.email.value,
    };

    this.authService.resendConfirmation(user)
      .subscribe(
        data => {
          this.router.navigate(['auth/aguardandoconfirmacao', { email: this.f.email.value }]);
          this.loading = false;
        },
        error => {
          if (error.status === 400) {
            this.toastr.warning('Não foi reenviar confirmação de e-mail. Verifique as mensagens.');

            if (error.error['']) {
              this.alertService.error(error.error['']);
            }
            else if (error.error.errors) {
              Object.keys(error.error.errors).forEach(key => {
                this.alertService.error(error.error.errors[key]);
              });
            }
          }
          else {
            this.toastr.error('Ocorreu um erro ao reenviar confirmação de e-mail.');
          }
          this.loading = false;
        });
  }
}
