import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-aguardando-confirmacao',
  templateUrl: './aguardando-confirmacao.component.html',
  styleUrls: ['./aguardando-confirmacao.component.css']
})
export class AguardandoConfirmacaoComponent implements OnInit {
  email: string;

  constructor(private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.email = this.route.snapshot.params?.email ?? '';
  }

}
