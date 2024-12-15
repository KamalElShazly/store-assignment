<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>

<template>
  <v-responsive>
    <v-app>
      <v-navigation-drawer>
        <v-list>
          <v-list-item title="Home" to="/"></v-list-item>
          <v-list-item title="Products" to="/products"></v-list-item>
          <v-list-item title="Suppliers" to="/suppliers"></v-list-item>
          <v-list-group>
            <template v-slot:activator="{ props }">
              <v-list-item v-bind="props" title="Statistics"></v-list-item>
            </template>
            <v-list-item title="Products To Restock" to="/statistics/products-stock"></v-list-item>
            <v-list-item title="Largest Suppliers" to="/statistics/largest-suppliers"></v-list-item>
            <v-list-item title="Minimum Order Products" to="/statistics/minimum-order-products"></v-list-item>
          </v-list-group>
        </v-list>
      </v-navigation-drawer>

      <v-main>
        <v-container>
          <RouterView />
        </v-container>
      </v-main>
    </v-app>
  </v-responsive>
</template>

<style>
.app {
  display: flex;
}

main {
  flex: 1;
  padding: 20px;
}
</style>
