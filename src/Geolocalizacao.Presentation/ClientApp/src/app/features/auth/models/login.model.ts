import { LoginDTO } from 'src/app/core/http/auth/login.dto';

export class Login{
  login: string;
  senha: string;
  lembrarMe: boolean;

  confirmacaoSenha: string;
  token: string;
  claimsArray: Array<string> = [];

  private constructor(model: any){
    model = model || {};
    this.login = model.login || null;
    this.senha = model.senha || null;
    this.lembrarMe = model.lembrarMe || null;
  }

  static fromDTO(dto: LoginDTO): Login{
    return new Login(dto);
  }
}
