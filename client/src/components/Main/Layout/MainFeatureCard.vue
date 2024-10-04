<script setup lang="ts">
import type {RouteNames} from "@/types/RouteNames";
import MainContentPill from "@/components/Main/Layout/MainContentPill.vue";

interface Props {
  iconUrl: string
  routeName: RouteNames
  routeHash?: string
  title: string
  text: string
  pillTitle? : string
}

const props = defineProps<Props>();
</script>

<template>
  <div class="flex flex-row gap-6 items-start text-left feature_card">
    <img class="pt-2" :src="iconUrl" height="45px" width="45px">
    <div class="flex flex-col">
      <h3>{{ title }}</h3>
      <MainContentPill :title="pillTitle" v-if="pillTitle" />
      <p>{{ text }}</p>
      <span class="inline-flex flex-row gap-2 font-bold">
        <RouterLink :to="{name: props.routeName, hash: props.routeHash}">read more</RouterLink>
        <img src="/src/assets/icons/arrow_right.svg" height="14" width="14">
      </span>
    </div>
  </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.feature_card {
  border: 1px solid $pt-main-black;
  border-radius: 5px;
  padding: 1.5rem;

  &:hover {
    background: $pt-main-light-gray;
    cursor: pointer;
  }
}
</style>