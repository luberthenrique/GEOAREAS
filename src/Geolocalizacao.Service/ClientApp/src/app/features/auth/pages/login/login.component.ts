import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { AuthApiService } from '../../api/auth.api';

@Component({
    styleUrls: ['login.component.css'],
    templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
    form: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authService: AuthApiService,
        private alertService: AlertService,
        private toastr: ToastrService
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            senha: ['', Validators.required],
            lembrarMe: [false]
        });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
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

        const login = {
            email: this.f.email.value,
            senha: this.f.senha.value,
            lembrarMe: this.f.lembrarMe.value
        };

        this.authService.login(login)
            .subscribe(
                data => {
                    this.router.navigate([this.returnUrl]);
                },
                error => {
                    if (error.status === 400) {
                        this.toastr.warning('Não foi possível efetuar login. Verifique as mensagens.');
                        this.alertService.error(error.error['']);
                    }
                    else {
                        this.toastr.error('Ocorreu um erro ao efetuar login.');
                    }

                }
                ).add(
                  () => {
                    this.loading = false;
                  }
                );
    }
}
