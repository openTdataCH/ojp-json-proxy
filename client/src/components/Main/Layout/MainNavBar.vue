<script setup lang="ts">
import APITokenButton from "@/components/Main/DialogAPIToken.vue";
import {computed} from "vue";
import {RouteNames} from "@/types/RouteNames";
import {useI18n} from "vue-i18n";
import MainSelectApiVersion from "@/components/Main/Controls/MainSelectApiVersion.vue";

const {t} = useI18n();

const swaggerUrl = computed<string>(() => {
    return import.meta.env.VITE_SWAGGER_BASEURL + '/index.html';
})

</script>

<template>
    <nav class="flex w-full justify-between py-2 mb-6 main-nav sticky">
        <div class="flex items-center flex-row gap-6">
            <RouterLink class="nav__primary-links" :to="{name: RouteNames.home}">OJP Explorer 🚂</RouterLink>
            <RouterLink class="nav__secondary-links" :to="{name: RouteNames.docsIntroduction}">{{ t('page.docs.index') }}</RouterLink>
            <RouterLink class="nav__secondary-links" :to="{name: RouteNames.demoTimeTable}">{{ t('page.demo.index') }}</RouterLink>
        </div>
        <div class="inline-flex flex-row items-center gap-4">
            <a :href="swaggerUrl" target="_blank">{{ t('action.goToSwagger') }}</a>
            <APITokenButton/>
            <MainSelectApiVersion/>
        </div>
    </nav>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.main-nav {
    border-bottom: 2px solid $pt-main-light-gray;

    p {
        margin: 0;
    }
}

.nav__secondary-links {
    font-size: 1rem;
}

.nav__primary-links {
    font-size: 1rem;
    font-family: "Inter Bold";
}

</style>