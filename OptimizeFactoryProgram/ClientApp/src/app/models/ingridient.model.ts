import { Material } from "./Material.model";

export interface Ingridient {
    id: string;
    materialId: string;
    material: Material;
    count: number;
}