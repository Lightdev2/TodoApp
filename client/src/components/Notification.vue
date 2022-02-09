<template>
  <div class="notification">
    <button @click="emitClose">close</button>
    <slot name="content" :item="props.item"></slot>
  </div>
</template>

<script>
import { defineProps, defineEmits } from 'vue';

export default {
  name: 'Notification',
};
</script>

<script setup>
const emits = defineEmits(['close']);
const props = defineProps({
  idx: {
    required: true,
  },
  item: {
    required: true,
  },
});
const emitClose = () => {
  emits('close', props.idx);
};
</script>

<style scoped lang="scss">
.notification {
  width: 350px;
  max-width: 350px;
  border-radius: 5px;
  height: 50px;
  background: red;
}
</style>
