<script setup lang="ts">
import APITokenButton from "@/components/Main/DialogAPIToken.vue";
import GettingStartedStep from "@/components/Doc/GettingStartedStep.vue";
import {useI18n} from "vue-i18n";
import i18n from "@/plugins/i18n";

const { t } = useI18n();

</script>

<template>
    <p>
        {{ t('docsIntroduction.header') }}
    </p>
</template>

<style scoped lang="scss">

</style>