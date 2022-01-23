<template>
  <div class="search-input">
    <label ref="input" class="search-input__label">
      <input v-model="searchText" placeholder="Search" class="search-input__input" type="text">
      <search-icon class="search-input__icon"/>
    </label>
    <div v-if="isShowDropdown" class="search-input__dropdown" :style="{width: dropdownWidth + 'px'}">
      <slot name="content"></slot>
    </div>
  </div>
</template>

<script>
import SearchIcon from '@/assets/icons/search.vue';
import {
  defineProps,
  ref,
  defineEmits,
  watch,
  onMounted,
  useSlots,
} from 'vue';

export default {
  name: 'SearchComponent',
  components: {
    SearchIcon,
  },
};
</script>

<script setup>
const input = ref(null);
const dropdownWidth = ref(null);
const slots = useSlots();

onMounted(() => {
  dropdownWidth.value = input.value.offsetWidth;
});
const props = defineProps({
  modelValue: String,
});

const isShowDropdown = ref(false);

const searchText = ref(props.modelValue);
const emits = defineEmits(['update:modelValue']);

watch(searchText, (_) => {
  emits('update:modelValue', searchText.value);
  setTimeout(() => {
    isShowDropdown.value = !!slots?.content;
  }, 0);
});

</script>

<style scoped lang="scss">
.search-input {
  min-width: 100px;
  &__label {
    position: relative;
  }
  &__input {
    border: 1px solid #dedede;
    border-radius: 5px;
    padding: 5px 20px;
    width: 100%;
  }
  &__icon {
    max-width: 17px;
    position: absolute;
    right: 15px;
    top: 2px;
  }
  &__dropdown {
    position: absolute;
    max-height: 350px;
    overflow: auto;
    top: 45px;
    border-radius: 5px;
    padding: 10px;
    background: #fff;
    border: 1px solid #dedede;
  }
}
</style>
