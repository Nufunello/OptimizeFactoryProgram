import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Material } from '../models/Material.model';
import { Measure } from '../models/Measure.enum';
import { MaterialService } from '../material.service';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: Material[] = [];
  constructor(private _liveAnnouncer: LiveAnnouncer, private router: Router, private service: MaterialService, public dialog: MatDialog) {
  }
  
  ngOnInit(): void {
    this.service.list().subscribe(response => {
      this.forecasts = response;
    })
}


  public getKey(value: number): string {
    return Measure[value]
  }
}

