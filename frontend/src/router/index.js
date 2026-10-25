import CreateAccountView from '@/views/CreateAccountView.vue'
import HomeView from '@/views/HomeView.vue'
import LoginView from '@/views/LoginView.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {path: '/', component: HomeView, meta: {hideNav: false}},
    {path: '/login', component: LoginView, meta: {hideNav: true}},
    {path: '/create-account', component: CreateAccountView, meta: {hideNav: true}}
  ],
})

export default router
