<script setup lang="ts">

import {computed, ref} from "vue";
import {useAuthStore} from "@/stores/auth";
import MainButton from "@/components/Main/Controls/IconButton.vue";
import PrimaryButton from "@/components/Main/Controls/PrimaryButton.vue";
import SecondaryButton from "@/components/Main/Controls/SecondaryButton.vue";
import ModalPopover from "@/components/Main/ModalPopover.vue";
import {useI18n} from "vue-i18n";

const { t } = useI18n();
const authStore = useAuthStore();
const modalComponent = ref();

const validateToken = computed(() => ({
  isTokenSet: !!authStore.ojpToken,
  response: ( authStore.ojpToken ? "API Key" : " Set API Key" ),
  imageUrl: ( authStore.ojpToken ? "/src/assets/icons/bubble_valid.svg" : undefined )
}));
</script>

<template>
  <MainButton popovertarget="manageApiToken" mainIconURL="/src/assets/icons/key_outline.svg"
              :text="validateToken.response"
              :secondaryIconURL="validateToken.imageUrl" />
  <ModalPopover ref="modalComponent" popover-id="manageApiToken">
    <div class="modal__token_input">
      <h3>{{ t('ui.apiToken') }}</h3>
      <p>{{ t('ui.apiTokenDescription') }}</p>
      <input class="token-input" type="text" v-model="authStore.ojpToken" placeholder="ojp-token" @keyup.enter="modalComponent.closeModal()"/>
      <div class="flex flex-row gap-4">
        <PrimaryButton :text="t('ui.save')" @click="modalComponent.closeModal()"/>
        <SecondaryButton :text="t('ui.clear')" @click="authStore.clearToken()" />
      </div>
      <a class="underline hover:no-underline w-max" href="https://opentransportdata.swiss/en/dev-dashboard/">{{ t('ui.apiTokenRenew') }}</a>
    </div>
  </ModalPopover>
</template>

<style scoped lang="scss">
@import "src/assets/scss/variables";

.button_container {
  border: 1px solid $pt-main-black;
  border-radius: 5px;
  background: $pt-main-white;
  padding: 8px;
  gap: 10px;
  font-family: 'Inter', sans-serif;
  font-weight: bold;
}

.modal__token_input{
  width: 600px;
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

.token-input {
  border: 1px solid $pt-main-black;
  border-radius: 5px;
  padding: 9px;
  width: 100%;
  margin-bottom: 2rem;
}



</style>