<template>
  <b-container>
    <div class="my-3">
      <b-alert v-if="message !== ''" variant="info" show>
        {{ message }}
      </b-alert>
      <form ref="newObject" v-on:submit.prevent="submitForm" enctype="multipart/form-data" method="POST" class="row g-3">
        <div class="col-4">
          <label class="form-label">Название</label>
          <input v-model="form.title" type="text" class="form-control" required="required">
        </div>
        <div class="col-4">
          <label class="form-label">Краткое описание</label>
          <input v-model="form.description" type="text" class="form-control" required="required">
        </div>
        <div class="col-4">
          <label class="form-label">Жанр</label>
          <select v-model="form.genre" class="form-control">
            <option v-for="genre in genres" :value="`${genre.id}`">{{ genre.name }}</option>
          </select>
        </div>
        <div class="mb-3">
          <label class="form-label">Картинка</label>
          <input ref="image" class="form-control" type="file" required="required" @change="uploadImage()">
        </div>
        <div class="col-12">
          <textarea v-model="form.info" placeholder="Полное описание" class="form-control" rows="5" required="required"/>
        </div>
        <div class="col-12 text-end">
          <b-button variant="primary" type="submit">Добавить</b-button>
        </div>
      </form>
    </div>
  </b-container>
</template>
<script>
import axios from "axios";

export default {
  name: 'createObject',
  data(){
    return {
      form: {
        id: 1,
        title: '',
        image: '',
        description: '',
        info: '',
        genre: 1
      },
      message: ''
    }
  },
  methods: {
    uploadImage() {
      this.form.image = this.$refs.image.files[0]
      let fileExtension = this.form.image.name.replace(/^.*\./, "")

      let allowedExtensions = /(\.jpg|\.JPG|\.jpeg|\.JPEG|\.png)$/i;
      if (!allowedExtensions.exec(this.form.image.name)) {
        this.message = "Вы можете загрузить только файлы с расширениями: jpg, jpeg и png";
        this.$refs.image.value = "";
      }
      this.message = this.form.image
      console.log(this.message)
    },
    async submitForm() {
      this.form.id = (await axios.get('/api/Anime')).data.length + 1
      axios.post('/api/Anime', this.form)
          .then((res) => {
            this.message = "Успех!"
          })
          .catch((error) => {
            // error.response.status -> to check status code
          }).finally(() => {
            // handle action in always
          }
      );
      this.$refs.newObject.reset()
    }
  }
}
</script>
<script setup>
import {onBeforeMount, ref} from "vue"

const genres = ref([])

onBeforeMount(async () => {
  genres.value = (await axios.get(`/api/Genres`)).data
})
</script>
