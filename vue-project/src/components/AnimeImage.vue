<template>
  <img :src="image" style="width: 300px;" alt=""/>
</template>

<script setup>
const props = defineProps ({
  image: String
});
</script>