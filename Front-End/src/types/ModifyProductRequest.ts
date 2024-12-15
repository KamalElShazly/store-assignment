import type { QuantityPerUnit } from "./QuantityPerUnitEnum";

export interface ModifyProductRequest {
  name: string;
  quantityPerUnit: QuantityPerUnit;
  reorderLevel: number;
  unitPrice: number;
  unitsInStock: number;
  unitsOnOrder: number;
  supplierId: number | null;
}
