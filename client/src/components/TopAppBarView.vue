<template>
<div class="top-view">
  <search-component v-model="searchText" class="top-view__search">
    <template #content v-if="results.length > 0 && searchText.length > 0">
      <ul>
        <li v-for="(result, idx) in results" :key="idx">
          {{ result }}
        </li>
      </ul>
    </template>
  </search-component>
  <div>
    <div class="top-view__user">
      <span class="top-view__user-name">Stanislav Shevchenko</span>
      <avatar-component/>
    </div>
  </div>
</div>
</template>

<script>
import SearchComponent from '@/components/SearchComponent.vue';
import AvatarComponent from '@/components/AvatarComponent.vue';

import { ref, computed } from 'vue';

export default {
  name: 'TopAppBarView',
  components: {
    SearchComponent,
    AvatarComponent,
  },
};
</script>

<script setup>
const searchText = ref('');

const results = computed(() => {
  const newData = [];
  data.forEach((el) => {
    if (el.includes(searchText.value)) {
      newData.push(el);
    }
  });
  return newData;
});
const data = [];
for (let i = 0; i < 100; i += 1) {
  data.push(`result ${i}`);
}
</script>

<style scoped lang="scss">
.top-view {
  padding: 10px 0;
  display: flex;
  justify-content: space-between;
  &__search {
    width: 33%;
  }
  &__user {
    display: flex;
    align-items: center;
  }
  &__user-name {
    margin-right: 1rem;
    @media screen and (max-width: 650px) {
      display: none;
    }
  }
}
</style>
