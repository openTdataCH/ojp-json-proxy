<script setup lang="ts">
import {ref} from "vue";

interface Props {
  popoverId: string
}

const props = defineProps<Props>();
const modalContainer = ref();

function closeModal(){
  modalContainer.value.togglePopover();
}

defineExpose({
  closeModal
})
</script>

<template>
  <div ref="modalContainer" :id="popoverId" popover class="modal" >
    <div @click="modalContainer.togglePopover()" class="modal__close">
      <img src="/src/assets/icons/x_white.svg" width="20" height="20">
    </div>
    <div class="modal__container main-content">
      <slot></slot>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "../../assets/scss/variables";

.modal::backdrop {
  background: rgba(22, 22, 22, 0.90);
}

.modal {
  background: none;
  position: fixed;
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