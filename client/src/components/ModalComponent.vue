<template>
  <div class="modal" v-if="isShowModal">
    <div class="modal__content">
      <div class="modal__header">
        <slot name="head"></slot>
        <div class="modal__control">
          <button @click="closeModal" class="modal__close-btn">
            <close-icon class="modal__close-icon"/>
          </button>
        </div>
      </div>
      <div class="modal__body">
        <slot name="content"></slot>
      </div>
    </div>
  </div>
</template>

<script>
import {
  defineProps,
  ref,
  watch,
  onMounted,
  onUnmounted,
} from 'vue';
import CloseIcon from '@/assets/icons/close.vue';

export default {
  name: 'ModalComponent',
  components: {
    CloseIcon,
  },
};
</script>

<script setup>
const props = defineProps({
  isShowModal: {
    type: Boolean,
    required: true,
    default: false,
  },
  closeOnEsc: {
    required: false,
    default: true,
  },
});

onMounted(() => {
  document.addEventListener('keydown', closeOnKeyDown);
});

onUnmounted(() => {
  document.removeEventListener('keydown', closeOnKeyDown);
});

const closeOnKeyDown = ({ key }) => {
  if (key === 'Escape') {
    closeModal();
  }
};
const isShowModal = ref(props.isShowModal);
watch(() => props.isShowModal, () => {
  isShowModal.value = true;
});
const closeModal = () => {
  isShowModal.value = false;
};
</script>

<style scoped lang="scss">
.modal {
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  position: fixed;
  background-color: rgba(9, 30, 66, 0.54);
  display: flex;
  justify-content: center;
  align-items: center;

  &__header {
    display: flex;
    position: relative;
    padding-right: 50px;
    min-height: 20px;
  }

  &__close-btn {
    cursor: pointer;
    background: transparent;
    border: none;
  }

  &__close-icon {
    width: 15px;
  }

  &__control {
    position: absolute;
    right: 0;
  }

  &__content {
    background: #fff;
    min-width: 30%;
    min-height: 30%;
    border-radius: 5px;
    padding: 10px;
    box-shadow: rgb(9 30 66 / 8%) 0px 0px 0px 1px, rgb(9 30 66 / 8%) 0px 2px 1px, rgb(9 30 66 / 31%) 0px 0px 20px -6px;
  }
}
</style>
