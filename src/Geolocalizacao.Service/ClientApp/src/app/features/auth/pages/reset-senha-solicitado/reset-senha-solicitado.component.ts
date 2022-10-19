import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reset-senha-solicitado',
  templateUrl: './reset-senha-solicitado.component.html',
  styleUrls: ['./reset-senha-solicitado.component.css']
})
export class ResetSenhaSolicitadoComponent implements OnInit {
  email: string;

  constructor(private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.email = this.route.snapshot.params?.email ?? '';
  }

}
