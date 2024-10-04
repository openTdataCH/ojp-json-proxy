<script setup lang="ts">
import {type PropType, ref} from "vue";
import type {AxiosCustomError} from "@/composables/services/axios";
import {Button} from "@/components/ui/button";
import {useI18n} from "vue-i18n";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle
} from "@/components/ui/dialog";
import PrimaryButton from "@/components/Main/Controls/PrimaryButton.vue";
import ModalPopover from "@/components/Main/ModalPopover.vue";
import SecondaryButton from "@/components/Main/Controls/SecondaryButton.vue";

const {t} = useI18n();

const props = defineProps({
    error: {type: Object as PropType<AxiosCustomError>, required: true}
})

const modalComponent = ref();

function openContact(): void {
    window.open('https://opentransportdata.swiss/en/contact/', '_blank');
}
</script>

<template>
    <div class="flex flex-row items-center gap-3">
        <div class="flex flex-col items-center border border-red-500 rounded py-1 px-3">
            <span>{{ error.httpStatus }}</span>
            <span class="font-light text-xs">{{ error.httpStatusText }}</span>
        </div>
        <div class="flex flex-col">
            <span>{{ error.canonicalErrorMsg }}</span>
            <span class="font-light text-xs">{{ error.errorMessage }}</span>
        </div>
        <div class="flex-grow flex justify-end">
            <Button popovertarget="showDetails" variant="link" class="text-red-500">{{ t('action.details') }}</Button>
        </div>
    </div>

  <ModalPopover ref="modalComponent" popover-id="showDetails">
    <div class="modal__details">
      <h3>{{ error.httpStatusText }}: {{ error.errorMessage }}</h3>
      <p v-if="error.response !== null" class="font-normal">{{ error.response }}</p>
      <div class="flex flex-row gap-4">
        <PrimaryButton :text="t('ui.close')" @click="modalComponent.closeModal()"/>
        <SecondaryButton @click="openContact" :text="t('action.contactSki')"></SecondaryButton>
      </div>
    </div>
  </ModalPopover>
</template>

<style scoped lang="scss">
@import "../../../assets/scss/variables";

.modal__details{
  width: 800px;
  padding: 2rem;
  background: $pt-main-white;
  border-radius: 15px;
  margin-top: 1rem;

  display: flex;
  flex-direction: column;

  a {
    margin-top: 1rem;
  }


}

</style>