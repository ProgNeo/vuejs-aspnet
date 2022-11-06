<template>
  <b-card-group>
    <AnimeCard v-for="anime in animeObjects" :anime="anime" />
  </b-card-group>
</template>

<script setup>
import {onBeforeMount, ref, watch} from "vue";
import AnimeCard from "@/components/AnimeCard.vue";
import axios from "axios";
import {useRoute} from "vue-router";

const route = useRoute()
const animeObjects = ref([]);
const genre = ref(route.query.genre)

watch(route, () => {
  genre.value = route.query.genre
  getData()
})

onBeforeMount(async () => {
  await getData()
});

async function getData() {
  if ('genre' in route.query){
    animeObjects.value = (await axios.get(`/api/Anime/Genre/${genre.value}`)).data
  } else {
    animeObjects.value = (await axios.get('/api/Anime/')).data
  }
}
</script>
