<script setup lang="ts">
import {ref} from "vue";

const modelValue = defineModel<string>();
const inputField = ref()

defineExpose({
  inputField
})
</script>

<template>
  <input class="timetable_input p-1 w-full"
         ref="inputField"
         type="text"
         v-model="modelValue"
         placeholder="Ausgangspunkt">
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.timetable_input {
  border: 1px solid $pt-main-black;
  border-radius: 5px;
  padding: 9px;
}

</style>