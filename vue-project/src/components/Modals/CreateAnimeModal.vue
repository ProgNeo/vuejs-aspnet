<template>
  <Modal ref="modalRef">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Добавление аниме</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form ref="createForm" @submit.prevent="$emit('create', anime)" method="POST" class="row g-3 justify-content-evenly">
          <b-col sm="4">
            <input v-model="anime.title" required type="text" class="form-control" placeholder="Наименование">
          </b-col>

          <b-col sm="4">
            <input v-model="anime.description" type="text" class="form-control" placeholder="Краткое описание">
          </b-col>

          <b-col sm="4">
            <select v-model="anime.genre" class="form-select">
              <option v-for="genre in props.genres" :value="genre.id">{{ genre.name }}</option>
            </select>
          </b-col>

          <b-col sm="12">
            <input ref="image" class="form-control" type="file" @change="uploadImage">
          </b-col>

          <b-col sm="12">
            <textarea v-model="anime.info" placeholder="Полное описание..." class="form-control" rows="5"></textarea>
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
  genres: Array
})
const emit = defineEmits(["create"])
const modalRef = ref(null)
const createForm = ref(null)
const anime = reactive({
  title: '',
  description: '',
  info: '',
  genre: 1,
  image: File
})
defineExpose({
  show () {
    modalRef.value.show()
  },
  close () {
    modalRef.value.close()
  },
  handGenre (id) {
    anime.genre = id
  },
  resetForm () {
    createForm.value.reset()
  }
})
function uploadImage(e) {
  anime.image = e.target.files[0]
}
</script>
