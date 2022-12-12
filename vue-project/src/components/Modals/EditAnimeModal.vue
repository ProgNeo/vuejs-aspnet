<template>
  <Modal ref="modalRef">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Изменение {{ animeObject.title }}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <form ref="editForm" @submit.prevent="$emit('edit', animeObject)" method="POST" class="row g-3 justify-content-evenly">
          <b-col sm="4">
            <input v-model="animeObject.title" required type="text" class="form-control" placeholder="Наименование">
          </b-col>

          <b-col sm="4">
            <input v-model="animeObject.description" type="text" class="form-control" placeholder="Краткое описание">
          </b-col>

          <b-col sm="4">
            <select v-model="animeObject.genre" class="form-select">
              <option v-for="genre in props.genres" :value="genre.id">{{ genre.title }}</option>
            </select>
          </b-col>

          <b-col sm="12">
            <textarea v-model="animeObject.info" placeholder="Полное описание..." class="form-control" rows="5"></textarea>
          </b-col>

          <b-col sm="12" class="text-end">
            <b-button type="submit" variant="outline-primary" data-bs-dismiss="modal">Изменить</b-button>
            <b-button class="ms-2" type="button" variant="outline-secondary" data-bs-dismiss="modal">Отмена</b-button>
          </b-col>
        </form>
      </div>
    </div>
  </Modal>
</template>

<script setup>
import Modal from '@/components/Modal.vue'
import {ref} from "vue"

const props = defineProps({
  genres: Array
})

const animeObject = ref(Object)
const emit = defineEmits(["edit"])
const modalRef = ref(null)
defineExpose({
  show () {
    modalRef.value.show()
  },
  handAnime (object) {
    animeObject.value = object
  }
})
</script>