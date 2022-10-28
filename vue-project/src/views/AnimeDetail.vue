<script setup>
  import { ref, onBeforeMount } from "vue";
  import { useRoute } from 'vue-router'
  import AnimeImage from "@/components/AnimeImage.vue";
  import AnimeInfo from "@/components/AnimeInfo.vue";

  const anime = ref();
  const route = ref(useRoute())
  const id = ref(route.value.params.id)
  const type = ref(route.value.query.show)

  onBeforeMount(async () => {
    const properties = {
      method: 'GET',
      mode: 'cors',
      headers: {
        "Content-Type": "application/json",
      }
    };

    anime.value = await fetch(`/api/Anime/${id.value}`, properties)
        .then(response => response.json());
  });
</script>
<template>
  <b-container>
    <div class="my-3">
      <router-link v-on:click="type = 'image'" to="?show=image" class="m-3">
        <b-button v-if="type === 'image'" variant="light" active>Картинка</b-button>
        <b-button v-else variant="light">Картинка</b-button>
      </router-link>
      <router-link v-on:click="type = 'info'" to="?show=info">
        <b-button v-if="type === 'info'" variant="light" active>Описание</b-button>
        <b-button v-else variant="light">Описание</b-button>
      </router-link>
    </div>
    <AnimeImage v-if="type === 'image'" :image="anime.image" />
    <AnimeInfo v-if="type === 'info'" :info="anime.info" />
  </b-container>
</template>