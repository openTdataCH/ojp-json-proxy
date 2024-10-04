<script setup lang="ts">
import {onMounted, ref} from "vue";
import useSwaggerService from "@/composables/services/swagger";
import type {SwaggerEndpoint} from "@/types/SwaggerModels";
import PrimaryButton from "@/components/Main/Controls/PrimaryButton.vue";
import MainFeatureCard from "@/components/Main/Layout/MainFeatureCard.vue";
import {RouteNames} from "@/types/RouteNames";
import {useRouter} from "vue-router";
import {useI18n} from "vue-i18n";

const swaggerService = useSwaggerService();
const router = useRouter();
const { t } = useI18n();

const loading = ref<boolean>(false);
const endpoints = ref<SwaggerEndpoint[]>([]);

onMounted(() => {
    loading.value = true;
    swaggerService.getSwaggerJson()
        .then(res => endpoints.value = res)
        .finally(() => loading.value = false);
})

</script>

<template>
    <div class="flex flex-col items-center">
      <div class="flex flex-col items-center text-center mb-10">
        <h1 class="max-w-3xl">{{ t('page.home.title') }}</h1>
        <div class="flex justify-center mt-4">
          <PrimaryButton @click="$router.push({name: RouteNames.docsGettingStarted})" :text="t('action.getStarted')"></PrimaryButton>
        </div>
      </div>
      <div class="flex flex-col justify-center gap-4 w-[700px]">
        <MainFeatureCard :title="t('page.home.featureCardDoc.title')"
                         icon-url="/src/assets/icons/sbb_book.svg"
                         :text="t('page.home.featureCardDoc.text')"
                         :route-name="RouteNames.docsIntroduction"
                         @click="router.push({name: RouteNames.docsIntroduction})"/>
        <h3 class="mt-4">see it in action</h3>
        <div class="grid grid-cols-2 gap-4">
          <MainFeatureCard :title="t('page.home.featureCardStationBoard.title')"
                           icon-url="/src/assets/icons/sbb_screen-inside.svg"
                           pill-title="Demo App"
                           :text="t('page.home.featureCardStationBoard.text')"
                           :route-name="RouteNames.demoTimeTable"
                           @click="router.push({name: RouteNames.demoTimeTable})"/>
          <MainFeatureCard :title="t('page.home.featureCardLIR.title')"
                           icon-url="/src/assets/icons/sbb_train-side.svg"
                           pill-title="DOC"
                           :text="t('page.home.featureCardLIR.text')"
                           :route-name="RouteNames.docsLocationInformation"
                           @click="router.push({name: RouteNames.docsLocationInformation})"/>
        </div>
      </div>
      <div class="mt-6 w-[700px]">
        <p>{{ t('page.home.intro') }}</p>
      </div>
    </div>
</template>
