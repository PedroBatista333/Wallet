import Vue from 'vue'
import VueRouter from 'vue-router'

import Vuetify from 'vuetify'

import 'vuetify/dist/vuetify.min.css'

import App from './Components/App.vue'

Vue.use(Vuetify)
Vue.use(VueRouter)

const routes = [
    //{ path: '/', component: HUDashboard }
]

const router = new VueRouter({
    routes
})

const app = new Vue({
    router,
    render: h => h(App)
}).$mount('#app')