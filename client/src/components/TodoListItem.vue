<template>
  <li>{{props.todo}}</li>
</template>

<script>
import { defineProps } from 'vue';

export default {
  name: 'TodoListItem',
};
</script>

<script setup>
const props = defineProps({
  todo: {
    required: true,
  },
});
</script>
<style scoped>

</style>
