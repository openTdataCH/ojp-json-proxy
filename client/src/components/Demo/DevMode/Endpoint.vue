<script setup lang="ts">
import type {SwaggerEndpoint} from "@/types/SwaggerModels";
import {computed, type PropType, ref} from "vue";
import EndpointRequest from "@/components/Demo/DevMode/EndpointRequest.vue";

const props = defineProps({
    endpoint: {type: Object as PropType<SwaggerEndpoint>, required: true},
    showName: {type: Boolean, default: false},
    showDescription: {type: Boolean, default: false}
})

</script>

<template>
    <div>
        <h3 v-if="showName">{{ endpoint.path }}</h3>

        <template v-for="method in endpoint.methods" :key="method.name">
            <p v-if="showDescription">{{ method.description }}</p>
            <EndpointRequest :method="method" :endpoint="endpoint.path" />
        </template>
    </div>
</template>

<style scoped lang="scss">

</style>