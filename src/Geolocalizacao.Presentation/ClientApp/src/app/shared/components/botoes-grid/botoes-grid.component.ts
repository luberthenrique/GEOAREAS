import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApiService } from 'src/app/features/auth/api/auth.api';

@Component({
  selector: 'app-botoes-grid',
  templateUrl: './botoes-grid.component.html',
  styleUrls: ['./botoes-grid.component.css']
})
export class BotoesGridComponent implements OnInit, OnChanges {
  id: string;
  route: string;

  @Input() public action: string;
  @Input() public selectedId: string;
  @Output() public deleteEvent = new EventEmitter<string>();

  constructor(
    private router: Router,
    private authApiservie: AuthApiService
  ) { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges){
    if (changes.action){
      this.route = this.action;
    }
    else if (changes.selectedId){
      this.id = this.selectedId;
    }
  }

  add(){
    this.router.navigate([this.route.toLocaleLowerCase() + '/new']);
  }

  edit(){
    window.localStorage.removeItem('edit' + this.route + 'Id');
    window.localStorage.setItem('edit' + this.route + 'Id', this.id.toString());
    this.router.navigate([this.route.toLocaleLowerCase() + '/edit']);
  }

  delete(){
    this.deleteEvent.emit(this.id);
  }

  permiteAdicionar(){
    return true;
    //return this.claims.filter(c=> c.type.toLocaleLowerCase()  == this.route.toLocaleLowerCase() && c.value === 'Post').length > 0;
  }

  permiteEditar(){
    return true;
    //return this.claims.filter(c=> c.type.toLocaleLowerCase()  == this.route.toLocaleLowerCase() && c.value === 'Put').length > 0;
  }

  permiteDeletar(){
    return true;
    //return this.claims.filter(c=> c.type.toLocaleLowerCase()  == this.route.toLocaleLowerCase() && c.value === 'Delete').length > 0;
  }

  rowIsSelected(){
    return this.id;
  }
}
