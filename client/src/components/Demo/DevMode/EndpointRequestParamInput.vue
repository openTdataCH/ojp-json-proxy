<script setup lang="ts">

import {type PropType, ref} from "vue";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover";
import {useI18n} from "vue-i18n";
import {getLocalTimeZone} from "@internationalized/date";
import {Calendar} from "@/components/ui/calendar";
import {InputType} from "@/types/DevMode/InputType";


const {d} = useI18n();

const props = defineProps({
    type: {type: Number as unknown as PropType<InputType>, default: InputType.String},
    required: {type: Boolean, default: false}
})

const value = defineModel<any>();
</script>

<template>
    <template v-if="type === InputType.DateTime">
        <Popover>
            <PopoverTrigger as-child>
                <input class="api-parameters__input"
                       type="text" :value="value ? d(value.toDate(getLocalTimeZone(), 'dateTime')) : 'Pick a date'">
            </PopoverTrigger>
            <PopoverContent class="w-auto p-0">
                <Calendar v-model="value" initial-focus/>
            </PopoverContent>
        </Popover>
    </template>

    <template v-else-if="type === InputType.Number">
        <input class="api-parameters__input"
               type="number" v-model="value" :required="required">
    </template>

    <template v-else-if="type === InputType.String">
        <input class="api-parameters__input"
               type="text" v-model="value" :required="required">
    </template>
</template>

<style scoped lang="scss">
@import "../../../assets/scss/variables";

.api-parameters__input {
    padding: 7px;
    border: 1px solid $pt-main-black;
    background: $pt-main-white;
    border-radius: 5px;
    height: min-content;
}
</style>