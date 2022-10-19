import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

import { AlertService } from '../../../../shared/services/alert/alert.service';
import { ConsultaCepService } from '../../../../shared/services/consulta-cep/consulta-cep.service';
import { ClienteApiService } from '../../api/cliente.api';
import { Cliente } from '../../models/cliente.model';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html',
  styleUrls: ['./formulario.component.css']
})
/** formuario-cliente component*/
export class FormularioComponent implements OnInit {
  form: FormGroup;
  formApi: FormGroup;

  loading = false;
  submitted = false;
  
  apis: any[] = new Array();
  selecao: any;

  @Output() salvar = new EventEmitter<Cliente>();

  @Input() public editar: boolean = false;  

  /** formuario-cliente ctor */
  constructor(
    private service: ClienteApiService,
    private formBuilder: FormBuilder,
    private router: Router,
    private alertService: AlertService,    
    private consutaCepService: ConsultaCepService,
    private toastr: ToastrService,
    private modalService: NgbModal) {

  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      id: [null],
      cnpj: ['', Validators.required],
      inscricaoMunicipal: ['', Validators.required],
      razaoSocial: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(300)]],
      observacao: [''],
      logradouro: [{ value: '', disabled: true }, Validators.required],
      numero: [{ value: '', disabled: true }, Validators.required],
      complemento: [{ value: '', disabled: true }],
      bairro: [{ value: '', disabled: true }, Validators.required],
      cidade: [{ value: '', disabled: true }, Validators.required],
      uf: [{ value: '', disabled: true }, Validators.required],
      cep: ['', Validators.required],
      telefone1: ['', Validators.required],
      telefone2: [''],
      email: ['', Validators.required],
    });    

    if (this.editar) {
      this.formApi = this.formBuilder.group({
        id: [null],
        nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(300)]],
      });

      const Id = window.localStorage.getItem('editClienteId');
      this.service.obterPorId(Id)
        .subscribe(data => {
          this.form.setValue({
            id: data.id,
            cnpj: data.cnpj,
            inscricaoMunicipal: data.inscricaoMunicipal,
            razaoSocial: data.razaoSocial,
            observacao: data.observacao,
            logradouro: data.logradouro,
            numero: data.numero,
            complemento: data.complemento,
            bairro: data.bairro,
            cidade: data.cidade,
            uf: data.uf,
            cep: data.cep,
            telefone1: data.telefone1,
            telefone2: data.telefone2,
            email: data.email
          });

          this.apis = data.apis;

        });
    }    

    this.f.cep.valueChanges.subscribe(selectedValue => {
      if (selectedValue && selectedValue.length >= 8) {
        this.buscarCep();
      }
      else {
        this.limparCamposEndereco();
      }
    });
  }

  get f() { return this.form.controls; }
  get fApi() { return this.formApi.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    const cliente = {
      id: this.f.id.value,
      cnpj: this.f.cnpj.value,
      inscricaoMunicipal: this.f.inscricaoMunicipal.value,
      razaoSocial: this.f.razaoSocial.value,
      observacao: this.f.observacao.value,
      logradouro: this.f.logradouro.value,
      numero: this.f.numero.value,
      complemento: this.f.complemento.value,
      bairro: this.f.bairro.value,
      cidade: this.f.cidade.value,
      uf: this.f.uf.value,
      cep: this.f.cep.value,
      telefone1: this.f.telefone1.value,
      telefone2: this.f.telefone2.value,
      email: this.f.email.value,
      datahoraInclusao: null,
      apis: null
    };

    this.salvar.emit(cliente);
  }

  private buscarCep() {
    const cep = this.f.cep.value.replace('_', '').trim();
    this.consutaCepService.consultaCEP(cep)
      .subscribe(
        data => {
          if (data.cep) {

            this.form.patchValue({
              logradouro: data.logradouro,
              bairro: data.bairro,
              cidade: data.localidade,
              uf: data.uf
            });

            if (data.bairro === '') {
              this.f.bairro.enable();
            }

            if (data.logradouro === '') {
              this.f.logradouro.enable();
            }

            this.f.numero.enable();
            this.f.complemento.enable();
          }
          else if (data.erro) {
            this.toastr.error('Ocorreu um erro ao buscar CEP.');
          }
        },
        error => {
          this.toastr.error('Ocorreu um erro ao buscar CEP.');

          this.f.bairro.enable();
          this.f.endereco.enable();
          this.f.cidade.enable();
          this.f.uf.enable();

          this.f.numero.enable();
          this.f.complemento.enable();
        });
  }

  private limparCamposEndereco() {
    this.f.logradouro.disable();
    this.f.numero.disable();
    this.f.complemento.disable();
    this.f.bairro.disable();
    this.f.cidade.disable();
    this.f.uf.disable();

    this.f.logradouro.setValue('');
    this.f.numero.setValue('');
    this.f.complemento.setValue('');
    this.f.bairro.setValue('');
    this.f.cidade .setValue('');
    this.f.uf.setValue('');
  }

  selectApi(api: any) {
    this.selecao = api;
  }
}
