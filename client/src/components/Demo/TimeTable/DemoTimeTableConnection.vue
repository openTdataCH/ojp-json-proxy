<script setup lang="ts">
import type {Connection} from "@/types/Connection";
import {useI18n} from "vue-i18n";

const {d} = useI18n();

interface Props {
  connection: Connection
}

const props = defineProps<Props>();
</script>

<template>
    <td>
      <div class="timetable_connection__line">
        {{ connection.name }}
      </div>
    </td>
    <td>
      <template v-if="connection.stop.departure">{{ d(connection.stop.departure, 'time') }}</template>
      <template v-else>N.A</template>
    </td>
    <!--TODO: Add Delay-->
    <td>{{ connection.to }}</td>
    <td>{{ connection.stop.platform}}</td>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.timetable_connection__line {
  border: 1px $pt-main-black solid;
  border-radius: 5px;
  display: inline-flex;
  padding: 5px 10px 5px 10px;
}

td {
  padding: 0.5rem;
}


</style>