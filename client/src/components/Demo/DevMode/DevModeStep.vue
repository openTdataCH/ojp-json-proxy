<script setup lang="ts">
import {ref} from "vue";
import {useDemoPageStore} from "@/stores/demo";
import {storeToRefs} from "pinia";

interface Props {
    stepNr: number
    devMode: boolean
}

const isActive = ref<boolean>(false);

const demoStore = useDemoPageStore();
const {getCurrentActiveStep} = storeToRefs(demoStore);

function handleFocusIn() {
    if (props.devMode) {
        demoStore.setCurrentActiveStep(props.stepNr)
    }
}

function handleFocusOut() {
    if (props.devMode) {
        demoStore.setCurrentActiveStep(0)
    }
}

const props = defineProps<Props>();
</script>

<template>
    <div class="step__container w-full pl-4" @focusin="handleFocusIn" @focusout="handleFocusOut"
         :class="{ active: stepNr === getCurrentActiveStep }">
        <div class="p-[1rem]">
            <span v-if="devMode">{{ "step " + stepNr }}</span>
            <slot/>
        </div>
    </div>
</template>

<style scoped lang="scss">
.step__container {
    box-sizing: border-box;
    margin: 0;
    position: relative;

    span {
        font-weight: 700;
        letter-spacing: 2.1px;
        text-transform: uppercase;
    }
}

.active {
    position: relative;
    //margin: calc(1rem - 1px) calc(1rem - 1px) calc(1rem - 1px) calc(1rem - 4px);;
    //border-radius: 5px;
    outline: 1px solid #59F;
    //border-inline: 1px solid #59F;
    //border-right: 1px solid #59F;
    //border-bottom: 1px solid #59F;
    //border-top: 1px solid #59F;
    //border-left: 4px solid #59F;
    background: rgba(31, 120, 255, 0.20);
}

.active:before {
    content: ''; /* Create a pseudo-element for the border */
    position: absolute; /* Position the border absolutely */
    top: 0;
    bottom: 0;
    left: 0;
    width: 4px; /* Adjust this width as needed */
    background-color: #59F;
}


</style>