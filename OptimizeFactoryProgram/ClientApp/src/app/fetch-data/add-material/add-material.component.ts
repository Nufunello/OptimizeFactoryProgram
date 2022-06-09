import { Component } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { Router } from "@angular/router";
import { MaterialService } from "src/app/material.service";
import {Location} from '@angular/common';


@Component({
    selector: 'app-add-materials',
    templateUrl: './add-material.component.html',
  })
  export class AddMaterialComponent {
  
    constructor(private fb: FormBuilder, private service: MaterialService, private router: Router, private _location: Location) {}
  
  
    researchForm = this.fb.group({
      name: [''],
      measure: []
    });
  
    onSubmit() {
      let rquest = this.researchForm.getRawValue();
      this.service.create(rquest).subscribe(() => this._location.back());
    }
  }