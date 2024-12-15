import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

import ProductsView from '../views/ProductsView.vue'
import ModifyProductView from '../views/ModifyProductView.vue'

import SuppliersView from '../views/SuppliersView.vue'
import ModifySupplierView from '../views/ModifySupplierView.vue'

import ProductsToRestock from '../views/ProductsToRestock.vue'
import LargestSuppliers from '../views/LargestSuppliers.vue'
import MinimumOrderProducts from '../views/MinimumOrderProducts.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/products',
      name: 'products',
      component: ProductsView,
    },
    {
      path: '/suppliers',
      name: 'suppliers',
      component: SuppliersView,
    },
    {
      path: '/products/add',
      name: 'add-product',
      component: ModifyProductView,
    },
    {
      path: '/products/edit/:id',
      name: 'edit-product',
      component: ModifyProductView,
      props: route => ({ id: Number(route.params.id) })
    },
    {
      path: '/suppliers/add',
      name: 'add-supplier',
      component: ModifySupplierView,
    },
    {
      path: '/suppliers/edit/:id',
      name: 'edit-supplier',
      component: ModifySupplierView,
      props: route => ({ id: Number(route.params.id) })
    },
    {
      path: '/statistics/products-stock',
      name: 'protucts-to-restock',
      component: ProductsToRestock,
    },
    {
      path: '/statistics/largest-suppliers',
      name: 'largest-suppliers',
      component: LargestSuppliers,
    },
    {
      path: '/statistics/minimum-order-products',
      name: 'minimum-order-products',
      component: MinimumOrderProducts,
    },
  ],
})

export default router
