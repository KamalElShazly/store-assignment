import axios, { type AxiosInstance } from 'axios';
import type { SupplierLookup } from '@/types/Lookups';

const apiClient: AxiosInstance = axios.create({
  baseURL: 'https://localhost:7226/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getAllSuppliers(): Promise<[SupplierLookup]> {
    const res = await apiClient.get<[SupplierLookup]>('/lookups/suppliers');
    return res.data;
  },
};
