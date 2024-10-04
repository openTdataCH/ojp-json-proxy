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
    endpoint.value = json.find(e => e.path === `/api/${apiVersion}/stationBoard`);
  }
}

const OJPJSON = {
  "stationBoard":
      [
        {
          "name":
              "RE5",
          "category":
              "rail",
          "number":
              "1141",
          "operator":
              "Regionalverkehr Bern-Solothurn",
          "to":
              "Solothurn",
          "stop":
              {
                "station":
                    null,
                "arrival":
                    null,
                "departure":
                    "2024-07-08T11:59:00Z",
                "delay":
                    null,
                "platform":
                    "21",
                "prognosis":
                    null
              }
        },
        {
          "name":
              "S3",
          "category":
              "rail",
          "number":
              "15350",
          "operator":
              "BLS AG (bls)",
          "to":
              "Belp",
          "stop":
              {
                "station":
                    null,
                "arrival":
                    "2024-07-08T12:00:00Z",
                "departure":
                    "2024-07-08T12:02:00Z",
                "delay":
                    null,
                "platform":
                    "1",
                "prognosis":
                    null
              }
        },
        {
          "name":
              "IR15",
          "category":
              "rail",
          "number":
              "2522",
          "operator":
              "Schweizerische Bundesbahnen SBB",
          "to":
              "Genève-Aéroport",
          "stop":
              {
                "station":
                    null,
                "arrival":
                    "2024-07-08T12:00:00Z",
                "departure":
                    "2024-07-08T12:04:00Z",
                "delay":
                    null,
                "platform":
                    "3",
                "prognosis":
                    null
              }
        }
      ]
};

