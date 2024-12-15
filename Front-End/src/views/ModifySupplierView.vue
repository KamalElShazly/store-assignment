<script setup lang="ts">
import { type ModifySupplierRequest } from '@/types/ModifySupplierRequest';
import { computed, onMounted, ref, toRaw, type Ref } from 'vue';
import { useSupplierStore } from '@/stores/supplier'
import router from '@/router';

const props = defineProps({
  id: Number
})

const store = useSupplierStore()

const showErrorDialog = ref(false)
const errorDialogMessage = ref('')

const isFormValid = ref(false)

const supplierId: Ref<number | null> = ref(null);
const isEditing = computed(() => supplierId.value != null)

const request: Ref<ModifySupplierRequest> = ref({
  name: '',
});

const rules = {
  required: (value: string) => !!value || 'Name is required',
  length: (value: string) => value?.length <= 100 || 'Name must be 100 characters or less',
};

const submit = async () => {
  if (isFormValid.value) {
    if (supplierId.value == null) {
      await store.createSupplier(request.value);
    } else {
      await store.updateSupplier(supplierId.value, request.value);
    }
    if (store.error) {
      openErrorDialog("Something went wrong, Please try again later")
    } else {
      router.push({ name: 'suppliers' })
    }
  }
};

onMounted(async () => {
  const id = toRaw(props).id
  if (id) {
    supplierId.value = id
    const supplier = await store.getSupplier(id)
    if (store.error) {
      openErrorDialog("Something went wrong, Please try again later")
    } else if (supplier) {
      request.value.name = supplier?.name
    } else {
      openErrorDialog("Something went wrong, Please try again later")
    }
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
  <v-container class="mt-4">
    <v-card>
      <v-card-title>
        <h2>{{ isEditing ? 'Edit Supplier' : 'Add Supplier' }}</h2>
      </v-card-title>
      <v-card-text>
        <v-form v-model="isFormValid" @submit.prevent="submit">
          <v-container>
            <v-row>
              <v-col>
                <v-text-field v-model="request.name" label="Name" variant="outlined" required :rules="[rules.required, rules.length]"></v-text-field>
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
