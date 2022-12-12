<template>
  <CreateGenreModal @create="createGenre" ref="createModalRef"/>
  <b-container>
    <b-navbar toggleable="lg" type="light" variant="faded">
      <b-navbar-brand to="/"><FontAwesomeIcon icon="fas fa-cat" /></b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav="">
        <b-navbar-nav v-for="genre in genres">
          <b-nav-item :to="{ path: '/', query: { genre: `${genre.id}` } }">{{ genre.name }}</b-nav-item>
        </b-navbar-nav>
        <b-navbar-nav class="ms-auto">
          <b-nav-item :to="{ path: '/search' }" rigth>Поиск</b-nav-item>
          <b-nav-item @click="onCreateClick">Добавить жанр</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </b-container>
</template>

<script setup>
import {onBeforeMount, reactive, ref} from "vue"
import GenresService from "@/services/genresService"
import CreateGenreModal from "@/components/Modals/CreateGenreModal.vue";
import axios from "axios";

const genres = ref([])

const response = reactive({
  message: '',
  variant: '',
  statusCode: 404,
  errorMessage: ''
})

const createModalRef = ref()

onBeforeMount(async () => {
  await fetchData()
})

function onCreateClick() {
  createModalRef.value.show()
}

async function createGenre(genre) {
  try {
    let res = await axios.post('/api/Genres', genre)
    response.message = "Жанр успешно добавлен!"
    response.variant = "info"
    response.statusCode = res.status
    response.errorMessage = ''
  } catch (error) {
    response.message = "Ошибка при добавлении!"
    response.variant = "danger"
    response.statusCode = error.status
    response.errorMessage = error.message
  }

  createModalRef.value.close()
  await fetchData()
}

async function fetchData() {
  genres.value = (await GenresService.getAll()).data
}
</script>
