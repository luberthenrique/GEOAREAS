import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../../services/loader/loader.service';

@Component({
  selector: 'app-my-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css']
})
export class LoaderComponent implements OnInit {

  loading = false;

  constructor(private loaderService: LoaderService) {

    this.loaderService.isLoading.subscribe((v) => {
      if (v){
        this.loading = v;
      }
      else{
        this.loading = false;
      }

    });

  }
  ngOnInit() {
  }

}
