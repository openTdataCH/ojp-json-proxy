<script setup lang="ts">
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import {ApiVersions} from "@/types/ApiVersions";
import {ref, toRef, toRefs, watch} from "vue";
import {useUiStore} from "@/stores/ui";

const uiStore = useUiStore();
const apiVersion = ref(uiStore.apiVersion);

watch(apiVersion, (value, oldValue) =>
    uiStore.changeApiVersion(value)
)

</script>

<template>
  <Select v-model="apiVersion" >
    <SelectTrigger class="max-w-min select__api-version">
      <SelectValue placeholder="Select Api Version" />
    </SelectTrigger>
    <SelectContent>
      <SelectGroup>
        <SelectLabel>OJP Version</SelectLabel>
        <SelectItem value="v1">
          {{ ApiVersions.v1 }}
        </SelectItem>
        <SelectItem value="v2">
          {{ ApiVersions.v2 }}
        </SelectItem>
      </SelectGroup>
    </SelectContent>
  </Select>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.select__api-version {
  border: 1px solid $pt-main-black;
  border-radius: 5px;
  background: $pt-main-black;
  padding: 8px 10px 8px 15px;
  gap: 0.5rem;
  color: $pt-main-white;
  font-family: 'Inter Bold';
}
</style>