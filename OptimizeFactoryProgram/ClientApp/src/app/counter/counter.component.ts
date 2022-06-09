import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Measure } from '../models/Measure.enum';
import { Product } from '../models/product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent implements OnInit {
  products: Product[] = [];
  constructor(private _liveAnnouncer: LiveAnnouncer, private router: Router, private service: ProductService, public dialog: MatDialog) {
  }
  
  ngOnInit(): void {
    this.service.list().subscribe(response => {
      this.products = response;
      console.log(response);
    })
}


  public getKey(value: number): string {
    return Measure[value]
  }
}
