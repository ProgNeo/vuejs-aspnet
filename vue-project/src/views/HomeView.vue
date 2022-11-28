<template>
  <main>
    <b-container>
      <AnimeList @update="update" :anime-list="animeList"/>
    </b-container>
  </main>
</template>

<script setup>
import AnimeList from "@/components/AnimeList.vue";
import {onBeforeMount, ref, watch} from "vue";
import AnimeService from "@/services/animeService";
import {useRoute} from "vue-router";

const route = useRoute()
const genre = ref(route.query.genre)
const animeList = ref([])

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

onBeforeMount(async () => {
  await fetchData()
})

watch(route, async () => {
  genre.value = route.query.genre
  await fetchData()
})
</script>