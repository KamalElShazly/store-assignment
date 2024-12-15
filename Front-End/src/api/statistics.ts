import type { Product } from '@/types/Product';
import type { SupplierWithProductsCount } from '@/types/Statistics';
import axios, { type AxiosInstance } from 'axios';

const apiClient: AxiosInstance = axios.create({
  baseURL: 'https://localhost:7226/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getProductsToRestock(): Promise<[Product]> {
    const res = await apiClient.get<[Product]>('/statistics/GetProductsToRestock');
    return res.data;
  },
  async getLargestSuppliers(): Promise<[SupplierWithProductsCount]> {
    const res = await apiClient.get<[SupplierWithProductsCount]>('/statistics/GetSuppliersWithMostProducts');
    return res.data;
  },
  async getMinimumOrderProducts(): Promise<[Product]> {
    const res = await apiClient.get<[Product]>('/statistics/GetProductsWithMinimumOrders');
    return res.data;
  },
};
