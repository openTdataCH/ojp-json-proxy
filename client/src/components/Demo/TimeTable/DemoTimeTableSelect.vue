<script setup lang="ts">
import DemoTimeTableInput from "@/components/Demo/TimeTable/DemoTimeTableInput.vue";
import {onMounted, type Ref, ref, watch} from "vue";
import type { LIR } from "@/types/LIR";
import useLIRService from "@/composables/services/lir";

const selectedLIR = defineModel<string>('lir');
const selectedStation = defineModel<string>('station');

const showDropDown = ref<boolean>(false);

const station = ref<string>("");

const lirList = ref<LIR>();

const multiSelectContainer: Ref<HTMLDivElement | undefined> = ref();
const inputComponent = ref();

function getLIR(inputString: string): void {
  useLIRService().getLIRForLocation(inputString)
      .then(res => {
        lirList.value = res;
      })
}

function getSingleLIR(inputString: string) {
  useLIRService().getSingleLIRForLocation(inputString)
      .then(res => updateLocation(res[0].id, res[0].name))
}

watch(() => selectedStation.value, (value) => {
  console.log(value);
  if (value.length > 2) {
    getLIR(value);
  }
});

function updateLocation(lir: string, stationName: string){
  selectedLIR.value = lir;
  selectedStation.value = stationName;
  inputComponent.value.inputField.blur();
  showDropDown.value = false;
  window.removeEventListener("click", closeDropDown);
}

function handleFocusIn(){
  showDropDown.value = true;
  registerEventListenerClickOutside();
}

function registerEventListenerClickOutside() {
  window.addEventListener("click", closeDropDown, false);
}

function closeDropDown(e: Event){
  if (!(e.target === multiSelectContainer.value || e.composedPath().includes(multiSelectContainer.value))) {
    showDropDown.value = false;
    // emits("change:value", selectedOptions);
    window.removeEventListener("click", closeDropDown);
  }
}

function handleEnterPress() {
  console.log('Enter pressed with value:', selectedStation.value);
  // Call any function you want with the input value
  if (selectedStation.value){
    getSingleLIR(selectedStation.value)
  }
}

</script>

<template>
  <div ref="multiSelectContainer" class="flex flex-col w-full">
    <DemoTimeTableInput ref="inputComponent" @focus="handleFocusIn" @keydown.enter="handleEnterPress"  v-model="selectedStation"></DemoTimeTableInput>
    <div class="relative">
      <div v-if="showDropDown" class="flex flex-col absolute z-10 w-full cursor-pointer select__dropdown">
        <div v-for="option in lirList?.locations" :key="option.id" class="select__option" @mousedown.prevent @click="updateLocation(option.id, option.name)">
          <label class="cursor-pointer">
            <span>{{ option.name }}</span>
          </label>
        </div>
      </div>
    </div>
  </div>

</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.select__option {
  width: 100%;
  padding: 9px;
  border-color: $pt-main-black;
  border-style: solid;
  border-width: 0 1px 1px 1px;
  background: $pt-main-white;

  &:hover{
      background: $pt-main-light-gray;
  }
}

.select__dropdown {
  max-height: 400px;
  overflow-y: scroll;
}

.select__dropdown > div:first-of-type {
  border-top-left-radius: 5px;
  border-top-right-radius: 5px;
  border-top: 1px $pt-main-black solid;
}

.select__dropdown > div:last-of-type {
  border-bottom-left-radius: 5px;
  border-bottom-right-radius: 5px;
}

</style>