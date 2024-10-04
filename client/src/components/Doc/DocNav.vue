<script setup lang="ts">
import {RouteNames} from "@/types/RouteNames";
import {useSwaggerStore} from "@/stores/swagger";
import {useI18n} from "vue-i18n";

const swaggerStore = useSwaggerStore();
const {t} = useI18n();
</script>

<template>
    <nav class="sticky top-4 py-10 px-8 flex flex-col gap-2 doc__sidebar">
        <RouterLink :to="{name: RouteNames.docsIntroduction}">{{ t('page.docs.introduction') }}</RouterLink>
        <RouterLink :to="{name: RouteNames.docsGettingStarted}">{{ t('page.docs.gettingStarted') }}</RouterLink>
        <span class="sidebar__section-title">{{ t('page.docs.endpoints') }}</span>
        <RouterLink :to="{name: RouteNames.docsLocationInformation}">/locationInformation</RouterLink>
        <RouterLink :to="{name: RouteNames.docsStationBoard}">/stationBoard</RouterLink>
        <span class="sidebar__section-title">Background</span>
        <a href="https://opentransportdata.swiss/en/cookbook/open-journey-planner-ojp/" target="_blank">Open journey platform</a>
        <a href="https://transport.opendata.ch/" target="_blank">Transport API</a>
    </nav>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.doc__container{
    position: absolute;
    height: 100%;
    color: $pt-main-dark-gray;
}

.sidebar__section-title{
    color: $pt-main-black;
    margin-top: 2rem;
    text-transform: uppercase;
    font-family: "Inter Bold";
    letter-spacing: 2.5px;
}

.doc__sidebar {
    //border: 1px solid $pt-main-black;
    border-radius: 5px;
    background: $pt-main-light-gray ;
}

.sidebar__active-link {
    color: $pt-main-black;
}
</style>