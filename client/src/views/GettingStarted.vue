<script setup lang="ts">
import APITokenButton from "@/components/Main/DialogAPIToken.vue";
import GettingStartedStep from "@/components/Doc/GettingStartedStep.vue";
import {useI18n} from "vue-i18n";
import DocLayout from "@/layouts/DocLayout.vue";
import DocLink from "@/components/Doc/DocLink.vue";
import {RouteNames} from "@/types/RouteNames";
import router from "@/router";

const {t} = useI18n();

</script>

<template>
    <p>
        This Guide will help you to make the first steps using the OJP-Service.
    </p>

    <GettingStartedStep :step="1" :title="t('docsGettingStarted.step1.title')">
      <p>
        <i18n-t keypath="docsGettingStarted.step1.description">
          <template v-slot:link>
            <a style="text-decoration: underline" href="https://opentransportdata.swiss/en/dev-dashboard/">OJP-Developer Portal</a>
          </template>
        </i18n-t>
      </p>
      <APITokenButton/>
    </GettingStartedStep>

    <div>
      <GettingStartedStep :step="2" :title="t('docsGettingStarted.step2.title')" endpoint-path="/api/locationInformation">
        <p>{{ t('docsGettingStarted.step2.description') }}</p>
      </GettingStartedStep>
      <DocLink :route-name="RouteNames.docsLocationInformation"
               @click="router.push({name: RouteNames.docsLocationInformation})"/>
    </div>

    <div>
      <GettingStartedStep :step="3" :title="t('docsGettingStarted.step3.title')" endpoint-path="/api/stationBoard">
        <p>{{ t('docsGettingStarted.step3.description') }}</p>
      </GettingStartedStep>
      <DocLink :route-name="RouteNames.docsStationBoard"
               @click="router.push({name: RouteNames.docsStationBoard})"/>
    </div>

</template>

<style scoped lang="scss">

</style>