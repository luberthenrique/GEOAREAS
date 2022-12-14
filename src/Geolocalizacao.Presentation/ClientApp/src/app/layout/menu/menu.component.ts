import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  @Input() route: string;
  @Input() name: string;
  @Input() icon: string;
  
  constructor() { }

  ngOnInit(): void {
  }

}
