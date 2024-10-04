import {defineStore} from "pinia";
import type {AxiosCustomError} from "@/composables/services/axios";
import i18n, {type SupportedLocale} from "@/plugins/i18n";
import {ApiVersions} from "@/types/ApiVersions";

export const useUiStore = defineStore('ui', {
    persist: {
        paths: ['ui', 'apiVersion']
    },
    state: () => ({
        axiosError: null as AxiosCustomError | null,
        queriedEndpoint: null as string | null,
        locale: 'en-CH',
        apiVersion: ApiVersions.v1 as ApiVersions,
    }),
    getters: {
        getApiVersion(): ApiVersions {
            return this.apiVersion;
        }
    },
    actions: {
        async changeLocale(l: string): Promise<void> {
            this.locale = l;
            i18n.global.locale.value = l as SupportedLocale;
        },
        async changeApiVersion(v: ApiVersions): Promise<void>{
            this.apiVersion = v;
        }
    }
})