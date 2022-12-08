<template>
  <CreateAnimeModal ref="createModalRef" :genresList="genresList"/>
  <main>
    <b-container>
      <div class="my-4 d-flex flex-row">
        <h2 class="me-3">Список аниме</h2>
        <b-button class="me-2" variant="outline-primary" @click="onCreateClick">
          <FontAwesomeIcon icon="fas fa-plus" />
        </b-button>
      </div>
      <AnimeList @update="update" :anime-list="animeList"/>
    </b-container>
  </main>
</template>

<script setup>
import AnimeList from "@/components/AnimeList.vue"
import {onBeforeMount, ref, watch} from "vue"
import AnimeService from "@/services/animeService"
import {useRoute} from "vue-router"
import CreateAnimeModal from "@/components/CreateAnimeModal.vue"
import GenresService from "@/services/genresService"

const route = useRoute()
const genre = ref(route.query.genre)
const animeList = ref([])
const genresList = ref([])
const createModalRef = ref()

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

onBeforeMount(async () => {
  genresList.value = (await GenresService.getAll()).data
  await fetchData()
})

watch(route, async () => {
  genre.value = route.query.genre
  await fetchData()
})
</script>