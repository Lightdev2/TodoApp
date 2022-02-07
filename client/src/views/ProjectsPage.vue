<template>
  <div class="projects-page">
    <div class="projects-page__container" v-if="isLoaded">
      <template v-if="projects.length > 0">
        <div class="projects-page__project" v-for="(project, idx) in projects" :key="idx">
          <h3>{{project.title}}</h3>
          <h4>Description</h4>
          <p>
            {{project.desc}}
          </p>
          <span>Created at: {{project.createdDate}}</span>
          <span>Last modified at: {{project.lastModifiedDate}}</span>
        </div>
        <div class="projects-page__project-add">
          <button class="projects-page__add-btn">
            <plus-icon class="projects-page__add-icon"/>
          </button>
        </div>
      </template>
      <div class="projects-page__no-projects" v-else>
        <h2>No projects yet</h2>
        <h3>Let add some project, just click below</h3>
        <div class="projects-page__project-add projects-page__project-add--new">
          <button class="projects-page__add-btn">
            <plus-icon class="projects-page__add-icon"/>
          </button>
        </div>
      </div>
    </div>
    <span v-else>Loading...</span>
  </div>
</template>

<script>
import { getProjects } from '@/api/api';
import { onMounted, ref, inject } from 'vue';
import PlusIcon from '@/assets/icons/plus.vue';

export default {
  name: 'ProjectsPage',
};
</script>

<script setup>
const projects = ref([]);
const bus = inject('bus');
const isLoaded = ref(false);
bus.subscribe(() => {
  const token = localStorage.getItem('Token');
  isLoaded.value = false;
  getProjects(token).then((res) => {
    projects.value = res;
    isLoaded.value = true;
  });
});
onMounted(() => {
  const token = localStorage.getItem('Token');
  getProjects(token).then((res) => {
    isLoaded.value = true;
    projects.value = res;
  });
});
</script>
<style scoped lang="scss">
.projects-page {
  &__container {
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    width: 100%;
  }

  &__project {
    cursor: pointer;
    display: flex;
    flex-direction: column;
    border-radius: 5px;
    border: 1px solid #dedede;
    padding: 10px;
    height: 300px;
    width: 300px;
    margin-right: 1rem;
    margin-bottom: 1rem;
  }
  &__add-btn {
    width: 40px;
    height: 40px;
    background: transparent;
    border: none;
  }
  &__no-projects {
    width: 100%;
    display: flex;
    max-height: 100%;
    align-items: center;
    flex-direction: column;
  }
  &__project-add {
    cursor: pointer;
    width: 300px;
    height: 300px;
    border-radius: 5px;
    border: 1px solid #dedede;
    display: flex;
    align-items: center;
    justify-content: center;
    margin-bottom: 1rem;
    &--new {
      margin-top: 1rem;
    }
  }
}
</style>
