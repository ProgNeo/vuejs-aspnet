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
          <input ref="image" class="form-control" type="file" @change="uploadImage()">
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

const genres = ref([])
const message = ref('')
const data = reactive({
  title: '',
  image: '',
  description: '',
  info: '',
  genre: 1
})

function uploadImage() {
  data.image = this.$refs.image.files[0]
  let fileExtension = data.image.name.replace(/^.*\./, "")

  let allowedExtensions = /(\.jpg|\.JPG|\.jpeg|\.JPEG|\.png)$/i;
  if (!allowedExtensions.exec(data.image.name)) {
    message.value = "Вы можете загрузить только файлы с расширениями: jpg, jpeg и png";
    this.$refs.image.value = "";
  }
  message.value = data.image
}

async function submitForm() {
  data.image = 'https://dere.shikimori.one/system/animes/original/13629.jpg?1635226170'
  let request = await axios.post('/api/Anime', data)
  message.value = "Успех!"
  resetForm()
}

function resetForm() {
  data.title = ''
  data.image = ''
  data.description = ''
  data.info =  ''
}

onBeforeMount(async () => {
  genres.value = (await axios.get(`/api/Genres`)).data
})
</script>
