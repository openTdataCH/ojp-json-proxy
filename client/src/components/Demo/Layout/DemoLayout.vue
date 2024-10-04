<script setup lang="ts">
import DemoLayoutButton from "@/components/Demo/Layout/DemoLayoutButton.vue";

interface Props {
  showDevMode: boolean
}

const props = defineProps<Props>();
</script>

<template>
  <div class="grid grid-flow-col justify-center w-full" :class="{ 'demo_layout--container': showDevMode }">
    <div class="demo_layout--main_container pt-4">
      <div class="demo_layout--main_content" :class="{ 'demo_layout--main_content_devMode': showDevMode }" >
        <slot name="main"></slot>
      </div>
    </div>
    <Transition name="transition-devmode">
    <div v-if="showDevMode" class="demo_layout--devMode_container pt-4">
      <div  class="demo_layout--devMode_content">
        <slot name="devMode"></slot>
      </div>
    </div>
    </Transition>
  </div>
</template>
<style scoped lang="scss">
@import "src/assets/scss/variables";

.demo_layout--container {
  grid-template-columns: 1fr 700px;
}

.demo_layout--main_container{
  display: flex;
  justify-content: right;
}

.demo_layout--main_content{
  width: 700px;
}

.demo_layout--main_content_devMode {
  width: 100%;
}

.demo_layout--devMode_content {
  width: 100%;
}

.demo_layout--devMode_container {
  position: relative;
  background: $pt-main-gray;
  display: flex;
  justify-content: left;
}

.transition-devmode-enter-active {
  transition: all 0.1s ease-in;
}

.transition-devmode-leave-active {
  transition: all 0.1s ease-out;
}

.transition-devmode-enter-to {
  opacity: 1;
}

.transition-devmode-enter-from,
.transition-devmode-leave-to {
  transform: translateX(100px);
  opacity: 0;
}

@media #{$media-query-l} {
  .demo_layout--container {
    grid-template-columns: 1fr;
  }

  .demo_layout--main_container{
    display: flex;
    justify-content: right;
  }

  .demo_layout--devMode_container {
    width: 100%;
    position: absolute;
  }
}
</style>