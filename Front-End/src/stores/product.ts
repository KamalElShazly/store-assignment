import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia';
import api from '@/api/products';
import { type Product } from '@/types/Product';
import type { ModifyProductRequest } from '@/types/ModifyProductRequest';

export const useProductStore = defineStore('productStore', () => {
  const products: Ref<Product[], Product[]> = ref([]);
  const total = ref(0);
  const loading = ref(false);
  const error = ref(null);

  async function fetchProducts(page: number, pageSize: number): Promise<void> {
    loading.value = true;
    try {
      const response = await api.getProducts(page, pageSize);
      products.value = response.items;
      total.value = response.totalCount;
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function getProduct(id: number): Promise<Product | null> {
    let product: Product | null
    loading.value = true;
    try {
      product = await api.getProduct(id);
      error.value = null;
      return product;
    } catch (err: any) {
      product = null;
      error.value = err.message;
    } finally {
      loading.value = false;
    }
    return product;
  }


  async function createProduct(request: ModifyProductRequest): Promise<void> {
    loading.value = true;
    try {
      await api.createProduct(request);
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function updateProduct(id: number, request: ModifyProductRequest): Promise<void> {
    loading.value = true;
    try {
      await api.updateProduct(id, request);
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  }

  async function removeProduct(id: number): Promise<void> {
    try {
      await api.deleteProduct(id);
      error.value = null;
    } catch (err: any) {
      error.value = err.message;
    }
  }

  return { products, total, loading, error, fetchProducts, getProduct, createProduct, updateProduct, removeProduct }
});
