import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import NotFound from '../views/NotFound.vue'
import AnimeDetail from '../views/AnimeDetail.vue'

function removeQueryParams(to) {
  if (Object.keys(to.query).length) {
    return {path: to.path, query: {}, hash: to.hash}
  }
}

function removeHash(to) {
  if (to.hash) return { path: to.path, query: to.query, hash: '' }
}

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/anime/:id(\\d+)',
      name: 'anime-detail',
      component: AnimeDetail,
      params: true
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'not-found',
      component: NotFound
    }
  ]
})

export default router
