import { createRouter, createWebHashHistory } from 'vue-router';
import LoginPage from '@/views/LoginPage.vue';
import RegisterPage from '@/views/RegisterPage.vue';
import Home from '@/views/Home.vue';
import SettingsPage from '@/views/SettingsPage.vue';
import OverviewPage from '@/views/OverviewPage.vue';
import StatsPage from '@/views/StatsPage.vue';
import CalendarPage from '@/views/CalendarPage.vue';
import ProjectsPage from '@/views/ProjectsPage.vue';
import ProjectPage from '@/views/ProjectPage.vue';
import store from '@/store';

const routes = [
  {
    path: '/login',
    name: 'login',
    component: LoginPage,
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterPage,
  },
  {
    path: '/home',
    name: 'home',
    alias: '/',
    component: Home,
    children: [
      {
        path: '/settings',
        name: 'settings',
        component: SettingsPage,
      },
      {
        path: '/overview',
        name: 'overview',
        component: OverviewPage,
      },
      {
        path: '/statistics',
        name: 'stats',
        component: StatsPage,
      },
      {
        path: '/calendar',
        name: 'calendar',
        component: CalendarPage,
      },
      {
        path: '/projects',
        name: 'projects',
        component: ProjectsPage,
      },
      {
        path: '/project/:id',
        name: 'project',
        component: ProjectPage,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

router.afterEach((to) => {
  const { isLoggedIn } = store.getters;
  if ((isLoggedIn === false && (to.name !== 'login' && to.name !== 'register')) === true) {
    router.push({
      name: 'login',
    });
  }
});

export default router;
