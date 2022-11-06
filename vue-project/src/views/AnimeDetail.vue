<template>
  {{ route }}
  {{ type }}
  <b-container>
    <div class="my-3">
      <router-link v-on:click="type = 'image'" to="#image" class="m-3">
        <b-button v-if="type === 'image'" variant="light" active>Картинка</b-button>
        <b-button v-else variant="light">Картинка</b-button>
      </router-link>
      <router-link v-on:click="type = 'info'" to="#info">
        <b-button v-if="type === 'info'" variant="light" active>Описание</b-button>
        <b-button v-else variant="light">Описание</b-button>
      </router-link>
    </div>
    <div class="d-flex flex-column border border-1 p-3">
      <div v-if="type === ''">
        <p>Нажмите на кнопку чтобы что-то узнать</p>
      </div>
      <div v-if="type === 'image'">
        <AnimeImage :image="anime.image" />
      </div>
      <div v-if="type === 'info'">
        <AnimeInfo :info="anime.info" />
      </div>
    </div>
  </b-container>
</template>

<script setup>
import { ref, onBeforeMount } from "vue";
import { useRoute } from 'vue-router'
import AnimeImage from "@/components/AnimeImage.vue";
import AnimeInfo from "@/components/AnimeInfo.vue";
import axios from "axios";

const anime = ref();
const route = ref(useRoute())
const id = ref(route.value.params.id)
const type = ref(route.value.hash)

onBeforeMount(async () => {
  anime.value = (await axios.get(`/api/Anime/${id.value}`)).data
});
</script>
