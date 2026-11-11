import CreateAccountView from '@/views/CreateAccountView.vue'
import HomeView from '@/views/HomeView.vue'
import LoginView from '@/views/LoginView.vue'
import { createRouter, createWebHistory } from 'vue-router'
import AuthHomeView from '@/views/auth/HomeView.vue'
import PublicLayout from '@/layouts/PublicLayout.vue'
import AppLayout from '@/layouts/AppLayout.vue'
import AuthFormLayout from '@/layouts/AuthFormLayout.vue'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: PublicLayout,
      children: [
        {path: '', name: 'home', component: HomeView, meta: {hideNav: false}},
        
      ],
    },
    {
      path: '/',
      component: AuthFormLayout,
      children: [
        {path: 'login', name: 'login', component: LoginView, meta: {hideNav: true}},
        {path: 'create-account', name: 'create-account', component: CreateAccountView, meta:{hideNav: true}}
      ]
    },
    {
      path: '/:publicId',
      //path: '/auth',
      component: AppLayout,
      meta: {requiresAuth: true},
      children: [
        {path: 'home', name: 'auth-home', component: AuthHomeView}
      ]
    }
  ],
});

router.beforeEach((to, next) => {
  const authStore = useAuthStore();
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    router.push('login');
  } else {
    next;
  }
});

export default router
