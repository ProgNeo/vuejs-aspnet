<template>
  <b-card-group class="d-flex justify-content-center">
    <AnimeCard @delete-anime-click="onDeleteClick" @edit-anime-click="onEditClick" v-for="anime in props.animeList" :anime="anime" />
  </b-card-group>
</template>

<script setup>
import AnimeCard from "@/components/AnimeCard.vue"

const props = defineProps({
  animeList: Array
})

const emit = defineEmits(['delete-anime', 'edit-anime'])

function onEditClick(animeObject) {
  emit("edit-anime", animeObject)
}

function onDeleteClick(animeId) {
  emit("delete-anime", animeId)
}
</script>