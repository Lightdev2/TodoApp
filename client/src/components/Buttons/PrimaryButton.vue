<template>
<button class="button">
  <slot></slot>
</button>
</template>

<script>
export default {
  name: 'PrimaryButton',
};
</script>

<style scoped lang="scss">
.button {
  border: none;
  background-color: #3454cf;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
  &:disabled {
    opacity: 0.7;
  }
  &:hover {
    opacity: 0.7;
  }
}
</style>
