<template>
  <!-- Modal -->
  <div class="modal fade" ref="modalRef" tabindex="-1" aria-labelledby="templateModal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <slot></slot>
      </div>
    </div>
  </div>
</template>

<script setup>
import {ref, onMounted} from "vue"
import {Modal} from "bootstrap"
const modalRef = ref()
let modalInstance = null
onMounted(() => {
  modalInstance = new Modal(modalRef.value)
})
function show() {
  modalInstance.show()
}
defineExpose({
  show
})
</script>
