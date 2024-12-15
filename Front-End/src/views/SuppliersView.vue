<script setup lang="ts">
import { computed, onMounted, ref, toRaw, type Ref } from 'vue'
import { useSupplierStore } from '@/stores/supplier'
import type { Supplier } from '@/types/Supplier';
import router from '@/router';

const page = ref(1);
const perPage = ref(10);
const pageCount = computed(() => Math.ceil(store.total / perPage.value))

const showDeleteDialog = ref(false);

const showErrorDialog = ref(false)
const errorDialogMessage = ref('')

const store = useSupplierStore();

const headers = ref([
  { title: "Id", key: 'id', sortable: false },
  { title: "Name", key: 'name', sortable: false },
  { title: "Actions", key: 'actions', sortable: false },
]);

const deletingObject: Ref<Supplier | null> = ref(null);

const editItem = (item: Supplier) => {
  router.push({ name: 'edit-supplier', params: { id: toRaw(item).id } })
}

const deleteItem = (item: Supplier) => {
  deletingObject.value = toRaw(item)
  showDeleteDialog.value = true
}

const loadSuppliers = async () => {
  await store.fetchSuppliers(page.value - 1, perPage.value);
  if (store.error) {
    openErrorDialog("Something went wrong, Please try again later")
  }
}

onMounted(async () => {
  await loadSuppliers();
})

const closeDelete = () => {
  showDeleteDialog.value = false;
  deletingObject.value = null;
}

const deleteItemConfirm = async () => {
  showDeleteDialog.value = false;
  if (deletingObject.value != null) {
    await store.deleteSupplier(deletingObject.value.id)
    if (store.error) {
      openErrorDialog("Something went wrong, Please try again later")
    } else {
      deletingObject.value = null;
      page.value = 1
      await loadSuppliers()
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
    <v-data-table-server :items="store.suppliers" :items-length="store.total" :page="page" :items-per-page="perPage" :headers="headers" :loading="store.loading">
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>Suppliers</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-btn variant="plain" :to="{ name: 'add-supplier' }">
            Add Supplier
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
          <v-pagination v-model="page" :length="pageCount" @update:model-value="loadSuppliers"></v-pagination>
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
