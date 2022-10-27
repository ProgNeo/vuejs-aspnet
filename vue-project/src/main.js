import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import BoostrapVue from 'bootstrap-vue-3'
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import "bootstrap/dist/css/bootstrap.css"
import "bootstrap-vue-3/dist/bootstrap-vue-3.css"
import "fontawesome-free/css/fontawesome.css"

const app = createApp(App)

app.use(router)
app.use(BoostrapVue)
app.component('FontAwesomeIcon', FontAwesomeIcon)
library.add(fas)

app.mount('#app')
