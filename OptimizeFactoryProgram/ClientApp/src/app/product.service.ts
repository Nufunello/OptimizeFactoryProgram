import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Material } from "./models/Material.model";
import { Product } from "./models/product.model";

@Injectable()
export class ProductService {
    constructor(private readonly _httpClient: HttpClient) {}

    create(entity: Product): Observable<number> {
        return this._httpClient.post<number>('http://localhost:4200/materials', entity, {headers : new HttpHeaders({ 'Content-Type': 'application/json' })});
    }

    
    list(): Observable<Product[]> {
        return this._httpClient.get<Product[]>('http://localhost:4200/products');
    }
}