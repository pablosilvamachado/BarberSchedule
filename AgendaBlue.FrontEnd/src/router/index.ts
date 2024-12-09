import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router' // Importação do tipo
import LoginView from '../views/LoginView.vue'
import HomeView from '../views/HomeView.vue'
import ScheduleView from '../views/ScheduleView.vue'
import ProfessionalsView from '../views/ProfessionalsView.vue'
import ClientsView from '../views/ClientsView.vue'
import ServicesView from '../views/ServicesView.vue'
import ScheduleSettingsView from '@/views/AgendamentoView.vue'

const isAuthenticated = (): boolean => {
  return !!localStorage.getItem('authToken')
}

// Middleware de autenticação
const requireAuth = (to: any, from: any, next: any) => {
  if (!isAuthenticated()) {
    next('/login') // Redireciona para a tela de login se não estiver autenticado
  } else {
    next() // Permite acesso à rota
  }
}

// Definição das rotas
const routes: Array<RouteRecordRaw> = [
  {
    path: '/login',
    name: 'Login',
    component: LoginView,
  },
  {
    path: '/',
    name: 'Home',
    component: HomeView,
    beforeEnter: requireAuth, // Protege a rota com middleware
  },
  {
    path: '/schedule',
    name: 'Schedule',
    component: ScheduleView,
    beforeEnter: requireAuth, // Protege a rota
  },
  {
    path: '/professionals',
    name: 'Professionals',
    component: ProfessionalsView,
    beforeEnter: requireAuth, // Protege a rota
  },
  {
    path: '/clients',
    name: 'Clients',
    component: ClientsView,
    beforeEnter: requireAuth, // Protege a rota
  },
  {
    path: '/services',
    name: 'Services',
    component: ServicesView,
    beforeEnter: requireAuth, // Protege a rota
  },
  {
    path: '/schedule-settings',
    name: 'ScheduleSettings',
    component: ScheduleSettingsView,
    beforeEnter: requireAuth, // Protege a rota
  },
  {
    path: '/:pathMatch(.*)*', // Rota para páginas não encontradas
    redirect: '/login',
  },
]

// Configuração do router
const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
