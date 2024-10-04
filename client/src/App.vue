<script setup lang="ts">
import MainNavBar from "@/components/Main/Layout/MainNavBar.vue";
import {useSwaggerStore} from "@/stores/swagger";
import {onMounted} from "vue";
import Footer from "@/components/Main/Layout/MainFooter.vue";
import {useI18n} from "vue-i18n";
import {useUiStore} from "@/stores/ui";

const swaggerStore = useSwaggerStore();

onMounted(() => {
    swaggerStore.fetchSwaggerJSON();
})
</script>

<template>
    <div class="flex justify-center">
        <main class="w-full flex flex-col h-screen 2xl:w-2/3 max-w-7xl px-6">
            <MainNavBar class="flex-grow-0" />
            <RouterView class="flex-grow" />
            <Footer class="flex-grow-0" />
        </main>
    </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.token-input {
    border: 1px solid $pt-main-black;
    border-radius: 5px;
    padding: 9px;
}
</style>
