import {defineStore} from "pinia";
import type {SwaggerEndpoint, SwaggerParams} from "@/types/SwaggerModels";
import useSwaggerService, {SwaggerService} from "@/composables/services/swagger";

const swaggerService = useSwaggerService();

export const useSwaggerStore = defineStore('swagger', {
    state: () => ({
        swaggerJSON: null as SwaggerEndpoint[] | null
    }),
    getters: {
        getQueryParametersForEndpoint(state) {
            return (path: string): SwaggerParams[] | undefined => {
                const found = state.swaggerJSON?.find(e => e.path === path);
                return found ? found.methods
                    .flatMap(method => method.parameters
                        .filter(param => param.in === 'query')) : undefined;
            };
        }
    },
    actions: {
        async fetchSwaggerJSON(){
            await swaggerService.getSwaggerJson()
                .then(res => this.swaggerJSON = res)
        }
    }
});