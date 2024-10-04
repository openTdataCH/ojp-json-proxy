<script setup lang="ts">

import {Tooltip, TooltipContent, TooltipProvider, TooltipTrigger} from "@/components/ui/tooltip";

const props = defineProps({
    iconPath: {type: String, required: true},
    tooltipMsg: {type: String, required: true}
})

function getImageUrl(): string {
    return new URL(`${props.iconPath}`, import.meta.url).toString();
}
</script>

<template>
    <div class="flex align-middle">
        <TooltipProvider>
            <Tooltip>
                <TooltipTrigger>
                    <button class="hover:brightness-200 flex align-middle">
                        <img :src="getImageUrl()" width="21" height="21">
                    </button>
                </TooltipTrigger>
                <TooltipContent>{{ tooltipMsg }}</TooltipContent>
            </Tooltip>
        </TooltipProvider>
    </div>
</template>

<style scoped lang="scss">

</style>