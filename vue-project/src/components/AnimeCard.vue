<template>
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
      <div v-if="authStore.canAction('Moderator')" class="d-flex justify-content-center">
        <b-button class="me-2" variant="outline-primary" @click="onEditClick({...props.anime})">
          <FontAwesomeIcon icon="fas fa-pen" />
        </b-button>
        <b-button variant="outline-danger" @click="onDeleteClick(props.anime)">
          <FontAwesomeIcon icon="fas fa-trash" />
        </b-button>
      </div>
    </b-card>
  </div>
</template>

<script setup>
import useAuthenticationStore from "@/store/authenticationStore";

const authStore = useAuthenticationStore()
const emit = defineEmits(['delete-anime-click', 'edit-anime-click'])

function onEditClick(animeObject) {
  emit("edit-anime-click", animeObject)
}

function onDeleteClick(animeObject) {
  emit("delete-anime-click", animeObject)
}

const props = defineProps ({
  anime: Object
})
</script>
