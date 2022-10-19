import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-section',
  template: `
    <h5 class= ""> {{text}}</h5>
    <div class="border-bottom w-100"></div>`,

    styleUrls: ['./section.component.css']
})
/** section component*/
export class SectionComponent {
  @Input() text: string;

    constructor() {
      
    }
}
