<template>
  <ul>
    <todo-list-item v-for="(todo,idx) in todos" :key="idx" :todo="todo"/>
  </ul>
</template>

<script>
import { defineProps } from 'vue';
import TodoListItem from '@/components/TodoListItem.vue';

export default {
  name: 'TodoList',
  components: {
    TodoListItem,
  },
};
</script>
<script setup>

const props = defineProps({
  todos: {
    type: Array,
    required: true,
  },
});
console.log(props?.todos);
</script>
<style scoped>

</style>