const OJPXML = `
<?xml version="1.0" encoding="UTF-8"?>
<siri:OJP xmlns:siri="http://www.siri.org.uk/siri" xmlns:ojp="http://www.vdv.de/ojp" version="1.0">
    <siri:OJPResponse>
        <siri:ServiceDelivery>
            <siri:ResponseTimestamp>2024-07-08T12:01:26Z</siri:ResponseTimestamp>
            <siri:ProducerRef>optmentzEFAControllerEFAController10.6.19.26-build-1633-f9e8925ea0ff</siri:ProducerRef>
            <siri:ResponseMessageIdentifier>cbcae1d716823579</siri:ResponseMessageIdentifier>
            <siri:Status>true</siri:Status>
            <ojp:OJPStopEventDelivery>
                <siri:ResponseTimestamp>2024-07-08T12:01:26Z</siri:ResponseTimestamp>
                <siri:Status>true</siri:Status>
                <ojp:CalcTime>48</ojp:CalcTime>
                <ojp:StopEventResponseContext>
                    <ojp:Places>
                        <ojp:Location>
                            <ojp:StopPlace>
                                <ojp:StopPlaceRef>8507000</ojp:StopPlaceRef>
                                <ojp:StopPlaceName>
                                    <ojp:Text xml:lang="de">Bern</ojp:Text>
                                </ojp:StopPlaceName>
                                <ojp:PrivateCode>
                                    <ojp:System>EFA</ojp:System>
                                    <ojp:Value>111055</ojp:Value>
                                </ojp:PrivateCode>
                                <ojp:TopographicPlaceRef>23006351:1</ojp:TopographicPlaceRef>
                            </ojp:StopPlace>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Bern</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>7.43849</siri:Longitude>
                                <siri:Latitude>46.94946</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:StopPoint>
                                <siri:StopPointRef>ch:1:sloid:7000:5:10</siri:StopPointRef>
                                <ojp:StopPointName>
                                    <ojp:Text xml:lang="de">Bern</ojp:Text>
                                </ojp:StopPointName>
                                <ojp:PrivateCode>
                                    <ojp:System>EFA</ojp:System>
                                    <ojp:Value>111055:0:10</ojp:Value>
                                </ojp:PrivateCode>
                                <ojp:ParentRef>8507000</ojp:ParentRef>
                                <ojp:TopographicPlaceRef>23006351:1</ojp:TopographicPlaceRef>
                            </ojp:StopPoint>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Bern</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>7.43849</siri:Longitude>
                                <siri:Latitude>46.94946</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:TopographicPlace>
                                <ojp:TopographicPlaceCode>23006351:1</ojp:TopographicPlaceCode>
                                <ojp:TopographicPlaceName>
                                    <ojp:Text xml:lang="de">Bern</ojp:Text>
                                </ojp:TopographicPlaceName>
                            </ojp:TopographicPlace>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Bern</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>0.0</siri:Longitude>
                                <siri:Latitude>0.0</siri:Latitude>
                                <siri:Precision>20000000</siri:Precision>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:StopPlace>
                                <ojp:StopPlaceRef>8501026</ojp:StopPlaceRef>
                                <ojp:StopPlaceName>
                                    <ojp:Text xml:lang="de">Genève-Aéroport</ojp:Text>
                                </ojp:StopPlaceName>
                                <ojp:PrivateCode>
                                    <ojp:System>EFA</ojp:System>
                                    <ojp:Value>107029</ojp:Value>
                                </ojp:PrivateCode>
                                <ojp:TopographicPlaceRef>23009623:2</ojp:TopographicPlaceRef>
                            </ojp:StopPlace>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Genève-Aéroport</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>6.11019</siri:Longitude>
                                <siri:Latitude>46.23134</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:StopPoint>
                                <siri:StopPointRef>ch:1:sloid:1026:1:2</siri:StopPointRef>
                                <ojp:StopPointName>
                                    <ojp:Text xml:lang="de">Genève-Aéroport</ojp:Text>
                                </ojp:StopPointName>
                                <ojp:PrivateCode>
                                    <ojp:System>EFA</ojp:System>
                                    <ojp:Value>107029:0:2</ojp:Value>
                                </ojp:PrivateCode>
                                <ojp:ParentRef>8501026</ojp:ParentRef>
                                <ojp:TopographicPlaceRef>23009623:2</ojp:TopographicPlaceRef>
                            </ojp:StopPoint>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Genève-Aéroport</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>6.11019</siri:Longitude>
                                <siri:Latitude>46.23134</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:TopographicPlace>
                                <ojp:TopographicPlaceCode>23009623:2</ojp:TopographicPlaceCode>
                                <ojp:TopographicPlaceName>
                                    <ojp:Text xml:lang="de">Le Grand-Saconnex</ojp:Text>
                                </ojp:TopographicPlaceName>
                            </ojp:TopographicPlace>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Le Grand-Saconnex</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>6.11019</siri:Longitude>
                                <siri:Latitude>46.23134</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:StopPlace>
                                <ojp:StopPlaceRef>8505000</ojp:StopPlaceRef>
                                <ojp:StopPlaceName>
                                    <ojp:Text xml:lang="de">Luzern</ojp:Text>
                                </ojp:StopPlaceName>
                                <ojp:PrivateCode>
                                    <ojp:System>EFA</ojp:System>
                                    <ojp:Value>109739</ojp:Value>
                                </ojp:PrivateCode>
                                <ojp:TopographicPlaceRef>23012061:4</ojp:TopographicPlaceRef>
                            </ojp:StopPlace>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Luzern</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>8.31029</siri:Longitude>
                                <siri:Latitude>47.04612</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:StopPoint>
                                <siri:StopPointRef>ch:1:sloid:5000:1:3</siri:StopPointRef>
                                <ojp:StopPointName>
                                    <ojp:Text xml:lang="de">Luzern</ojp:Text>
                                </ojp:StopPointName>
                                <ojp:PrivateCode>
                                    <ojp:System>EFA</ojp:System>
                                    <ojp:Value>109739:0:3</ojp:Value>
                                </ojp:PrivateCode>
                                <ojp:ParentRef>8505000</ojp:ParentRef>
                                <ojp:TopographicPlaceRef>23012061:4</ojp:TopographicPlaceRef>
                            </ojp:StopPoint>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Luzern</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>8.31029</siri:Longitude>
                                <siri:Latitude>47.04612</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                        <ojp:Location>
                            <ojp:TopographicPlace>
                                <ojp:TopographicPlaceCode>23012061:4</ojp:TopographicPlaceCode>
                                <ojp:TopographicPlaceName>
                                    <ojp:Text xml:lang="de">Luzern</ojp:Text>
                                </ojp:TopographicPlaceName>
                            </ojp:TopographicPlace>
                            <ojp:LocationName>
                                <ojp:Text xml:lang="de">Luzern</ojp:Text>
                            </ojp:LocationName>
                            <ojp:GeoPosition>
                                <siri:Longitude>8.31029</siri:Longitude>
                                <siri:Latitude>47.04612</siri:Latitude>
                            </ojp:GeoPosition>
                        </ojp:Location>
                    </ojp:Places>
                </ojp:StopEventResponseContext>
                <ojp:StopEventResult>
                    <ojp:ResultId>ID-B9404F3B-EC2E-466A-82AD-CF79626F7146</ojp:ResultId>
                    <ojp:StopEvent>
                        <ojp:ThisCall>
                            <ojp:CallAtStop>
                                <siri:StopPointRef>ch:1:sloid:7000:5:10</siri:StopPointRef>
                                <ojp:StopPointName>
                                    <ojp:Text xml:lang="de">Bern</ojp:Text>
                                </ojp:StopPointName>
                                <ojp:PlannedQuay>
                                    <ojp:Text xml:lang="de">10</ojp:Text>
                                </ojp:PlannedQuay>
                                <ojp:ServiceDeparture>
                                    <ojp:TimetabledTime>2024-07-08T12:00:00Z</ojp:TimetabledTime>
                                </ojp:ServiceDeparture>
                                <ojp:Order>9</ojp:Order>
                                <ojp:NotServicedStop>true</ojp:NotServicedStop>
                            </ojp:CallAtStop>
                        </ojp:ThisCall>
                        <ojp:Service>
                            <ojp:OperatingDayRef>2024-07-08</ojp:OperatingDayRef>
                            <ojp:JourneyRef>ch:1:sjyid:100001:2521-003</ojp:JourneyRef>
                            <siri:LineRef>ojp:91015:B</siri:LineRef>
                            <siri:DirectionRef>H</siri:DirectionRef>
                            <ojp:Mode>
                                <ojp:PtMode>rail</ojp:PtMode>
                                <siri:RailSubmode>regionalRail</siri:RailSubmode>
                                <ojp:Name>
                                    <ojp:Text xml:lang="de">Zug</ojp:Text>
                                </ojp:Name>
                                <ojp:ShortName>
                                    <ojp:Text xml:lang="de">IR</ojp:Text>
                                </ojp:ShortName>
                            </ojp:Mode>
                            <ojp:PublishedLineName>
                                <ojp:Text xml:lang="de">IR15</ojp:Text>
                            </ojp:PublishedLineName>
                            <ojp:OperatorRef>ojp:11</ojp:OperatorRef>
                            <ojp:Attribute>
                                <ojp:Text>
                                    <ojp:Text xml:lang="de">Businesszone in 1. Klasse</ojp:Text>
                                </ojp:Text>
                                <ojp:Code>A__BZ</ojp:Code>
                                <siri:FareClassFacility>firstClass</siri:FareClassFacility>
                                <siri:PassengerCommsFacility>businessServices</siri:PassengerCommsFacility>
                            </ojp:Attribute>
                            <ojp:Attribute>
                                <ojp:Text>
                                    <ojp:Text xml:lang="de">Gratis-Internet mit der App SBB FreeSurf</ojp:Text>
                                </ojp:Text>
                                <ojp:Code>A__FS</ojp:Code>
                                <siri:PassengerCommsFacility>internet</siri:PassengerCommsFacility>
                            </ojp:Attribute>
                            <ojp:Attribute>
                                <ojp:Text>
                                    <ojp:Text xml:lang="de">Aussteigeseite: Rechts</ojp:Text>
                                </ojp:Text>
                                <ojp:Code>ojp91015BH_InfoCall73_111055_1</ojp:Code>
                            </ojp:Attribute>
                            <ojp:OriginStopPointRef>ch:1:sloid:1026:1:2</ojp:OriginStopPointRef>
                            <ojp:OriginText>
                                <ojp:Text xml:lang="de">Genève-Aéroport</ojp:Text>
                            </ojp:OriginText>
                            <ojp:DestinationStopPointRef>ch:1:sloid:5000:1:3</ojp:DestinationStopPointRef>
                            <ojp:DestinationText>
                                <ojp:Text xml:lang="de">Romont FR</ojp:Text>
                            </ojp:DestinationText>
                        </ojp:Service>
                        <ojp:Extension>
                            <ojp:TransportTypeName>
                                <ojp:Text xml:lang="de">InterRegio</ojp:Text>
                            </ojp:TransportTypeName>
                            <ojp:PublishedJourneyNumber>
                                <ojp:Text xml:lang="de">2521</ojp:Text>
                            </ojp:PublishedJourneyNumber>
                            <ojp:OperatorName>
                                <ojp:Text xml:lang="de">Schweizerische Bundesbahnen SBB</ojp:Text>
                            </ojp:OperatorName>
                        </ojp:Extension>
                    </ojp:StopEvent>
                </ojp:StopEventResult>
            </ojp:OJPStopEventDelivery>
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
        <p><span class="font-bold">GET</span> /api/{{ apiVersion }}/stationBoard</p>
      </div>
      <h3>Request Parameters</h3>
      <table class="doc__table">
        <thead>
        <tr>
          <td>Name</td>
          <td>Required</td>
          <td>Description</td>
          <td>Example</td>
        </tr>
        </thead>
        <tbody>
        <tr>
          <td>station</td>
          <td>required</td>
          <td>Specified the location name to search for</td>
          <td>Bern</td>
        </tr>
        <tr>
          <td>id</td>
          <td>optional</td>
          <td>The stationRef for the entered location name</td>
          <td>8507000</td>
        </tr>
        <tr>
          <td>datetime</td>
          <td>optional</td>
          <td>Specific date for the station board</td>
          <td>2024-07-02</td>
        </tr>
        <tr>
          <td>limit</td>
          <td>optional</td>
          <td>limits the amount of connections returned</td>
          <td>10</td>
        </tr>
        </tbody>
      </table>
      <div class="flex flex-col mt-4 gap-4">
        <h2>XML vs JSON</h2>
        <div class="doc__url">
          <p><span class="font-bold">GET</span>/api/{{ apiVersion }}/stationBoard?station=Bern&id=8507000&limit=1</p>
        </div>
        <h3>OJP-Service Response</h3>
        <p>{{ t('docsStationBoard.ojpResponse') }}</p>
        <pre class="w-[700px] doc__code-block overflow-x-scroll">
              {{ OJPXML }}
            </pre>
        <h3>OJP-Proxy Response</h3>
        <p>{{ t('docsStationBoard.ojpProxyResponse') }}</p>
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