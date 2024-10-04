<script setup lang="ts">
import {computed, onMounted, ref, watch} from "vue";
import DemoTimeTableConnections from "@/components/Demo/TimeTable/DemoTimeTableConnectionTable.vue";
import DevModeToggle from "@/components/Demo/DevMode/DevModeToggle.vue";
import DemoLayout from "@/components/Demo/Layout/DemoLayout.vue";
import DemoTimeTableSelect from "@/components/Demo/TimeTable/DemoTimeTableSelect.vue";
import DevModeStep from "@/components/Demo/DevMode/DevModeStep.vue";
import type {StationBoard} from "@/types/StationBoard";
import useStationBoardService from "@/composables/services/stationBoard";
import {useDemoPageStore} from "@/stores/demo";
import {storeToRefs} from "pinia";
import DemoLayoutButton from "@/components/Demo/Layout/DemoLayoutButton.vue";
import Endpoint from "@/components/Demo/DevMode/Endpoint.vue";
import type {SwaggerEndpoint} from "@/types/SwaggerModels";
import {useSwaggerStore} from "@/stores/swagger";
import {RouteNames} from "@/types/RouteNames";
import router from "@/router";
import DocLink from "@/components/Doc/DocLink.vue";
import {useI18n} from "vue-i18n";
import {useUiStore} from "@/stores/ui";

const stationBoardService = useStationBoardService();
const swaggerStore = useSwaggerStore();

const loading = ref<boolean>(false);
const stationBoard = ref<StationBoard>();
const errorMessage = ref<string | null>(null);
const selectedStation = ref("");
const showDevMode = ref(false);
const selectedLIR = ref<string>();
const demoStore = useDemoPageStore();
const uiStore = useUiStore();
const { apiVersion } = storeToRefs(uiStore);
const {t} = useI18n();

watch(() => selectedLIR.value, async (value) => {
    if (errorMessage.value !== null) errorMessage.value = null;

    if (value) {
        loading.value = true;
        stationBoardService.getStationBoardForLocation(value)
            .then((res) => stationBoard.value = res)
            .finally(() => loading.value = false);
        demoStore.setParameterValueForEndpoint('/api/' + apiVersion.value + '/stationBoard', 'id', value)
        demoStore.setParameterValueForEndpoint('/api/' + apiVersion.value + '/stationBoard', 'limit', 8)
    }
});

watch(() => selectedStation.value, async (value) => {
    demoStore.setParameterValueForEndpoint('/api/' + apiVersion.value + '/locationInformation', 'locationName', value)
    demoStore.setParameterValueForEndpoint('/api/' + apiVersion.value + '/stationBoard', 'station', value)
});

watch(() => apiVersion.value, (value) => {
  swaggerStore.fetchSwaggerJSON();
});

onMounted(() => {
    demoStore.initializeSwaggerDoc();
});

function getEndpoint(endpointPath: string): SwaggerEndpoint | null {
    return swaggerStore.swaggerJSON?.find(endpoint => endpoint.path === endpointPath) ?? null;
}

</script>

<template>
    <DemoLayout :showDevMode>
        <template #main>
            <div class="flex flex-col items-center">
                <DevModeToggle :checked="showDevMode" toggleLabel="Developer Mode"
                               @checked="showDevMode = !showDevMode"/>
                <DevModeStep :dev-mode=false :step-nr=1>
                    <div class="p-[1rem]">
                        <DemoTimeTableSelect v-model:lir="selectedLIR"
                                             v-model:station="selectedStation"></DemoTimeTableSelect>
                    </div>
                </DevModeStep>
                <DevModeStep :dev-mode=false :step-nr=2>
                    <div v-if="loading" class="flex content-center justify-center">
                        <p class="italic">loading Station Board...</p>
                    </div>
                    <div v-else>
                        <div v-if="stationBoard" class="p-[1rem]">
                            <DemoTimeTableConnections v-if="stationBoard.stationBoard.length > 0"
                                                      :connections="stationBoard?.stationBoard"/>
                            <div v-else>
                                <p>No Station Board for this Destination</p>
                            </div>
                        </div>
                    </div>
                </DevModeStep>
            </div>
        </template>
        <template #devMode>
            <DevModeToggle id="devMode_toggle" :checked="showDevMode" toggleLabel="Developer Mode"
                           @checked="showDevMode = !showDevMode"/>
            <div class="flex flex-col gap-6">
                <h3 class="font-bold ml-[1rem]">Developer Mode</h3>
                <DemoLayoutButton class="demo_layout-button" @click="showDevMode = !showDevMode"/>
                <DevModeStep :devMode=true :stepNr=1>
                    <p>{{ t('page.demo.steps.LIR') }}</p>
                    <Endpoint v-if="getEndpoint(`/api/${apiVersion}/locationInformation`) !== null"
                              :endpoint="getEndpoint(`/api/${apiVersion}/locationInformation`)!"/>
                    <DocLink :route-name="RouteNames.docsLocationInformation"
                             @click="router.push({name: RouteNames.docsLocationInformation})"/>
                </DevModeStep>
                <DevModeStep :devMode=true :stepNr=2>
                    <p>{{ t('page.demo.steps.StationBoard') }}</p>
                    <Endpoint v-if="getEndpoint(`/api/${apiVersion}/stationBoard`) !== null"
                              :endpoint="getEndpoint(`/api/${apiVersion}/stationBoard`)!"/>
                    <DocLink :route-name="RouteNames.docsStationBoard"
                             @click="router.push({name: RouteNames.docsStationBoard})"/>
                </DevModeStep>
            </div>
        </template>
    </DemoLayout>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

#devMode_toggle {
    display: none;
}

.demo_layout-button {
    position: absolute;
    z-index: 1000;
    top: 50%;
    left: -25px;
    cursor: pointer;
}

@media #{$media-query-l} {
    .demo_layout-button {
        display: none;
    }

    #devMode_toggle {
        display: flex;
    }
}

</style>