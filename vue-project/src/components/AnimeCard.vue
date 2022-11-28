<template>
  <DeleteModal @delete="deleteAnimeObject" ref="deleteModalRef" :anime-object="props.anime"/>
  <EditModal ref="editModalRef" :anime-object="props.anime"/>

  <div class="p-2 position-relative">
    <b-card
        :title="anime.title"
        :img-src="anime.image"
        img-height="390"
        border-variant="light"
        bg-variant="light"
        style="width: 290px;"
        class="text-center"
    >
      <b-button variant="link" :to="{ path: `/anime/${anime.id}`, hash: '#image' }" class="card-link">Картинка</b-button>
      <b-button variant="link" :to="{ path: `/anime/${anime.id}`, hash: '#info' }" class="card-link">Описание</b-button>
      <div class="d-flex justify-content-center">
        <b-button class="me-2" variant="outline-primary" @click="onEditClick">
          <FontAwesomeIcon icon="fas fa-pen" />
        </b-button>
        <b-button variant="outline-danger" @click="onDeleteClick">
          <FontAwesomeIcon icon="fas fa-trash" />
        </b-button>
      </div>
    </b-card>
  </div>
</template>

<script setup>
import {ref} from "vue"
import DeleteModal from "@/components/DeleteAnimeModal.vue"
import EditModal from "@/components/EditAnimeModal.vue"
import AnimeService from "@/services/animeService"

const emit = defineEmits(["update"])

const deleteModalRef = ref()
const editModalRef = ref()

function onEditClick() {
  editModalRef.value.show()
}

function onDeleteClick() {
  deleteModalRef.value.show()
}

async function deleteAnimeObject(id) {
  await AnimeService.delete(id)
  emit("update")
}

const props = defineProps ({
  anime: Object
})
</script>
