<template>
  <div>
    <b-container>
      <b-navbar toggleable="lg" type="light" variant="faded">
        <b-navbar-brand to="/"><FontAwesomeIcon icon="fas fa-cat" /></b-navbar-brand>

        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

        <b-collapse id="nav-collapse" is-nav="">
          <b-navbar-nav v-for="genre in genres">
            <b-nav-item :to="{ path: '/', query: { genre: `${genre.id}` } }">{{ genre.name }}</b-nav-item>
          </b-navbar-nav>
          <b-navbar-nav class="ms-auto">
            <b-nav-item :to="{ path: '/search' }" rigth>Поиск</b-nav-item>
          </b-navbar-nav>
        </b-collapse>
      </b-navbar>
    </b-container>
  </div>
</template>

<script setup>
import {onBeforeMount, ref} from "vue"
import GenresService from "@/services/genresService"

const genres = ref([])

onBeforeMount(async () => {
  genres.value = (await GenresService.getAll()).data
})
</script>
