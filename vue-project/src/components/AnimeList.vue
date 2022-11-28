<template>
  <b-card-group class="d-flex justify-content-center">
    <AnimeCard @update="update" v-for="anime in props.animeList" :anime="anime" />
  </b-card-group>
</template>

<script setup>
import AnimeCard from "@/components/AnimeCard.vue"

const props = defineProps({
  animeList: Array
})

const emit = defineEmits(["update"])

function update() {
  emit("update")
}
</script>