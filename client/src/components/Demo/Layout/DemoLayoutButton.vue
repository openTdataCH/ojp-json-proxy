<script setup lang="ts">

</script>

<template>
  <div class="button_container">
    <img src="/src/assets/icons/chevron_right.svg" />
  </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.button_container {
  width: 50px;
  height: 50px;
  background: $pt-main-white;
  border-radius: 50%;
  border: 1px solid $pt-main-black;

  display: flex;
  align-items: center;
  justify-content: center;
}
</style>