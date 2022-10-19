import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRouting } from './auth-routing';
import { LoginComponent } from './pages/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { AguardandoConfirmacaoComponent } from './pages/aguardando-confirmacao/aguardando-confirmacao.component';
import { ConfirmacaoEmailComponent } from './pages/confirmacao-email/confirmacao-email.component';
import { ReenviarCodigoEmailComponent } from './pages/reenviar-codigo-email/reenviar-codigo-email.component';
import { ResetarSenhaComponent } from './pages/resetar-senha/resetar-senha.component';
import { ResetSenhaSolicitadoComponent } from './pages/reset-senha-solicitado/reset-senha-solicitado.component';
import { ResetarSenhaConfirmarComponent } from './pages/resetar-senha-confirmar/resetar-senha-confirmar.component';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        AuthRouting
    ],
    declarations: [
        LoginComponent,
        AguardandoConfirmacaoComponent,
        ConfirmacaoEmailComponent,
        ReenviarCodigoEmailComponent,
        ResetarSenhaComponent,
        ResetarSenhaConfirmarComponent,
        ResetSenhaSolicitadoComponent
    ]
})
export class AuthModule { }
