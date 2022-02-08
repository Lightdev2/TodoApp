<template>
  <div @click="toggleAddMode" class="card" :style="{
      cursor: !isAddMode ? 'pointer' : 'default'
    }">
    <button v-if="!isAddMode" class="card__btn">
      <plus-icon class="card__btn-icon"/>
    </button>
    <div class="card__form" v-else>
      <label class="card__input">
        <span class="card__input-label">Title</span>
        <TInput v-model="project.title"/>
      </label>
      <label class="card__input">
        <span class="card__input-label">Description</span>
        <TTextarea v-model="project.desc" class="card__textarea"/>
      </label>
      <div class="card__controls">
        <primary-button :disabled="isAllowToCreate" @click.stop="emitCreate" class="card__controls-btn">
          <span>Create</span>
        </primary-button>
        <flat-button @click.stop="reset" class="card__controls-btn">
          <span>Cancel</span>
        </flat-button>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, defineEmits, computed } from 'vue';
import PlusIcon from '@/assets/icons/plus.vue';
import PrimaryButton from '@/components/Buttons/PrimaryButton.vue';
import TInput from '@/components/Inputs/TInput.vue';
import TTextarea from '@/components/Inputs/TTextarea.vue';
import FlatButton from '@/components/Buttons/FlatButton.vue';

export default {
  name: 'AddProjectCard',
  components: {
    PlusIcon,
    PrimaryButton,
    TInput,
    TTextarea,
    FlatButton,
  },
};
</script>

<script setup>
const project = ref({
  title: '',
  desc: '',
});
const isAddMode = ref(false);
const toggleAddMode = () => {
  if (isAddMode.value) return;
  isAddMode.value = !isAddMode.value;
};
const reset = () => {
  isAddMode.value = false;
  project.value.title = '';
  project.value.desc = '';
};
const isAllowToCreate = computed(() => {
  const projValue = project.value;
  return projValue.title.length === 0 || projValue.desc.length === 0;
});
const emits = defineEmits(['create']);
const emitCreate = () => {
  const projectToCreate = {
    title: project.value.title.trim(),
    desc: project.value.desc.trim(),
  };
  emits('create', projectToCreate);
  reset();
};
</script>

<style scoped lang="scss">
.card {
  width: 300px;
  height: 300px;
  border-radius: 5px;
  border: 1px solid #dedede;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-bottom: 1rem;
  &__btn {
    cursor: pointer;
    background: transparent;
    border: none;
  }
  &__btn-icon {
    width: 50px;
  }
  &__form {
    display: flex;
    flex-direction: column;
  }
  &__input {
    display: flex;
    flex-direction: column;
    margin-bottom: 1rem;
  }
  &__input-label {
    display: block;
    margin-bottom: 5px;
  }
  &__textarea {
    min-height: 100px;
  }
  &__controls-btn {
    width: 45%;
    &:not(:last-child) {
      margin-right: 1rem;
    }
  }
}
</style>
