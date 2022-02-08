<template>
  <textarea v-model="text" class="textarea" @change="emitOnChange">
  </textarea>
</template>

<script>
import {
  ref,
  watch,
  defineProps,
  defineEmits,
} from 'vue';

export default {
  name: 'TTextarea',
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

<style scoped>
.textarea {
  border: 1px solid #dedede;
  border-radius: 5px;
  resize: none;
  width: 100%;
  height: 100%;
}
</style>
