<template>
<div class="avatar">
  <slot name="icon"></slot>
</div>
</template>

<script>
export default {
  name: 'AvatarComponent',
};
</script>

<style scoped>
.avatar {
  border-radius: 50%;
  width: 30px;
  height: 30px;
  background-color: #dedede;
}
</style>
