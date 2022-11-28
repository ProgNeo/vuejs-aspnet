<template>
  <Modal ref="modalRef">
    <div class="modal-header">
      <h5 class="modal-title">Изменение {{ animeObject.title }}</h5>
      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div class="modal-body">
      В разработке
    </div>
    <div class="modal-footer">
      <b-button type="button" variant="primary" data-bs-dismiss="modal">Сохранить</b-button>
      <b-button type="button" variant="outline-secondary" data-bs-dismiss="modal">Отмена</b-button>
    </div>
  </Modal>
</template>

<script setup>
import Modal from '@/components/Modal.vue'
import {ref} from "vue"
const props = defineProps({
  animeObject: Object
})
const modalRef = ref(null)
defineExpose({
  show () {
    modalRef.value.show()
  }
})
</script>