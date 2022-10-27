<script setup>
  import {onBeforeMount, ref} from "vue";

  const genres = ref([]);

  onBeforeMount(async () => {
    const properties = {
      method: 'GET',
      mode: 'cors',
      headers: {
        "Content-Type": "application/json",
      }
    };

    genres.value = await fetch("/api/Genres", properties)
        .then(response => response.json());
  });
</script>

<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="light">
      <b-navbar-brand href="#"><FontAwesomeIcon icon="fas fa-cat" /></b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav="">
        <b-navbar-nav>
          <div v-for="genre in genres">
            <b-nav-item href="#">{{ genre.name }}</b-nav-item>
          </div>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>