<template>
  <b-container>
    <div class="my-3">
      <router-link to="#image" class="m-3">
        <b-button v-if="type === '#image'" variant="light" active>Картинка</b-button>
        <b-button v-else variant="light">Картинка</b-button>
      </router-link>
      <router-link to="#info">
        <b-button v-if="type === '#info'" variant="light" active>Описание</b-button>
        <b-button v-else variant="light">Описание</b-button>
      </router-link>
    </div>
    <div class="d-flex flex-column border border-1 p-3">
      <div v-if="type === ''">Нажмите на кнопку чтобы что-то узнать</div>
      <AnimeImage v-else-if="type === '#image'" :image="anime.image" />
      <AnimeInfo v-else-if="type === '#info'" :info="anime.info" />
    </div>
  </b-container>
</template>

<script setup>
import {ref, onBeforeMount, watch} from "vue"
import {useRoute} from 'vue-router'
import AnimeImage from "@/components/AnimeImage.vue"
import AnimeInfo from "@/components/AnimeInfo.vue"
import AnimeService from "@/services/animeService"

const anime = ref();
const route = ref(useRoute())
const id = ref(route.value.params.id)
const type = ref('')

watch(route.value, () => {
  type.value = route.value.hash
})

onBeforeMount(async () => {
  anime.value = (await AnimeService.get(id.value)).data
  type.value = route.value.hash
});
</script>
