<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="light">
      <b-navbar-brand to="/"><FontAwesomeIcon icon="fas fa-cat" /></b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav="">
        <b-navbar-nav>
          <div v-for="genre in genres">
            <b-nav-item :to="{ path: '/', query: { genre: `${genre.id}` } }">{{ genre.name }}</b-nav-item>
          </div>
          <div>
            <b-nav-item :to="{ path: '/search' }">Поиск</b-nav-item>
          </div>
          <div>
            <b-nav-item :to="{ path: '/create' }">Добавить</b-nav-item>
          </div>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script setup>
import {onBeforeMount, ref} from "vue"
import axios from "axios"

const genres = ref([])

onBeforeMount(async () => {
  genres.value = (await axios.get(`/api/Genres`)).data
})
</script>