import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import NotFound from '../views/NotFound.vue'
import AnimeDetail from '../views/AnimeDetail.vue'
import SearchView from '../views/SearchView.vue'
import useAuthenticationStore from "@/store/authenticationStore";

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
      path: '/anime/:id',
      name: 'anime-detail',
      component: AnimeDetail,
      params: true,

    },
    {
      path: '/:pathMatch(.*)*',
      name: 'not-found',
      component: NotFound
    },
    {
      path: '/search',
      name: 'search',
      component: SearchView
    },
    {
      path: '/authorization',
      name: 'authorization',
      component: () => import('@/views/AuthorizationView.vue'),
      meta: {
        authorization: false
      }
    },
    {
      path: '/registration',
      name: 'registration',
      component: () => import('@/views/RegistrationView.vue'),
      meta: {
        authorization: false
      }
    },
  ]
})

router.beforeEach(async (to, from, next) => {
  const authStore = await useAuthenticationStore()

  if (to.meta.authorization !== undefined && authStore.checkAuth !== to.meta.authorization) {
    next({
      path: "/"
    })
    return
  }
  next()
})

export default router
