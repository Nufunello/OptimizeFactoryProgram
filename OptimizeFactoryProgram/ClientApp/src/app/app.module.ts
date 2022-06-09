import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialExampleModule } from './material.module';
import { MaterialService } from './material.service';
import { AddMaterialComponent } from './fetch-data/add-material/add-material.component';
import { ProductService } from './product.service';
import { AddProductComponent } from './counter/add-product/add-product.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AddMaterialComponent,
    AddProductComponent,
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialExampleModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'api/materials', component: FetchDataComponent },
      { path: 'api/products', component: CounterComponent },
      { path: 'add-material', component: AddMaterialComponent },
      { path: 'add-product', component: AddProductComponent },
    ])
  ],
  providers: [MaterialService, ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
