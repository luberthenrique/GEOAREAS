import { Component, OnInit } from '@angular/core';
import { LoaderService } from '../../services/loader/loader.service';

@Component({
  selector: 'app-loader',
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
      else {
        setTimeout(() => {
          this.loading = false;
        },
          1000);
        
      }

    });

  }
  ngOnInit() {
  }

}
