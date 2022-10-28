<script setup>
  defineProps ({
    anime: Object
  })
</script>

<template>
  <div>
    <b-card
        :title="anime.title"
        :img-src="anime.image"
        img-height="400"
        border-variant="light"
        bg-variant="light"
        style="width: 300px;"
        class="text-center m-2"
    >
      <b-button variant="link" :to="`/anime/${anime.id}?show=image`" class="card-link">Картинка</b-button>
      <b-button variant="link" :to="`/anime/${anime.id}?show=info`" class="card-link">Описание</b-button>
    </b-card>
  </div>
</template>