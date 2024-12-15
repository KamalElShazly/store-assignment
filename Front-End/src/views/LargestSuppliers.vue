<script setup lang="ts">
import { onMounted, ref, } from 'vue'
import { useStatisticsStore } from '@/stores/statistics'

const showErrorDialog = ref(false)
const errorDialogMessage = ref('')

const store = useStatisticsStore();

onMounted(async () => {
  await store.getLargestSuppliers();
  if (store.error) {
    openErrorDialog("Something went wrong, Please try again later")
  }
})

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
    <v-table>
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Products</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in store.largestSuppliers" :key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.name }}</td>
          <td>{{ item.productsCount }}</td>
        </tr>
      </tbody>
    </v-table>
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
