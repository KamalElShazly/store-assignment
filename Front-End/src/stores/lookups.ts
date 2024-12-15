import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia';
import api from '@/api/lookups';
import type { SupplierLookup } from '@/types/Lookups';

export const useLookupsStore = defineStore('lookupsStore', () => {
  const suppliers: Ref<SupplierLookup[]> = ref([]);
  const loading = ref(false);
  const error = ref(null);

  async function getAllSuppliers(): Promise<void> {
    loading.value = true;
    try {
      const result = await api.getAllSuppliers();
      suppliers.value = result;
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  return { suppliers, loading, error, getAllSuppliers }
});
