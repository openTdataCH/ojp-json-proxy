<script setup lang="ts">
import type {APIMethods} from "@/types/DevMode/APIMethods";
import router from "@/router";

interface Props {
  title: string
  link: string
}

const props = defineProps<Props>();
</script>

<template>
    <div class="hero-bubble_container" @click="$router.push(link)">
        <h2 class="hero-bubble_title">
          <span class="inline-flex gap-2">
            {{ title }}
            <img src="/src/assets/icons/arrow_right.svg" height="40" width="40" />
          </span>
        </h2>
    </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

  .hero-bubble_container {
    width: 100%;
    height: 500px;
    background: $pt-main-gray;
    border-radius: 25px;
    margin: 2rem;
    display: flex;
    cursor: pointer;
  }

  .hero-bubble_title {
    margin: auto 0 0 1.5rem;
    font-weight: bold;
  }
</style>