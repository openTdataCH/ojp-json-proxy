<script setup lang="ts">
interface Props {
  text: string
}

const props = defineProps<Props>();
</script>

<template>
  <button class="inline-flex items-center primary-button_container" >
    <span class="lowercase">{{ text }}</span>
  </button>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.primary-button_container {
  border: 1px solid $pt-main-black;
  border-radius: 5px;
  background: $pt-main-black;
  padding: 8px 15px;
  gap: 10px;
  width: fit-content;
  color: $pt-main-white;
  font-family: 'Inter Bold';
}
</style>