<script setup lang="ts">
import { type ModifyProductRequest } from '@/types/ModifyProductRequest';
import { computed, onMounted, ref, toRaw, type Ref } from 'vue';
import { useProductStore } from '@/stores/product'
import { useLookupsStore } from '@/stores/lookups'
import router from '@/router';
import { QuantityPerUnit } from '@/types/QuantityPerUnitEnum';
import type { Product } from '@/types/Product';

const props = defineProps({
  id: Number
})

const productStore = useProductStore()
const lookupsStore = useLookupsStore()

const showErrorDialog = ref(false)
const errorDialogMessage = ref('')

const isFormValid = ref(false)

const productId: Ref<number | null> = ref(null);
const isEditing = computed(() => productId.value != null)

const request: Ref<ModifyProductRequest> = ref({
  name: '',
  quantityPerUnit: QuantityPerUnit.Kilo,
  reorderLevel: 0,
  unitPrice: 0,
  unitsInStock: 0,
  unitsOnOrder: 0,
  supplierId: null,
});

const rules = {
  requiredString: (value: string) => !!value || 'Field is required',
  requiredNumber: (value: number) => (value != null && !isNaN(value) && isFinite(value)) || 'Field is required',
  length: (value: string) => value?.length <= 100 || 'Field must be 100 characters or less',
  greaterThanZero: (value: number) => value > 0 || 'Value must be greater than 0',
};

const submit = async () => {
  if (isFormValid.value) {
    if (productId.value == null) {
      await productStore.createProduct(request.value);
    } else {
      await productStore.updateProduct(productId.value, request.value);
    }
    if (productStore.error) {
      openErrorDialog("Something went wrong, Please try again later")
    } else {
      router.push({ name: 'products' })
    }
  }
};

onMounted(async () => {
  const id = toRaw(props).id
  let getProductRequest: Promise<Product | null> = Promise.resolve(null);
  if (id) {
    productId.value = id
    getProductRequest = productStore.getProduct(id);
  }

  const results = await Promise.all([getProductRequest, lookupsStore.getAllSuppliers()])
  if (productStore.error || lookupsStore.error) {
    openErrorDialog("Something went wrong, Please try again later")
  } else {
    const product = results[0];
    if (id) {
      if (product) {
        request.value.name = product.name
        request.value.quantityPerUnit = product.quantityPerUnit
        request.value.reorderLevel = product.reorderLevel
        request.value.unitPrice = product.unitPrice
        request.value.unitsInStock = product.unitsInStock
        request.value.unitsOnOrder = product.unitsOnOrder
        request.value.supplierId = product.supplierId
      } else {
        openErrorDialog("Something went wrong, Please try again later")
      }
    }
  }
})

const unitOptions = ref(Object.entries(QuantityPerUnit)
  .filter(([, value]) => typeof value === "number")
  .map(([key, value]) => ({
    title: key,
    value: value as number,
  })))

const openErrorDialog = (message: string) => {
  errorDialogMessage.value = message
  showErrorDialog.value = true
}

const closeErrorDialog = () => {
  showErrorDialog.value = false
}

</script>


<template>
  <v-container class="mt-4">
    <v-card>
      <v-card-title>
        <h2>{{ isEditing ? 'Edit Product' : 'Add Product' }}</h2>
      </v-card-title>
      <v-card-text>
        <v-form v-model="isFormValid" @submit.prevent="submit">
          <v-container>
            <v-row>
              <v-col>
                <v-text-field v-model="request.name" label="Name" variant="outlined" required :rules="[rules.requiredString, rules.length]"></v-text-field>
              </v-col>
              <v-col>
                <v-select v-model="request.quantityPerUnit" label="Quantity Per Unit" :items="unitOptions" :rules="[rules.requiredNumber]"></v-select>
              </v-col>
              <v-col>
                <v-text-field v-model="request.reorderLevel" label="Reorder Level" variant="outlined" required type="number" :rules="[rules.requiredNumber, rules.greaterThanZero]" min=0></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field v-model="request.unitPrice" label="Unit Price" variant="outlined" required type="number" :rules="[rules.requiredNumber, rules.greaterThanZero]" min=0></v-text-field>
              </v-col>
              <v-col>
                <v-text-field v-model="request.unitsInStock" label="Units In Stock" variant="outlined" required type="number" :rules="[rules.requiredNumber, rules.greaterThanZero]" min=0></v-text-field>
              </v-col>
              <v-col>
                <v-text-field v-model="request.unitsOnOrder" label="Units On Order" variant="outlined" required type="number" :rules="[rules.requiredNumber, rules.greaterThanZero]" min=0></v-text-field>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-select v-model="request.supplierId" label="Supplier" :items="lookupsStore.suppliers" item-title="name" item-value="id" :rules="[rules.requiredNumber]"></v-select>
              </v-col>
            </v-row>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn variant="flat" type="submit" color="primary" class="mt-4">Submit</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-container>
        </v-form>
      </v-card-text>
    </v-card>
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
  </v-container>
</template>
