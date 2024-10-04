<script setup lang="ts">
interface Props {
  text: string
}

const props = defineProps<Props>();
</script>

<template>
  <button class="inline-flex items-center secondary-button_container" >
    <span>{{ text }}</span>
  </button>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.secondary-button_container {
  border: 1px solid transparent;
  border-radius: 5px;
  background: $pt-main-white;
  padding: 8px 15px;
  gap: 10px;
  width: fit-content;
  color: $pt-main-black;
  font-family: 'Inter Bold';

  &:hover {
    background: $pt-main-light-gray;
  }
}
</style>