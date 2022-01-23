<template>
  <input v-model="text" type="text" class="t-input" @change="emitOnChange">
</template>

<script>
import {
  ref, watch, defineEmits, defineProps,
} from 'vue';

export default {
  name: 'TInput',
};
</script>

<script setup>
const emits = defineEmits(['update:modelValue', 'change']);
const props = defineProps({
  modelValue: String,
});

const text = ref(props.modelValue);

const emitOnChange = () => {
  emits('change', text.value);
};

watch(text, (_) => {
  emits('update:modelValue', text.value);
});
</script>

<style scoped lang="scss">
.t-input {
  padding: 5px 10px;
  border: 1px solid #f0eff4;
  border-radius: 0.2rem;
  font-size: 16px;
}
</style>
