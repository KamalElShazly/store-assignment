<script setup lang="ts">
import { computed, onMounted, ref, toRaw, type Ref } from 'vue'
import { useProductStore } from '@/stores/product'
import type { Product } from '@/types/Product';
import router from '@/router';
import { QuantityPerUnit } from '@/types/QuantityPerUnitEnum';

const page = ref(1);
const perPage = ref(10);
const pageCount = computed(() => Math.ceil(store.total / perPage.value))

const showDeleteDialog = ref(false);

const showErrorDialog = ref(false)
const errorDialogMessage = ref('')

const store = useProductStore();

const headers = ref([
  { title: "Id", key: 'id', sortable: false },
  { title: "Name", key: 'name', sortable: false },
  { title: "Quantity Per Unit", key: 'quantityPerUnit', value: (item: Product) => `${QuantityPerUnit[item.quantityPerUnit]}`, sortable: false },
  { title: "Reorder Level", key: 'reorderLevel', sortable: false },
  { title: "Unit Price", key: 'unitPrice', sortable: false },
  { title: "Units In Stock", key: 'unitsInStock', sortable: false },
  { title: "Units On Order", key: 'unitsOnOrder', sortable: false },
  { title: "Actions", key: 'actions', sortable: false },
]);

const deletingObject: Ref<Product | null> = ref(null);

const editItem = (item: Product) => {
  router.push({ name: 'edit-product', params: { id: toRaw(item).id } })
}

const deleteItem = (item: Product) => {
  deletingObject.value = toRaw(item)
  showDeleteDialog.value = true
}

const loadProducts = async () => {
  await store.fetchProducts(page.value - 1, perPage.value);
  if (store.error) {
    openErrorDialog("Something went wrong, Please try again later")
  }
}

onMounted(async () => {
  await loadProducts();
})

const closeDelete = () => {
  showDeleteDialog.value = false;
  deletingObject.value = null;
}

const deleteItemConfirm = async () => {
  showDeleteDialog.value = false;
  if (deletingObject.value != null) {
    await store.removeProduct(deletingObject.value.id)
    if (store.error) {
      openErrorDialog("Something went wrong, Please try again later")
    } else {
      deletingObject.value = null;
      page.value = 1
      await loadProducts()
    }
  }
}

const openErrorDialog = (message: string) => {
  errorDialogMessage.value = message
  showErrorDialog.value = true
}

const closeErrorDialog = () => {
  showErrorDialog.value = false
}

</script>

<template>
  <div>
    <v-data-table-server :items="store.products" :items-length="store.total" :page="page" :items-per-page="perPage" :headers="headers" :loading="store.loading">
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Products</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-btn variant="plain" :to="{ name: 'add-product' }">
            Add Product
          </v-btn>
          <v-dialog v-model="showDeleteDialog" max-width="500px">
            <v-card>
              <v-card-title class="text-h5">Are you sure you want to delete this item?</v-card-title>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text="Cancel" @click="closeDelete"></v-btn>
                <v-btn variant="flat" class="danger" text="Delete" @click="deleteItemConfirm"></v-btn>
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:item.actions="{ item }">
        <v-icon class="me-2" size="small" @click="editItem(item)">
          mdi-pencil
        </v-icon>
        <v-icon size="small" @click="deleteItem(item)">
          mdi-delete
        </v-icon>
      </template>
      <template v-slot:bottom>
        <div class="text-center pt-2">
          <v-pagination v-model="page" :length="pageCount" @update:model-value="loadProducts"></v-pagination>
        </div>
      </template>
    </v-data-table-server>
    <v-dialog v-model="showErrorDialog" max-width="500px">
      <v-card>
        <v-card-text>{{ errorDialogMessage }}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text="Confirm" @click="closeErrorDialog"></v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>
