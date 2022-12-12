<template>
  <header>
    <Navbar />
  </header>
  <main>
    <b-container>
      <div class="d-flex">
        <div class="col-8">
          <RouterView />
        </div>
        <div class="col-4 p-3">
          <p>Страницы, которые вы посещали:</p>
          <b-list-group>
            <b-list-group-item v-for="item in history">
              {{item}}
            </b-list-group-item>
          </b-list-group>
        </div>
      </div>
    </b-container>
  </main>
</template>

<script setup>
import {onBeforeMount, ref, watch} from "vue";
import {RouterView, useRoute} from 'vue-router'
import {useCookies} from "vue3-cookies";
import Navbar from "@/components/Navbar.vue";

const history = ref([])
const route = ref(useRoute())
const id = ref(route.value.params.id)

const cookies = useCookies().cookies

watch(route.value, () => {
  addRoute(route.value.fullPath)
})

function addRoute(route) {
  history.value.push(route)
  if (history.value.length > 10) {
    history.value.shift()
  }
  saveHistory()
}

function saveHistory() {
  cookies.set('history', history.value.join(', '));
}

onBeforeMount(async () => {
  if (cookies.get('history')) {
    try {
      history.value = cookies.get('history').split(', ')
    } catch(e) {
      cookies.remove('history')
    }
  }
})

</script>