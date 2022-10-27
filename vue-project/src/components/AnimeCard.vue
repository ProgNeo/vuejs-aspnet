<script setup>
  defineProps ({
    anime: Object
  })
</script>

<template>
  <div>
    <b-card body-class="text-center">
      <div class="h-75 overflow-hidden">
        <b-card-img :src="anime.image" alt="Image" ></b-card-img>
      </div>
      <b-button variant="primary">{{ anime.title }}</b-button>
      <a href="#" class="card-link">Картинка</a>
      <b-link href="#" class="card-link">Описание</b-link>
    </b-card>
  </div>
</template>