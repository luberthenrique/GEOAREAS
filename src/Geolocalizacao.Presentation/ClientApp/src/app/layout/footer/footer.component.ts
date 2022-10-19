import { Component, OnInit } from '@angular/core';

declare var require: any;

@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.css']
})
/** footer component*/
export class FooterComponent implements OnInit {
    /** footer ctor */
    constructor() {

  }

  appVersion = '';

  ngOnInit() {
    this.appVersion = require('package.json').version;
  }
}
