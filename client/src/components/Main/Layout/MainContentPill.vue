<script setup lang="ts">
interface Props {
  title: string
}

const props = defineProps<Props>();
</script>

<template>
  <div class="content-pill__container">
    <p>{{ title }}</p>
  </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.content-pill__container {
  background: $pt-main-black;
  border-radius: 10px;
  color: $pt-main-white;
  font-weight: bold;
  display: flex;
  justify-content: center;
  align-items: center;
  width: max-content;
  padding: 0.3rem 0.5rem;
  margin-bottom: 0.5rem;
  text-transform: uppercase;
  letter-spacing: 1px;

  p {
    margin: 0;
    font-size: 0.8rem;
  }
}
</style>