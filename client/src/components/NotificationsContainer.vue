<template>
<div class="container">
  <slot></slot>
</div>
</template>

<script>
export default {
  name: 'NotificationsContainer',
};
</script>

<style scoped>
.container {
  position: fixed;
  right: 10px;
  background: red;
  bottom: 10px;
}
</style>
