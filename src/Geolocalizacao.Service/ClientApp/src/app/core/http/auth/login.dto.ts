export interface LoginDTO{
  login: string;
  senha: string;
  lembrarMe: string;

  confirmacaoSenha: string;
  token: string;
  claimsArray: Array<string>;
}
