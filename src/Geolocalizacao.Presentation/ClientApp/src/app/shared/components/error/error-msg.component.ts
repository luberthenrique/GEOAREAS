import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FormValidations } from '../../form-validations';

import {  throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-error-msg',
  templateUrl: './error-msg.component.html',
  styleUrls: ['./error-msg.component.css']
})
export class ErrorMsgComponent implements OnInit {

  @Input() label = '';

  @Input() control = new FormControl();
  @Input() submitted = false;
  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    
  }

  get errorMessage(): any{
    
    for (const propertyName in this.control.errors){
      if (this.control.errors.hasOwnProperty(propertyName) &&
        (this.control.touched || this.submitted)) {
        return FormValidations.getErrorMsg(this.label, propertyName, this.control.errors[propertyName]);
      }
    }
    return null;
  }
}
