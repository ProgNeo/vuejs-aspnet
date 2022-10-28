<script setup>
  const props = defineProps ({
    image: Object
  });
</script>

<template>
  <img :src="image" style="width: 300px;" alt=""/>
</template>