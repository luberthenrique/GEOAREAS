import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'telefone'})
export class TelefonePipe implements PipeTransform {

  transform(value: string): string {
    if (!value) {
      return null;
    }
    const identificacao = value.replace(/[^0-9]/g, '');

    if (identificacao.length === 10) {
      return identificacao.replace(/(\d{2})(\d{4})(\d{4})/g, "\($1) \$2-\$3");
    } else if (identificacao.length === 11) {
      return identificacao.replace(/(\d{2})(\d{1})(\d{4})(\d{4})/g, "\($1) \$2 \$3-\$4");
    }

    return value;
  }

}
