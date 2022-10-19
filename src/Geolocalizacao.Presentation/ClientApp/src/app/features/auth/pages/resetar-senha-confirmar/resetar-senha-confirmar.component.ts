import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { AuthApiService } from '../../api/auth.api';

@Component({
  selector: 'app-resetar-senha-confirmar',
  templateUrl: './resetar-senha-confirmar.component.html',
  styleUrls: ['./resetar-senha-confirmar.component.css']
})
export class ResetarSenhaConfirmarComponent implements OnInit {
  form: FormGroup;
  submitted = false;
  loading = false;

  id: string;
  token: string;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthApiService,
    private alertService: AlertService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.queryParams?.id;
    this.token = this.route.snapshot.queryParams?.token;

    this.form = this.formBuilder.group({      
      senha: ['', Validators.required],
      confirmacaoSenha: ['', [Validators.required]]
    });
  }

// convenience getter for easy access to form fields
    // tslint:disable-next-line: typedef
    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;
        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        if (this.f.senha && this.f.senha.value !== this.f.confirmacaoSenha.value) {
          this.f.confirmacaoSenha.setErrors({ confirmacaoSenha: true });
          return;
        }

        this.loading = true;

        const user = {
          id: this.id,
          token: this.token,
          novaSenha: this.f.senha.value,
          confirmacaoSenha: this.f.confirmacaoSenha.value,
        };

      this.authService.resetPasswordConfirm(this.id, user)
            .subscribe(
              data => {
                    this.toastr.success('Senha redefinida com sucesso.');
                    this.router.navigate(['auth/login']);
                    this.loading = false;
                },
                error => {
                    if (error.status === 400) {
                        this.toastr.warning('Não foi possível redefinir senha. Verifique as mensagens.');

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
                      this.toastr.error('Ocorreu um erro ao redefinir senha.');
                    }
                    this.loading = false;
                });
    }

}
