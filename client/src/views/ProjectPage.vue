<template>
  <div class="project">
    <h1>Project</h1>
    <button @click="isShowModal = !isShowModal">Add task</button>
    <modal-component :is-show-modal="isShowModal">
      <template #head>
        <h3>Add new task into project</h3>
      </template>
      <template #content>
        <div>
          <input v-model.trim="newTodo.title" type="text">
          <textarea v-model.trim="newTodo.desc"></textarea>
          <Calendar v-if="showCalendar" lang="ENG" :date="new Date()"/>
          <button @click="createTask">Create</button>
        </div>
      </template>
    </modal-component>
    <div class="project__dashboard">
      <div class="project__column">
        <h2 class="project__column-title">Not finished</h2>
        <draggable
          class="project__column-drag"
          v-model="inProgress"
          group="people"
          item-key="id">
          <template #item="{element}">
            <div class="project__todo">
              <p>{{ element.title }}</p>
              <p>{{ element.desc }}</p>
            </div>
          </template>
        </draggable>
        <div class="project__todo--mock"></div>
      </div>
      <div class="project__column">
        <h2 class="project__column-title">Finished</h2>
        <draggable
          class="project__column-drag"
          v-model="finished"
          group="people"
          item-key="id">
          <template #item="{element}">
            <div class="project__todo">
              <p>{{ element.title }}</p>
              <p>{{ element.desc }}</p>
            </div>
          </template>
        </draggable>
      </div>
    </div>
  </div>
</template>

<script>
import draggable from 'vuedraggable';
import ModalComponent from '@/components/ModalComponent.vue';
import Calendar from '@/components/Calendar/Calendar.vue';
import {
  ref,
  onMounted,
  inject,
  watch,
} from 'vue';
import { useRouter } from 'vue-router';
import { createTaskAsync, getTodosInProjectAsync, updateTodoAsync } from '@/api/api';

export default {
  name: 'ProjectPage',
  components: {
    draggable,
    Calendar,
    ModalComponent,
  },
};
</script>

<script setup>

const router = useRouter();
const projId = router.currentRoute.value.params.id;
const myArray = ref([]);
const finished = ref([]);
const inProgress = ref([]);
const showCalendar = ref(false);
const isShowModal = ref(false);
const newTodo = ref({
  title: '',
  desc: '',
  projectId: projId,
});
const bus = inject('bus');
bus.subscribe(() => {
  const token = localStorage.getItem('Token');
  getTodosInProjectAsync(token, {
    id: projId,
    start: 0,
    end: 100,
  })
    .then((res) => {
      myArray.value = res;
    });
});
const start = ref(0);
const end = ref(50);
const stop = ref(false);
onMounted(() => {
  const token = localStorage.getItem('Token');
  const options = {
    rootMargin: '0px',
    threshold: 1.0,
  };
  const callback = (entries, observer) => {
    if (stop.value) return;
    getTodosInProjectAsync(token, {
      id: projId,
      start: start.value += 50,
      end: end.value += 50,
    })
      .then((res) => {
        if (res.length < 50) {
          stop.value = true;
        }
        myArray.value = res;
      });
  };
  const observer = new IntersectionObserver(callback, options);
  getTodosInProjectAsync(token, {
    id: projId,
    start: 0,
    end: 50,
  })
    .then((res) => {
      // eslint-disable-next-line no-restricted-syntax
      for (const el of res) {
        if (el.isFinished === true) {
          finished.value.push(el);
        } else {
          inProgress.value.push(el);
        }
      }
    });
  const elem = document.querySelector('.project__todo--mock');
  setTimeout(() => {
    observer.observe(elem);
  }, 1500);
});

watch(finished, (_new, _old) => {
  const seta = new Set(_new);
  const setb = new Set(_old);
  const result = Array.from(diff(seta, setb));
  if (result.length === 0) return;
  const token = localStorage.getItem('Token');
  result[0].isFinished = true;
  updateTodoAsync(token, result[0])
    .then((res) => {
      console.log(res);
    });
});

watch(inProgress, (_new, _old) => {
  const seta = new Set(_new);
  const setb = new Set(_old);
  const result = Array.from(diff(seta, setb));
  const token = localStorage.getItem('Token');
  if (result.length === 0) return;
  result[0].isFinished = false;
  updateTodoAsync(token, result[0])
    .then((res) => {
      console.log(res);
    });
});
const createTask = () => {
  const token = localStorage.getItem('Token');
  createTaskAsync(token, newTodo.value)
    .then((res) => {
      console.log(res);
    });
};

const diff = (setA, setB) => {
  const difference = new Set(setA);
  // eslint-disable-next-line no-restricted-syntax
  for (const elem of setB) {
    difference.delete(elem);
  }
  return difference;
};

</script>
<style scoped lang="scss">
.project {
  &__column {
    min-height: 100px;
    border-radius: 5px;
    background-color: var(--grey);
    flex-grow: 1;
    padding: 5px;
    margin-right: 1rem;
  }

  &__column-drag {
    min-height: 100px;
  }

  &__todo {
    cursor: grab;
    height: 100px;
    border-radius: 5px;
    background-color: #fff;
    border: 1px solid #dedede;
    margin-bottom: 1rem;
    padding: 5px;
    box-shadow: 0 1px 2px 0 rgb(9 30 66 / 25%);

    &:hover {
      background-color: var(--hoverGrey);
    }
  }

  &__dashboard {
    display: flex;
  }

  &__column-title {
    color: var(--textGrey);
    font-size: 14px;
    margin-top: 5px;
    margin-bottom: 1rem;
    font-weight: normal;
  }
}
</style>
