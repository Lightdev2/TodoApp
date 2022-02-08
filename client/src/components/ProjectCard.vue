<template>
  <div class="card">
    <div class="card__content">
      <h3 class="card__title">Title : {{ props.project.title }}</h3>
      <h4>Description: </h4>
      <p class="card__desc"> {{ props.project?.desc }}</p>
      <div class="card__date">Created Date: {{ getFriendlyDate(props.project.createdDate) }}</div>
      <div class="card__date">
        Last Modified Date:
        {{
          props.project.lastModifiedDate === null
            ? 'Never modified'
            : getFriendlyDate(props.project.lastModifiedDate)
        }}
      </div>
    </div>
    <div class="card__footer">
      <danger-button @click="emitDelete" class="card__remove">
        <span>Remove</span>
      </danger-button>
      <span class="card__progress">Progress 0%</span>
    </div>
  </div>
</template>

<script>
import { defineProps, defineEmits } from 'vue';
import DangerButton from '@/components/Buttons/DangerButton.vue';

export default {
  name: 'ProjectCard',
  components: {
    DangerButton,
  },
};
</script>

<script setup>
const props = defineProps({
  project: {
    type: Object,
    required: true,
  },
});
const emits = defineEmits(['delete']);
const emitDelete = () => {
  emits('delete', props.project.id);
};
const formatNum = (_num) => (_num < 10 ? `0${_num}` : _num);
const getFriendlyDate = (_date) => {
  const date = new Date(_date);
  return `${date.getFullYear()}.${formatNum(date.getMonth())}.${formatNum(date.getDate())} at ${date.getHours()}:${date.getMinutes()}`;
};
</script>

<style scoped lang="scss">
.card {
  cursor: pointer;
  display: flex;
  flex-direction: column;
  border-radius: 5px;
  border: 1px solid #dedede;
  padding: 10px 20px;
  height: 300px;
  width: 300px;
  margin-right: 1rem;
  margin-bottom: 1rem;
  justify-content: space-between;
  &__content {
    width: 100%;
  }
  &__desc {
    margin: 10px 0;
  }
  &__title {
    margin-bottom: 5px;
  }
  &__date {
    color: #dedede;
    font-size: 12px;
  }
  &__remove {
    max-width: 50%;
  }
  &__footer {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }
}
</style>
