<template>
  <button class="button">
    <slot></slot>
  </button>
</template>

<script>
export default {
  name: 'FlatButton',
};
</script>

<style scoped lang="scss">
.button {
  border: none;
  user-select: none;
  background-color: transparent;
  border-radius: 5px;
  padding: 5px 10px;
  cursor: pointer;
  color: #948d8d;
  &:disabled {
    opacity: 0.7;
  }
  &:hover {
    opacity: 0.7;
    background: #dedede;
  }
}
</style>
