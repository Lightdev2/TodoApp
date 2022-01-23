<template>
<div class="sidebar" :class="{'sidebar--closed': !showSidebar}">
  <template v-if="showSidebar">
    <slot></slot>
  </template>
  <button class="sidebar__button" @click="toggleSidebar">
    <arrow-icon class="sidebar__arrow" :class="arrowClassObj"/>
  </button>
</div>
</template>

<script>
import { ref, computed } from 'vue';
import ArrowIcon from '@/assets/icons/arrow-down.vue';

export default {
  name: 'SideBar',
  components: {
    ArrowIcon,
  },
};
</script>

<script setup>
const showSidebar = ref(true);

const toggleSidebar = () => {
  showSidebar.value = !showSidebar.value;
};

const arrowClassObj = computed(() => {
  if (showSidebar.value) {
    return {
      'sidebar__arrow--opened': true,
    };
  }
  return {
    'sidebar__arrow--closed': true,
  };
});
</script>

<style scoped lang="scss">
.sidebar {
  max-width: 350px;
  border-right: 1px solid #dedede;
  height: 100vh;
  max-height: 100vh;
  position: relative;
  &__button {
    position: absolute;
    top: 40px;
    right: -10px;
    border-radius: 50%;
    background-color: #fff;
    border: 1px solid #dedede ;
    height: 20px;
    width: 20px;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  &__arrow {
    width: 13px;
    &--opened {
      transform: rotate(90deg);
      margin-right: 1.5px;
    }
    &--closed {
      margin-left: 3px;
      transform: rotate(-90deg);
    }
  }
  &--closed {
    max-width: 30px;
  }
}
</style>
