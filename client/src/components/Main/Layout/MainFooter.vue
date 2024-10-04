<script setup lang="ts">
import {Separator} from "@/components/ui/separator";
import {useI18n} from "vue-i18n";
import {useUiStore} from "@/stores/ui";
import {useRouter} from "vue-router";

const i18n = useI18n();
const uiStore = useUiStore();

function changeLocale(locale: string): void {
    uiStore.changeLocale(locale);
}

</script>

<template>
    <div class="flex flex-col gap-6 p-3">
        <Separator />
        <div class="flex h-5 items-center space-x-3 text-sm">

            <template v-for="(locale, index) in i18n.availableLocales" :key="index">
                <div class="uppercase cursor-pointer hover:underline"
                     :class="{'font-bold underline': locale == i18n.locale.value}" @click="changeLocale(locale)">
                    {{ locale.slice(0, locale.indexOf('-')) }}
                </div>
                <Separator v-if="index < i18n.availableLocales.length - 1" orientation="vertical"/>
            </template>

        </div>
    </div>
</template>

<style scoped lang="scss">

</style>