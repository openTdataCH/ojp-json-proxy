<script setup lang="ts">
import {useSwaggerStore} from "@/stores/swagger";
import Endpoint from "@/components/Demo/DevMode/Endpoint.vue";
import type {SwaggerEndpoint} from "@/types/SwaggerModels";
import 'vue-json-pretty/lib/styles.css';
import VueJsonPretty from "vue-json-pretty";
import {computed, type ComputedRef, onMounted, type Ref, ref, watch} from "vue";
import EndpointRequest from "@/components/Demo/DevMode/EndpointRequest.vue";
import {APIMethods} from "@/types/DevMode/APIMethods";
import {useI18n} from "vue-i18n";
import {useUiStore} from "@/stores/ui";
import {storeToRefs} from "pinia";
import {getNextLastDayOfWeek} from "radix-vue/date";

const {t} = useI18n();
const swaggerStore = useSwaggerStore();
const uiStore = useUiStore();
const { apiVersion } = storeToRefs(uiStore);
const endpoint = ref();

watch( () => apiVersion.value, (newValue, oldValue) => {
  if (newValue === oldValue) return;
  getEndpointData(newValue);
});

async function getEndpointData(apiVersion: string = "v1") {
  await swaggerStore.fetchSwaggerJSON();
  const json = swaggerStore.swaggerJSON;
  if (json && apiVersion) {
    endpoint.value = json.find(e => e.path === `/api/${apiVersion}/locationInformation`);
  }
}

const OJPJSON = {
  "locations":
  [
    {
      "id":
          "8507000",
      "name":
          "Bern",
      "transportType":
          "rail",
      "probability":
          1
    },
    {
      "id":
          "216",
      "name":
          "Berlin",
      "transportType":
          "rail",
      "probability":
          0.977999985
    }
  ]
};

const OJPXML = `
<?xml version="1.0" encoding="UTF-8"?>
<siri:OJP xmlns:siri="http://www.siri.org.uk/siri" xmlns:ojp="http://www.vdv.de/ojp" version="1.0">
    <siri:OJPResponse>
        <siri:ServiceDelivery>
            <siri:ResponseTimestamp>2024-06-30T19:27:21Z</siri:ResponseTimestamp>
            <siri:ProducerRef>optmentzEFAControllerEFAController10.6.19.21-build-1624-dce883a3488d</siri:ProducerRef>
            <siri:ResponseMessageIdentifier>1c7470659e37e38f</siri:ResponseMessageIdentifier>
            <siri:Status>true</siri:Status>
            <ojp:OJPLocationInformationDelivery>
                <siri:ResponseTimestamp>2024-06-30T19:27:20Z</siri:ResponseTimestamp>
                <siri:Status>true</siri:Status>
                <ojp:CalcTime>278</ojp:CalcTime>
                <ojp:Location>
                    <ojp:Location>
                        <ojp:StopPlace>
                            <ojp:StopPlaceRef>8507000</ojp:StopPlaceRef>
                            <ojp:StopPlaceName>
                                <ojp:Text xml:lang="de">Bern</ojp:Text>
                            </ojp:StopPlaceName>
                            <ojp:TopographicPlaceRef>23006351:1</ojp:TopographicPlaceRef>
                        </ojp:StopPlace>
                        <ojp:LocationName>
                            <ojp:Text xml:lang="de">Bern (Bern)</ojp:Text>
                        </ojp:LocationName>
                        <ojp:GeoPosition>
                            <siri:Longitude>7.43913</siri:Longitude>
                            <siri:Latitude>46.94883</siri:Latitude>
                        </ojp:GeoPosition>
                    </ojp:Location>
                    <ojp:Complete>true</ojp:Complete>
                    <ojp:Probability>1</ojp:Probability>
                </ojp:Location>
                <ojp:Location>
                    <ojp:Location>
                        <ojp:StopPlace>
                            <ojp:StopPlaceRef>216</ojp:StopPlaceRef>
                            <ojp:StopPlaceName>
                                <ojp:Text xml:lang="de">Berlin</ojp:Text>
                            </ojp:StopPlaceName>
                            <ojp:TopographicPlaceRef>32000000:1</ojp:TopographicPlaceRef>
                        </ojp:StopPlace>
                        <ojp:LocationName>
                            <ojp:Text xml:lang="de">Berlin (Unbekannt)</ojp:Text>
                        </ojp:LocationName>
                        <ojp:GeoPosition>
                            <siri:Longitude>13.37018</siri:Longitude>
                            <siri:Latitude>52.52330</siri:Latitude>
                        </ojp:GeoPosition>
                    </ojp:Location>
                    <ojp:Complete>true</ojp:Complete>
                    <ojp:Probability>0.977999985</ojp:Probability>
                </ojp:Location>
            </ojp:OJPLocationInformationDelivery>
        </siri:ServiceDelivery>
    </siri:OJPResponse>
</siri:OJP>
`

