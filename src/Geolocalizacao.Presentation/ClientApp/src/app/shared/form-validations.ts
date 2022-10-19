import { FormArray, FormControl, FormGroup } from '@angular/forms';

export class FormValidations {

  static requiredMinCheckbox(min = 1): any {
    const validator = (formArray: FormArray) => {
      /* const values = formArray.controls;
      let totalChecked = 0;
      for (let i = 0; i < values.length; i++) {
        if (values[i].value) {
          totalChecked += 1;
        }
      } */
      const totalChecked = formArray.controls
        .map(v => v.value)
        .reduce((total, current) => current ? total + current : total, 0);
      return totalChecked >= min ? null : { required: true };
    };
    return validator;
  }

  static cepValidator(control: FormControl): any {

    const cep = control.value;
    if (cep && cep !== '') {
      const validacep = /^[0-9]{8}$/;
      return validacep.test(cep) ? null : { cepInvalido : true };
    }
    return null;
  }

  static equalsTo(otherField: string): any {
    const validator = (formControl: FormControl) => {
      if (otherField == null) {
        throw new Error('É necessário informar um campo.');
      }

      if (!formControl.root || !(formControl.root as FormGroup).controls) {
        return null;
      }

      const field = (formControl.root as FormGroup).get(otherField);

      if (!field) {
        throw new Error('É necessário informar um campo válido.');
      }

      if (field.value !== formControl.value) {
        return { equalsTo : otherField };
      }

      return null;
    };
    return validator;
  }

  static getErrorMsg(fieldName: string, validatorName: string, validatorValue?: any): any {
    const config: any = {
      required: `O campo ${fieldName} é obrigatório.`,     
      minlength: `O campo ${fieldName} precisa ter no mínimo ${validatorValue.requiredLength} caracteres.`,
      maxlength: `O campo ${fieldName} pode ter no máximo ${validatorValue.requiredLength} caracteres.`,
      min: `O campo ${fieldName} precisa ter valor mínimo de ${validatorValue.min}.`,
      max: `O campo ${fieldName} precisa ter valor máximo de  ${validatorValue.max}.`,
      email: `Campo ${fieldName} inválido.`,
      cepCadastrado: `${fieldName} já cadastrado.`,
      emailCadastrado: `${fieldName} já cadastrado.`,
      equalsTo: 'Campos não são iguais',
      pattern: 'Campo inválido'
    };

    return config[validatorName];
  }
}
