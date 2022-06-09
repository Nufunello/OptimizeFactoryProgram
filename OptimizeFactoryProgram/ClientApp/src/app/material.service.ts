import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Material } from "./models/Material.model";

@Injectable()
export class MaterialService {
    constructor(private readonly _httpClient: HttpClient) {}

    create(entity: Material): Observable<number> {
        return this._httpClient.post<number>('http://localhost:4200/materials', entity, {headers : new HttpHeaders({ 'Content-Type': 'application/json' })});
    }

    
    list(): Observable<Material[]> {
        return this._httpClient.get<Material[]>('http://localhost:4200/materials');
    }
}