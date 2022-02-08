<template>
  <div class="projects-page">
    <div class="projects-page__container" v-if="isLoaded">
      <template v-if="projects.length > 0">
        <project-card
          @delete="deleteProject"
          v-for="(project,idx) in projects"
          :project="project"
          :key="idx"
        />
        <add-project-card @create="addProject"/>
      </template>
      <div class="projects-page__no-projects" v-else>
        <h2>No projects yet</h2>
        <h3>Let add some project, just click below</h3>
        <add-project-card @create="addProject"/>
      </div>
    </div>
    <span v-else>Loading...</span>
  </div>
</template>

<script>
import { getProjects, deleteProjectAsync, createProjectAsync } from '@/api/api';
import { onMounted, ref, inject } from 'vue';
import AddProjectCard from '@/components/AddProjectCard.vue';
import ProjectCard from '@/components/ProjectCard.vue';

export default {
  name: 'ProjectsPage',
  components: {
    AddProjectCard,
    ProjectCard,
  },
};
</script>

<script setup>
const projects = ref([]);
const bus = inject('bus');
const isLoaded = ref(false);
bus.subscribe(() => {
  const token = localStorage.getItem('Token');
  isLoaded.value = false;
  getProjects(token)
    .then((res) => {
      projects.value = res;
      isLoaded.value = true;
    });
});
const deleteProject = (id) => {
  const token = localStorage.getItem('Token');
  deleteProjectAsync(token, String(id))
    .then((res) => {
      console.log(res);
      isLoaded.value = false;
      getProjects(token)
        .then((res) => {
          isLoaded.value = true;
          projects.value = res;
        });
    });
};
const addProject = async (project) => {
  const token = localStorage.getItem('Token');
  await createProjectAsync(token, project);
};
onMounted(() => {
  const token = localStorage.getItem('Token');
  getProjects(token)
    .then((res) => {
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

  &__no-projects {
    width: 100%;
    display: flex;
    max-height: 100%;
    align-items: center;
    flex-direction: column;

    & > h3 {
      margin-bottom: 1rem;
    }
  }

  &_add-wrapper {
    display: flex;
    flex-direction: column;
  }

  &__input {
    display: flex;
    flex-direction: column;
  }
}
</style>
