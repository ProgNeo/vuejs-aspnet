<template>
  <b-container>
    <div class="row my-3 gx-3 gy-2 align-items-end">
      <div class="col-4">
        <label class="visually">Жанр</label>
        <select v-model="genre" class="form-select">
          <option value="0" selected>Все</option>
          <option value="1">Экшн</option>
          <option value="2">Психологическое</option>
          <option value="3">Драма</option>
        </select>
      </div>
      <div class="col-4">
        <label class="visually">Название</label>
        <input v-model="title" type="text" class="form-control" placeholder="Название">
      </div>
      <div class="col-4">
        <label class="visually">Описание</label>
        <input v-model="info" type="text" class="form-control" placeholder="Описание">
      </div>
    </div>
    <b-list-group>
      <b-list-group-item v-for="anime in filteredAnimeObjects">
        <router-link :to="{ path: `/anime/${anime.id}` }">{{ anime.title }}</router-link>
      </b-list-group-item>
    </b-list-group>
  </b-container>
</template>

<script setup>
import {onBeforeMount, ref, watch} from "vue";
import axios from "axios";

const animeObjects = ref([])
const filteredAnimeObjects = ref([])

const title = ref('')
const info =  ref('')
const genre = ref('0')

watch(title, async () => {
  await filterData()
})

watch(info, async () => {
  await filterData()
})

watch(genre, async () => {
  await filterData()
})

onBeforeMount(async () => {
  await getData()
})

function filterData() {
  filteredAnimeObjects.value = animeObjects.value.filter(anime =>
      (genre.value === '0' || anime.genre.toString() === genre.value) &&
      anime.title.toLowerCase().includes(title.value.toLowerCase()) &&
      anime.info.toLowerCase().includes(info.value.toLowerCase())
  )
}

async function getData() {
  animeObjects.value = (await axios.get(`/api/Anime`)).data
  filteredAnimeObjects.value = animeObjects.value
}
</script>