<template>
<div class="homepage">
  <SideBar class="homepage__sidebar">
    <h2 class="homepage__subtitle">TodoApp</h2>
    <div class="homepage__sidebar-content">
      <div>
        <ul class="homepage__sidebar-list">
          <li>
            <router-link :to="{ name: 'overview' }" class="homepage__link">
              <home-icon class="homepage__sidebar-icon"/>
              <span>Overview</span>
            </router-link>
          </li>
<!--          <li>-->
<!--            <router-link :to="{ name: 'stats' }" class="homepage__link">-->
<!--              <stats-icon class="homepage__sidebar-icon"/>-->
<!--              <span>Stats</span>-->
<!--            </router-link>-->
<!--          </li>-->
          <li>
            <router-link :to="{ name: 'projects' }" class="homepage__link">
              <projects-icon class="homepage__sidebar-icon"/>
              <span>Projects</span>
            </router-link>
          </li>
          <li>
            <router-link :to="{ name: 'calendar' }" class="homepage__link">
              <calendar-icon class="homepage__sidebar-icon"/>
              <span>Calendar</span>
            </router-link>
          </li>
        </ul>
      </div>
      <div>
        <ul class="homepage__sidebar-list">
          <li>
            <router-link :to="{ name: 'settings' }" class="homepage__link">
              <gear-icon class="homepage__sidebar-icon"/>
              <span>Settings</span>
            </router-link>
          </li>
          <li>
            <a @click="logout" class="homepage__link">
              <logout-icon class="homepage__sidebar-icon"/>
              <span>Logout</span>
            </a>
          </li>
        </ul>
      </div>
    </div>
  </SideBar>
  <div class="homepage__page">
    <top-app-bar-view class="top"/>
    <div class="page">
      <router-view></router-view>
    </div>
  </div>
</div>
</template>

<script>
import SideBar from '@/components/SideBar.vue';
import GearIcon from '@/assets/icons/gear.vue';
import ProjectsIcon from '@/assets/icons/overview.vue';
//  import StatsIcon from '@/assets/icons/stats.vue';
import LogoutIcon from '@/assets/icons/logout.vue';
import CalendarIcon from '@/assets/icons/calendar.vue';
import HomeIcon from '@/assets/icons/home.vue';
import TopAppBarView from '@/components/TopAppBarView.vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';

export default {
  name: 'Home',
  components: {
    TopAppBarView,
    ProjectsIcon,
    SideBar,
    GearIcon,
    //  StatsIcon,
    LogoutIcon,
    CalendarIcon,
    HomeIcon,
  },
};
</script>

<script setup>

const store = useStore();
const router = useRouter();
const logout = () => {
  store.dispatch('signOut');
  router.push({
    name: 'login',
  });
};
</script>

<style scoped lang="scss">
.homepage {
  display: flex;
  &__sidebar {
    width: 100%;
    padding: 10px 20px 0px 20px;
  }
  &__subtitle {
    margin-bottom: 10px;
  }
  &__sidebar-list {
    list-style: none;
  }
  &__sidebar-icon {
    max-width: 25px;
    display: block;
    margin-right: 10px;
  }
  &__sidebar-content {
    margin-top: 1rem;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 90vh;
  }
  &__link {
    display: flex;
    margin-bottom: 1rem;
    align-items: center;
    text-decoration: none;
    color: #000;
    cursor: pointer;
  }
  &__page {
    width: 100%;
    height: 100%;
  }
}
.page {
  padding: 10px  20px 0 20px;
}
.top {
  border-bottom: 1px solid #dedede;
  padding: 15px 20px;
}
</style>
