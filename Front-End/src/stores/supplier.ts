import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia';
import api from '@/api/suppliers';
import { type Supplier } from '@/types/Supplier';
import type { ModifySupplierRequest } from '@/types/ModifySupplierRequest';

export const useSupplierStore = defineStore('supplierStore', () => {
  const suppliers: Ref<Supplier[]> = ref([]);
  const total = ref(0);
  const loading = ref(false);
  const error = ref(null);

  async function fetchSuppliers(page: number, pageSize: number): Promise<void> {
    loading.value = true;
    try {
      const response = await api.getSuppliers(page, pageSize);
      suppliers.value = response.items;
      total.value = response.totalCount;
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function getSupplier(id: number): Promise<Supplier | undefined> {
    loading.value = true;
    try {
      const supplier = await api.getSupplier(id);
      error.value = null;
      return supplier
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function createSupplier(request: ModifySupplierRequest): Promise<void> {
    loading.value = true;
    try {
      await api.createSupplier(request);
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function updateSupplier(id: number, request: ModifySupplierRequest): Promise<void> {
    loading.value = true;
    try {
      await api.updateSupplier(id, request);
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function deleteSupplier(id: number): Promise<void> {
    try {
      await api.deleteSupplier(id);
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    }
  }

  return { suppliers, total, loading, error, fetchSuppliers, getSupplier, createSupplier, updateSupplier, deleteSupplier }
});
