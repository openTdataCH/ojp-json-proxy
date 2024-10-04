import {defineStore} from "pinia";

export const useAuthStore = defineStore('auth', {
    persist: {
        paths: ['ojpToken']
    },
    state: () => ({
    ojpToken: null
}),
    getters: {

    },
    actions: {
        clearToken(): void {
            this.ojpToken = null
        }
    }
})