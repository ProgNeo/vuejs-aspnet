<template>
  <b-container>
    <div class="my-3">
      <b-alert v-if="message !== ''" variant="info" show>
        {{ message }}
      </b-alert>
      <form ref="newObject" @submit.prevent="submitForm" enctype="multipart/form-data" method="POST" class="row g-3">
        <div class="col-4">
          <label class="form-label">Название</label>
          <input v-model="data.title" type="text" class="form-control" required="required">
        </div>
        <div class="col-4">
          <label class="form-label">Краткое описание</label>
          <input v-model="data.description" type="text" class="form-control" required="required">
        </div>
        <div class="col-4">
          <label class="form-label">Жанр</label>
          <select v-model="data.genre" class="form-control">
            <option v-for="genre in genres" :value="`${genre.id}`">{{ genre.name }}</option>
          </select>
        </div>
        <div class="mb-3">
          <label class="form-label">Картинка</label>
          <input ref="image" class="form-control" type="file" @change="uploadImage">
        </div>
        <div class="col-12">
          <textarea v-model="data.info" placeholder="Полное описание" class="form-control" rows="5" required="required"/>
        </div>
        <div class="col-12 text-end">
          <b-button variant="primary" type="submit">Добавить</b-button>
        </div>
      </form>
    </div>
  </b-container>
</template>

<script setup>
import {onBeforeMount, reactive, ref} from "vue"
import axios from "axios";
import AnimeService from "@/services/animeService"
import GenresService from "@/services/genresService"

const image =  ref()
const genres = ref([])
const message = ref('')
const data = reactive({
  title: '',
  description: '',
  info: '',
  genre: 1,
  image: File
})

function uploadImage(e) {
  data.image = e.target.files[0]
}

async function submitForm() {
  const formData = new FormData()

  formData.append('Title', data.title)
  formData.append('Genre', data.genre)
  formData.append('Description', data.description)
  formData.append('Info', data.info)
  formData.append('Image', data.image)

  let response = await axios.post('/api/Anime', formData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })

  message.value = "Успех!"
  resetForm()
}

function resetForm() {
  data.title = ''
  data.description = ''
  data.info =  ''
}

onBeforeMount(async () => {
  genres.value = (await GenresService.getAll()).data
})
</script>
