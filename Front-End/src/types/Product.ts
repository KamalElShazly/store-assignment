import type { QuantityPerUnit } from "./QuantityPerUnitEnum";

export interface Product {
  id: number;
  name: string;
  quantityPerUnit: QuantityPerUnit;
  reorderLevel: number;
  unitPrice: number;
  unitsInStock: number;
  unitsOnOrder: number;
  supplierId: number;
}
