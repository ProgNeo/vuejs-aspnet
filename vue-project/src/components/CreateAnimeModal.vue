<template>
  <Modal ref="modalRef">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Добавление аниме</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form ref="createForm" @submit.prevent="$emit('create', data)" method="POST" class="row g-3 justify-content-evenly">
            <b-col sm="4">
              <input v-model="data.title" required type="text" class="form-control" placeholder="Наименование">
            </b-col>

            <b-col sm="4">
              <input v-model="data.description" type="text" class="form-control" placeholder="Краткое описание">
            </b-col>

            <b-col sm="4">
              <select v-model="data.genre" required class="form-select">
                <option v-for="genre in props.genresList" :value="genre.id">{{ genre.title }}</option>
              </select>
            </b-col>

            <b-col sm="12">
              <input ref="image" class="form-control" type="file" @change="uploadImage">
            </b-col>

            <b-col sm="12">
              <textarea v-model="data.info" placeholder="Полное описание..." class="form-control" rows="5"></textarea>
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
const props = defineProps({
  genresList: Array
})
const emit = defineEmits(["create"])
const modalRef = ref(null)
const createForm = ref(null)
const data = reactive({
  title: '',
  image: '',
  description: '',
  info: '',
  genre: 1
})
defineExpose({
  show () {
    modalRef.value.show()
  },
  close () {
    modalRef.value.close()
  },
  handGenre (id) {
    data.genre = id
  },
  resetForm () {
    createForm.value.reset()
  }
})
</script>