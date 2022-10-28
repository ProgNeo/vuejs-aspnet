<script setup>
  import {onBeforeMount, ref} from "vue";
  import AnimeCard from "@/components/AnimeCard.vue";

  const animeObjects = ref([]);

  onBeforeMount(async () => {
    const properties = {
      method: 'GET',
      mode: 'cors',
      headers: {
        "Content-Type": "application/json",
      }
    };

    animeObjects.value = await fetch("/api/Anime", properties)
        .then(response => response.json());
  });
</script>

<template>
  <b-card-group>
    <AnimeCard v-for="anime in animeObjects" :anime="anime" />
  </b-card-group>
</template>