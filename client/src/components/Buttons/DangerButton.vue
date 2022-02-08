<template>
  <button class="button">
    <slot></slot>
  </button>
</template>

<script>
export default {
  name: 'DangerButton',
};
</script>

<style scoped lang="scss">
.button {
  user-select: none;
  border: 1px solid #f54343;
  color: #f54343;
  background-color: transparent;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
  &:disabled {
    opacity: 0.7;
  }
  &:hover {
    opacity: 0.7;
    background-color: #f54343;
    color: #fff;
  }
}
</style>