onMounted(() => {
  swaggerStore.fetchSwaggerJSON();
  getEndpointData(apiVersion.value)
})

</script>

<template>
    <div class="max-w-[700px]">
      <Endpoint v-if="endpoint" :endpoint show-description show-name />
      <div class="flex flex-col gap-2 mt-4">
        <!-- TODO: Adjust color etc. -->
        <h3>Resource URL</h3>
        <div class="doc__url">
          <p><span class="font-bold">GET</span> /api/{{ apiVersion }}/locationInformation</p>
        </div>
        <h3>Request Parameters</h3>
        <table class="doc__table">
          <thead>
            <tr>
              <td>{{ t('page.docs.table.header.name') }}</td>
              <td>{{ t('page.docs.table.required') }}</td>
              <td>{{ t('page.docs.table.header.description') }}</td>
              <td>{{ t('page.docs.table.header.example') }}</td>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>stationName</td>
              <td>{{ t('page.docs.table.required') }}</td>
              <td>Specified the location name to search for</td>
              <td>Bern</td>
            </tr>
            <tr>
              <td>limit</td>
              <td>optional</td>
              <td>limits the amount of locations returned</td>
              <td>10</td>
            </tr>
          </tbody>
        </table>
        <div class="flex flex-col mt-4 gap-4">
          <h2>XML vs JSON</h2>
          <div class="doc__url">
            <p><span class="font-bold">GET</span> /api/{{ apiVersion }}/locationInformation?locationName=Bern&limit=1</p>
          </div>
          <h3>OJP-Service Response</h3>
            <p>{{ t('docsLocationInformation.ojpResponse') }}</p>
            <pre class="w-[700px] doc__code-block overflow-x-scroll">
              {{ OJPXML }}
            </pre>
          <h3>OJP-Proxy Response</h3>
          <p>{{ t('docsLocationInformation.ojpProxyResponse') }}</p>
            <div class="doc__code-block">
              <vue-json-pretty theme="light" :data="OJPJSON"></vue-json-pretty>
            </div>
        </div>
      </div>
    </div>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.doc__code-block {
  background: $pt-main-light-gray;
  color: $pt-main-black;
  font-size: 12px;
  padding: 1rem;
  border-radius: 15px;
  border: 1px solid $pt-main-black;
}

.doc__url {
  display: flex;
  flex-direction: row;
  text-align: center;
  border-radius: 15px;
  background: $pt-main-light-gray;
  padding: 1rem;

   p {
     margin: 0;
   }
}

.doc__table {
  border-spacing: 0;
  border-collapse: separate;
  border-radius: 5px;
  border: 1px solid $pt-main-dark-gray;
}

.doc__table td {
  padding: 0.5rem;
}

.doc__table thead {
  font-weight: bold;
}

.doc__table th:not(:last-child),
.doc__table td:not(:last-child) {
  border-right: 1px solid $pt-main-dark-gray;
}

/* Apply a border to the bottom of all but the last row */
.doc__table>tbody>tr:not(:last-child)>td,
.doc__table>thead>tr>td{
  border-bottom: 1px solid $pt-main-dark-gray;
}




</style>