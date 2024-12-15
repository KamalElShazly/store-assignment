import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia';
import api from '@/api/statistics';
import type { Product } from '@/types/Product';
import type { SupplierWithProductsCount } from '@/types/Statistics';

export const useStatisticsStore = defineStore('statisticsStore', () => {
  const productsToRestock: Ref<Product[]> = ref([]);
  const largestSuppliers: Ref<SupplierWithProductsCount[]> = ref([]);
  const minimumOrderProducts: Ref<Product[]> = ref([]);
  const loading = ref(false);
  const error = ref(null);

  async function getProductsToRestock(): Promise<void> {
    loading.value = true;
    try {
      const result = await api.getProductsToRestock();
      productsToRestock.value = result;
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function getLargestSuppliers(): Promise<void> {
    loading.value = true;
    try {
      const result = await api.getLargestSuppliers();
      largestSuppliers.value = result;
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function getMinimumOrderProducts(): Promise<void> {
    loading.value = true;
    try {
      const result = await api.getMinimumOrderProducts();
      minimumOrderProducts.value = result;
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  return { productsToRestock, largestSuppliers, minimumOrderProducts, loading, error, getProductsToRestock, getLargestSuppliers, getMinimumOrderProducts }
});
