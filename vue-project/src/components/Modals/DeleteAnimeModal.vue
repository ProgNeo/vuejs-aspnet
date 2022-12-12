<template>
  <Modal ref="modalRef">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Удалить {{ animeObject.title }}?</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        Отменить это действие будет невозможно
      </div>
      <div class="modal-footer">
        <b-button type="button" variant="danger" data-bs-dismiss="modal" @click="$emit('delete', animeObject.id)">Удалить</b-button>
        <b-button type="button" variant="outline-secondary" data-bs-dismiss="modal">Отмена</b-button>
      </div>
    </div>
  </Modal>
</template>

<script setup>
import Modal from '@/components/Modal.vue'
import {ref} from "vue"

const animeObject = ref(Object)

const emit = defineEmits(["delete"])

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
