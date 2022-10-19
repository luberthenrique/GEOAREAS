import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { AuthApiService } from '../../api/auth.api';

@Component({
  selector: 'app-resetar-senha',
  templateUrl: './resetar-senha.component.html',
  styleUrls: ['./resetar-senha.component.css']
})
export class ResetarSenhaComponent implements OnInit {
  form: FormGroup;
  submitted = false;
  loading = false;
  loadingFb = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthApiService,
    private alertService: AlertService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
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

        this.loading = true;

        const user = {
          email: this.f.email.value
        };

        this.authService.resetPassword(user)
            .subscribe(
                data => {
                    this.router.navigate(['auth/resetar-senha-solicitado', {email: this.f.email.value}]);
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
