<script setup lang="ts">
interface Props {
  mainIconURL: string
  secondaryIconURL?: string
  text: string
}

const props = defineProps<Props>();
</script>

<template>
  <button class="inline-flex items-center button_container" >
    <img :src="mainIconURL" height="20" width="20">
    <span>{{ text }}</span>
    <img v-if="secondaryIconURL" :src="secondaryIconURL">
  </button>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.button_container {
  border: 1px solid $pt-main-black;
  border-radius: 5px;
  background: $pt-main-white;
  padding: 8px;
  gap: 10px;
  width: fit-content;
  font-family: 'Inter Bold';

  &:hover {
    background: $pt-main-light-gray;
  }
}
</style>