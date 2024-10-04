<script setup lang="ts">
import {computed, type PropType} from "vue";
import type {ResponseTimes} from "@/types/ResponseTimes";

const props = defineProps({
    responseTimes: {type: Object as PropType<ResponseTimes>, required: true}
})

/*const responseTimeGridCols = computed<string>(() => {
    const total = responseTimes.value.total;
    const arr: number[] = [];
    responseTimes.value.ojp.forEach(time => arr.push(Math.round(100/+total * +time)));
    const diff = 100 - arr.reduce((curr, acc) => curr + acc, 0);
    return 'grid-cols-[' + arr.join('%_') + '%_' + diff + '%]';
})*/

const responseTimeGridCols = computed<string>(() => {
    const total = props.responseTimes.ojp.reduce((curr, acc) => +curr + +acc, 0);
    const arr: number[] = [];
    props.responseTimes.ojp.forEach(time => arr.push(Math.round(100 / +total * +time)));
    return 'grid-cols-[' + arr.join('%_') + '%]';
})

const reponseTimeGridWidth = computed<string>(() => {
    const totalOjp = props.responseTimes.ojp.reduce((curr, acc) => +curr + +acc, 0);
    const width = 100 / +props.responseTimes.total * +totalOjp;
    return width.toString();
})
</script>

<template>
    <div>
        <span class="font-bold text-xs text-gray-300">{{ responseTimes.total }}ms </span>
        <span class="text-xs text-gray-500">(ojp: {{ responseTimes.ojp.join('+') }}ms)</span>
    </div>
<!--    <div class="flex items-center h-3" :style="{ width: 100 + '%' }">
        <div class="block bg-gray-300 h-1/2 w-full rounded"></div>
    </div>
    <div
        :class="'grid grid-cols-' + responseTimes.ojp.length + ' ' + responseTimeGridCols + ' grid-rows-' + (responseTimes.ojp.length)"
        :style="'width: ' + reponseTimeGridWidth + '%'">

        <div
            v-for="(times, index) in responseTimes.ojp"
            :key="index"
            :class="'row-start-' + (index+1) + ' col-start-' + (index+1)"
            class="flex items-center h-3"
        >
            <div class="block bg-gray-500 h-1/2 w-full rounded"></div>
        </div>
    </div>-->
</template>

<style scoped lang="scss">

</style>