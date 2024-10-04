<script setup lang="ts">

import DocNav from "@/components/Doc/DocNav.vue";
import {useRouter} from "vue-router";

const router = useRouter();
</script>

<template>
    <div class="flex flex-col md:flex-row">
        <aside class="mb-10">
            <DocNav/>
        </aside>
        <div class="flex flex-col gap-20 md:ml-6 md:w-[700px]">
            <h1>{{ router.currentRoute.value.meta.title }}</h1>
            <RouterView/>
        </div>
    </div>
</template>

<style scoped lang="scss">

</style>