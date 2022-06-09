import { Ingridient } from "./ingridient.model";
import { Measure } from "./Measure.enum";

export interface Product {
    id?: string;
    name: number;
    ingridients: any[];
    measure: Measure;
}