<script setup lang="ts">
import type {Connection} from "@/types/Connection";
import type {StationBoard} from "@/types/StationBoard";
import DemoTimeTableConnection from "@/components/Demo/TimeTable/DemoTimeTableConnection.vue";

interface Props {
  connections: Connection[]
}

const props = defineProps<Props>();
</script>

<template>
  <table class="w-full text-left">
    <thead>
    <tr>
      <th>Linie</th>
      <th>Zeit</th>
      <th>Von</th>
      <th>Gleis/Kante</th>
    </tr>
    </thead>
  <tbody>
  <tr v-for="connection in connections">
    <DemoTimeTableConnection :connection="connection"></DemoTimeTableConnection>
  </tr>
  </tbody>
  </table>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";
table {
  border-collapse: separate;
  border-spacing: 0 1rem;
}

tbody tr {
  border-radius: 5px;
  border-spacing: 1rem;
  outline: 1px $pt-main-black solid;
}

td:first-child {
  border-radius: 5px 0 0 5px;
}

td:last-child {
  border-radius: 0 5px 5px 0;
}

th {
  padding-left: 0.5rem;
}

</style>