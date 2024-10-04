<script setup lang="ts">
import type {RouteNames} from "@/types/RouteNames";
import {useI18n} from "vue-i18n";

interface Props {
  routeName: RouteNames
}

const {t} = useI18n();

const props = defineProps<Props>();
</script>

<template>
  <div class="doc__link-to-page">
    <img src="/src/assets/icons/book_open-white.svg">
      <div class="inline-flex flex-rows items-center w-max gap-2">
        <p>{{ t('page.docs.readMore') }}</p>
        <img src="/src/assets/icons/arrow_right-white.svg">
      </div>
  </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.doc__link-to-page {
  width: 100%;
  border-radius: 15px;
  background: $pt-main-black;
  display: flex;
  flex-direction: row;
  color: $pt-main-white;
  padding: 1rem;
  gap: 1rem;
  margin: 1rem 0 1rem 0;
  font-weight: bold;

    &:hover {
    cursor: pointer;
  }

  p {
    margin: 0;
  }

}
</style>