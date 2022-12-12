<template>
  <CreateModal @create="createAnimeObject" ref="createModalRef" :genres="genresList"/>
  <EditModal @edit="editAnimeObject" ref="editModalRef" :genres="genresList"/>
  <DeleteModal @delete="deleteAnimeObject" ref="deleteModalRef"/>

  <main>
    <b-container>
      <div class="my-4 d-flex flex-row">
        <h2 class="me-3">Список аниме</h2>
        <b-button class="me-2" variant="outline-primary" @click="onCreateClick">
          <FontAwesomeIcon icon="fas fa-plus" />
        </b-button>
      </div>
      <AnimeList @edit-anime="onEditClick" @delete-anime="onDeleteClick" :anime-list="animeList"/>
    </b-container>
  </main>
</template>

<script setup>
import {onBeforeMount, reactive, ref, watch} from "vue"
import {useRoute} from "vue-router"
import axios from "axios";

import AnimeList from "@/components/AnimeList.vue"
import AnimeService from "@/services/animeService"
import GenresService from "@/services/genresService"
import CreateModal from "@/components/Modals/CreateAnimeModal.vue"
import DeleteModal from "@/components/Modals/DeleteAnimeModal.vue"
import EditModal from "@/components/Modals/EditAnimeModal.vue"

const route = useRoute()
const genre = ref(route.query.genre)
const animeList = ref([])
const genresList = ref([])
const createModalRef = ref()
const editModalRef = ref()
const deleteModalRef = ref()

const response = reactive({
  message: '',
  variant: '',
  statusCode: 404,
  errorMessage: ''
})

async function fetchData() {
  if ('genre' in route.query){
    animeList.value = (await AnimeService.findByGenre(genre.value)).data
  } else {
    animeList.value = (await AnimeService.getAll()).data
  }
}

async function update() {
  await fetchData()
}

function onCreateClick() {
  createModalRef.value.show()
}

function onEditClick(anime) {
  editModalRef.value.show()
  editModalRef.value.handAnime(anime)
}

function onDeleteClick(animeId) {
  deleteModalRef.value.show()
  deleteModalRef.value.handAnime(animeId)
}

async function createAnimeObject(formData) {
  createModalRef.value.close()

  try {
    let res = await axios.post('/api/Anime', formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    })
    response.message = "Аниме успешно добавлено!"
    response.variant = "info"
    response.statusCode = res.status
    response.errorMessage = ''
  } catch (error) {
    response.message = "Ошибка при добавлении!"
    response.variant = "danger"
    response.statusCode = error.status
    response.errorMessage = error.message
  }

  createModalRef.value.resetForm()
  await update()
}

async function editAnimeObject(animeObject) {
  console.log(animeObject)
  try {
    let res = await axios.put(`/api/Anime/${animeObject.id}`, animeObject)
    response.message = "Аниме успешно изменёно!"
    response.variant = "info"
    response.statusCode = res.status
    response.errorMessage = ''
  } catch (error) {
    response.message = "Ошибка при изменении!"
    response.variant = "danger"
    response.statusCode = error.status
    response.errorMessage = error.message
  }
  await update()
}

async function deleteAnimeObject(animeId) {
  await axios.delete(`/api/Anime/${animeId}`)
  await update()
}

onBeforeMount(async () => {
  genresList.value = (await GenresService.getAll()).data
  await fetchData()
})

watch(route, async () => {
  genre.value = route.query.genre
  await fetchData()
})
</script>