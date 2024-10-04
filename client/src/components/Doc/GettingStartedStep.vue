<script setup lang="ts">
import {computed, type PropType} from "vue";
import {useSwaggerStore} from "@/stores/swagger";
import Endpoint from "@/components/Demo/DevMode/Endpoint.vue";
import type {SwaggerEndpoint} from "@/types/SwaggerModels";

const swaggerStore = useSwaggerStore();

const props = defineProps({
    step: {type: Number, required: true},
    title: {type: String, required: true},
    endpointPath: {type: String as PropType<string | null>},
})

const endpoint = computed<SwaggerEndpoint | null>(() => {
    if (props.endpointPath === null) return null;
    return swaggerStore.swaggerJSON?.find(endpoint => endpoint.path === props.endpointPath) ?? null;
})
</script>

<template>
    <div>
        <h2>{{ step }}. {{ title }}</h2>
        <slot/>
        <Endpoint v-if="endpoint !== null" :endpoint="endpoint"/>
    </div>
</template>

<style scoped lang="scss">

</style>