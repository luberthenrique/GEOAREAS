import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { filter, first } from 'rxjs/operators';
import { AlertService } from '../../../../shared/services/alert/alert.service';
import { SetoresCensitariosApiService } from '../../api/setores-censitarios.api';

@Component({
  selector: 'app-modal-carregar-arquivo',
  templateUrl: './modal-carregar-arquivo.component.html',
  styleUrls: ['./modal-carregar-arquivo.component.scss']
})
/** modal-obter-regiao component*/
export class ModalCarregarArquivoComponent implements OnInit {
  files: File[] = new Array();
  form: FormGroup;

  constructor(
    private service: SetoresCensitariosApiService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private alertService: AlertService,
    public activeModal: NgbActiveModal) {

  }

  get f() { return this.form.controls; }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      id: [null],
      nome: ['', Validators.required]
    });
  }

  onSubmit() {

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    const formData = new FormData();

    for (let i = 0; i < this.files.length; i++) {
      formData.append('files', this.files[i])
    }

    formData.append('nome', this.f.nome.value);

    this.service.inserir(formData)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Arquivo de setores censitários adicionado com sucesso.');
          this.activeModal.close(true);
        },
        error => {
          if (error.status === 400) {
            this.toastr.warning('Não foi possível adicionar arquivo de setores censitários. Verifique as mensagens.');

            if (error.error.errors) {
              Object.keys(error.error.errors).forEach(key => {
                this.alertService.error(error.error.errors[key]);
              });
            }
            else if (error.error) {
              Object.keys(error.error).forEach(key => {
                this.alertService.error(error.error[key]);
              });
            }

          }
          else {
            this.toastr.error('Ocorreu um erro ao adicionar arquivo de setores censitários.');
          }
        });
  }

  chooseFile(files: FileList) {
    // ...

    console.log(files)
    for (let i = 0; i < files.length; i++) {
      this.files.push(files[i]);
    }

    console.log(this.files)
  }

  delete(file) {
    this.files = this.files.filter(c => c !== file);
  }
}
