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
  <div>
    <b-card-group deck>
      <div v-for="anime in animeObjects">
        <AnimeCard :anime="anime" />
      </div>
    </b-card-group>
  </div>
</template>