import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { AuthApiService } from '../../api/auth.api';

@Component({
  selector: 'app-confirmacao-email',
  templateUrl: './confirmacao-email.component.html',
  styleUrls: ['./confirmacao-email.component.css']
})
export class ConfirmacaoEmailComponent implements OnInit {
  email = '';
  token = '';
  nome = '';
  loading = false;
  emailConfirmado;

  constructor(
    private route : ActivatedRoute,
    private authApiService: AuthApiService,
    private alertService: AlertService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    var email = this.email = this.route.snapshot.queryParams?.email;
    var token = this.token = this.route.snapshot.queryParams?.token;

    const params = {
      email: email,
      token: token
    };

    this.loading = true;

    this.authApiService.confirmEmail(params).subscribe(
      data => {
          this.emailConfirmado = true;
          this.loading = false;
      },
      error => {
          this.loading = false;
          if (error.status === 400) {
              this.toastr.warning('Não foi possível confirmar email. Verifique as mensagens.');
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
              this.toastr.error('Ocorreu um erro ao confirmar email.');
          }

          this.emailConfirmado = false;
          this.loading = false;
      });
  }

}
