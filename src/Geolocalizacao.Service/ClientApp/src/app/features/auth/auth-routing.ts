import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AguardandoConfirmacaoComponent } from './pages/aguardando-confirmacao/aguardando-confirmacao.component';
import { ConfirmacaoEmailComponent } from './pages/confirmacao-email/confirmacao-email.component';
import { LoginComponent } from './pages/login/login.component';
import { ReenviarCodigoEmailComponent } from './pages/reenviar-codigo-email/reenviar-codigo-email.component';
import { ResetarSenhaConfirmarComponent } from './pages/resetar-senha-confirmar/resetar-senha-confirmar.component';
import { ResetSenhaSolicitadoComponent } from './pages/reset-senha-solicitado/reset-senha-solicitado.component';
import { ResetarSenhaComponent } from './pages/resetar-senha/resetar-senha.component';


const routes: Routes = [
    {
        path: 'login', component: LoginComponent
    },
    {
        path: 'aguardandoconfirmacao', component: AguardandoConfirmacaoComponent
    },
    {
        path: 'confirmemail', component: ConfirmacaoEmailComponent
    },
    {
        path: 'reenviar-confirmacao-email', component: ReenviarCodigoEmailComponent
    },
    {
      path: 'resetar-senha', component: ResetarSenhaComponent
    },
    { 
      path: 'resetar-senha-confirmar', component: ResetarSenhaConfirmarComponent
    },
    {
      path: 'resetar-senha-solicitado', component: ResetSenhaSolicitadoComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRouting { }
