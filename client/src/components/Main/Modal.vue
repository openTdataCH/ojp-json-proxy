<script setup lang="ts">
interface Props {
  showModal: boolean
}

const props = defineProps<Props>();

const emit = defineEmits(["modal-close"]);
</script>

<template>
  <div v-if="showModal" class="modal__backdrop" >
    <div @click="emit('modal-close')" class="modal__close">
      <img src="/src/assets/icons/x_white.svg" width="20" height="20">
    </div>
    <div class="modal__container main-content">
      <slot></slot>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.modal__backdrop {
  margin: 0;
  padding: 0 50px 0 50px;
  position: fixed;
  inset: 0;
  z-index: 99;
  width: 100%;
  overflow-y: hidden;
  background: rgba(22, 22, 22, 0.90);
}

.modal__close {
  position: sticky;
  display: flex;
  border-radius: 50%;
  background: rgba(22, 22, 22, 0.90);
  width: 60px;
  height: 60px;
  justify-content: center;
  align-items: center;
  top: 1rem;
  margin-left: auto;
  margin-top: 1rem;
  margin-bottom: 1rem;
  cursor: pointer;
}

.modal__container{
  display: flex;
  justify-content: center;
}

</style>