import axios, { type AxiosInstance } from 'axios';
import { type PaginationResponse } from '@/types/PaginationResponse';
import { type Product } from '@/types/Product';
import type { ModifyProductRequest } from '@/types/ModifyProductRequest';

const apiClient: AxiosInstance = axios.create({
  baseURL: 'https://localhost:7226/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getProducts(page: number, pageSize: number): Promise<PaginationResponse<Product>> {
    const res = await apiClient.get<PaginationResponse<Product>>('/products', { params: { page: page, pageSize: pageSize } });
    return res.data;
  },
  async getProduct(id: number): Promise<Product> {
    const res = await apiClient.get<Product>(`/products/${id}`);
    return res.data;
  },
  async createProduct(request: ModifyProductRequest): Promise<void> {
    const res = await apiClient.post('/products', request);
    return res.data;
  },
  async updateProduct(id: number, request: ModifyProductRequest): Promise<void> {
    const res = await apiClient.put(`/products/${id}`, request);
    return res.data;
  },
  async deleteProduct(id: number): Promise<void> {
    const res = await apiClient.delete(`/products/${id}`);
    return res.data;
  },
};
