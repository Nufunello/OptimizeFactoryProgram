import { Component, OnInit } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { Router } from "@angular/router";
import { MaterialService } from "src/app/material.service";
import {Location} from '@angular/common';
import { ProductService } from "src/app/product.service";
import { Material } from "src/app/models/Material.model";


@Component({
    selector: 'app-add-product',
    templateUrl: './add-product.component.html',
  })
  export class AddProductComponent implements OnInit {
    materials: Material[] = [];
    constructor(private fb: FormBuilder, private service: ProductService, private serviceMaterials: MaterialService, private router: Router, private _location: Location) {}

    ngOnInit(): void {
      this.serviceMaterials.list().subscribe((response) => this.materials = response);
    }
  
  
    researchForm = this.fb.group({
      name: [''],
      ingridients: [],
      measure: [],
      count: [],
      material: [],
    });
  
    onSubmit() {
      let rquest = this.researchForm.getRawValue();
      console.log(rquest);
      const product = {
        name: rquest.name,
        measure: rquest.measure,
        ingridients: [{
          materialId: rquest.material.id,
          count: rquest.count,
        }]
      }
      this.service.create(product).subscribe(() => this._location.back());
    }
  }