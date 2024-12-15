import axios, { type AxiosInstance } from 'axios';
import { type PaginationResponse } from '@/types/PaginationResponse';
import { type Supplier } from '@/types/Supplier';
import type { ModifySupplierRequest } from '@/types/ModifySupplierRequest';

const apiClient: AxiosInstance = axios.create({
  baseURL: 'https://localhost:7226/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getSuppliers(page: number, pageSize: number): Promise<PaginationResponse<Supplier>> {
    const res = await apiClient.get<PaginationResponse<Supplier>>('/suppliers', { params: { page: page, pageSize: pageSize } });
    return res.data;
  },
  async getSupplier(id: number): Promise<Supplier> {
    const res = await apiClient.get<Supplier>(`/suppliers/${id}`);
    return res.data;
  },
  async createSupplier(request: ModifySupplierRequest): Promise<void> {
    const res = await apiClient.post('/suppliers', request);
    return res.data;
  },
  async updateSupplier(id: number, request: ModifySupplierRequest): Promise<void> {
    const res = await apiClient.put(`/suppliers/${id}`, request);
    return res.data;
  },
  async deleteSupplier(id: number): Promise<void> {
    const res = await apiClient.delete(`/suppliers/${id}`);
    return res.data;
  },
};
