<template>
  <Modal ref="modalRef">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Добавление жанра</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>

      <div class="modal-body">
        <form ref="createForm" @submit.prevent="$emit('create', genre)" method="POST" class="row g-3 justify-content-evenly">
          <b-col>
            <input v-model="genre.name" required type="text" class="form-control" placeholder="Наименование">
          </b-col>

          <b-col sm="12" class="text-end">
            <b-button type="submit" variant="outline-primary">Добавить</b-button>
            <b-button class="ms-2" type="button" variant="outline-secondary" data-bs-dismiss="modal">Отмена</b-button>
          </b-col>
        </form>
      </div>
    </div>
  </Modal>
</template>

<script setup>
import Modal from '@/components/Modal.vue'
import {reactive, ref} from "vue"

const emit = defineEmits(["create"])
const modalRef = ref(null)
const createForm = ref(null)
const genre = reactive({
  name: ''
})
defineExpose({
  show () {
    modalRef.value.show()
  },
  close () {
    modalRef.value.close()
  },
  resetForm () {
    createForm.value.reset()
  }
})
</script>