import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormGroupDirective, FormControl, NgForm, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

import { AlertService } from 'src/app/shared/services/alert/alert.service';
import { ToastrService } from 'ngx-toastr';
import { PerfilUsuarioApiService } from '../../api/perfil-usuario.api';
import { EnumService } from 'src/app/shared/services/enum/enum.service';
import { AuthApiService } from 'src/app/features/auth/api/auth.api';


@Component({
  selector: 'app-edit-perfil',
  templateUrl: './edit-perfil-usuario.component.html',
  styleUrls: ['./edit-perfil-usuario.component.css']
})
export class EditPerfilUsuarioComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  classificacoes;
  claims = new Array();
  hierarquiaSelecionada;
  classificacaoSelecionada;
  perfisHierarquia = new Array();
  hierarquiaGravada: any;

  menus = [
    {
      titulo: 'Cadastros',
      submenus: [
        {
          titulo: 'Filiais',
          value: 'Filial',
          methods: []
        },
        {
          titulo: 'Ordens de Serviço',
          value: 'OrdemServico',
          methods: []
        },
      ]
    },
    {
      titulo: 'Configurações',
      submenus: [
        {
          titulo: 'Usuários',
          value: 'Usuario',
          methods: []
        },
        {
          titulo: 'Perfis de usuário',
          value: 'PerfilUsuario',
          methods: []
        }
      ]
    },
  ];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private service: PerfilUsuarioApiService,
    private toastr: ToastrService,
    private authService: AuthApiService,
    private enumService: EnumService
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      id: [null],
      nome: ['', Validators.required],
      isAdmin: [false, Validators.required]
    });

    const claims = this.authService.currentUserValue.claims;

    this.menus.forEach(menu => {
      menu.submenus.forEach(submenu => {

        claims.filter(c => c.type === submenu.value).forEach(claim => {
          submenu.methods.push(claim.value);
        });

      });
    });

    this.menus = this.menus.filter(c => c.submenus.length > 0);

    const perfilUsuarioId = window.localStorage.getItem('editPerfilUsuarioId');

    this.service.obterPorId(perfilUsuarioId)
      .subscribe(data => {
        this.form.setValue({
          id: data.id,
          nome: data.nome,
          isAdmin: data.isAdmin
        });
        this.claims = data.claims;
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

    const perfilUsuario = {
      id: this.f.id.value,
      nome: this.f.nome.value,
      idAdmin: this.f.isAdmin.value,
      claims: this.claims
    };

    this.service.alterar(perfilUsuario.id, perfilUsuario)
      .pipe(first())
      .subscribe(
        data => {
          this.toastr.success('Perfil de usuário editado com sucesso.');
          this.router.navigate(['/perfilusuario']);
        },
        error => {

          this.loading = false;
          if (error.status === 400) {
            this.toastr.warning('Não foi possível editar perfil de usuário. Verifique as mensagens.');
            this.alertService.error(error.error['']);
          }
          else {
            this.toastr.error('Ocorreu um erro ao editar perfil de usuário.');
          }

        });
  }

  onCheckChange(type, value, event) {    
    if (event.target.checked){  
      if (this.claims.filter(c=> c.type === type && c.value === value).length == 0){
        this.claims.push({type, value});

        // Caso seja selecionada uma permissão, incluir a listagem
        if (value !== 'Get' && this.claims.filter(c=> c.type === type && c.value === 'Get').length === 0)
          this.claims.push({type, value: 'Get'});
      }    
    
    }
    else{
      //Caso retire a permissão de leitura, remover todas as permissões
      if (value === 'Get'){
        this.claims = this.claims.filter(c=> c.type !== type)
      }
      else {
        this.claims = this.claims.filter(c=> c.type + c.value !== type + value)
      }
        
        
    }
  }

  existePermissao(type, value): boolean {
    return this.claims.filter(c=> c.type === type && c.value === value).length > 0;
  }
}
