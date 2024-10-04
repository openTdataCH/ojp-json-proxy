import {defineStore} from "pinia";
import type {APIParameters} from "@/types/DevMode/APIParameters";
import type {SwaggerEndpoint, SwaggerParams} from "@/types/SwaggerModels";
import {useSwaggerStore} from "@/stores/swagger";
import {useAuthStore} from "@/stores/auth";

interface Endpoint {
    endpoint: string
    parameters: APIParameters;
}

interface State {
    activeStep: number
    endpoints: SwaggerEndpoint[] | null;
}

export const useDemoPageStore = defineStore('demo', {
    state: (): State => ({
        activeStep: 0,
        endpoints: []
    }),
    getters: {
        getCurrentActiveStep(state): number{
            return state.activeStep;
        },
        getParametersForEndpoint(state) {
            return (path: string): SwaggerParams[] | undefined => {
                const found = state.endpoints?.find(e => e.path === path);
                return found ? found.methods.flatMap(method => method.parameters) : undefined;
            };
        },
        getQueryParametersForEndpoint(state) {
            return (path: string): SwaggerParams[] | undefined => {
                const found = state.endpoints?.find(e => e.path === path);
                return found ? found.methods.flatMap(method => method.parameters.filter(param => param.in === 'query')) : undefined;
            };
        }
    },
    actions: {
        setCurrentActiveStep(stepNr: number): void{
            this.activeStep = stepNr;
        },
        setParameterValueForEndpoint(path: string, parameterName: string, value: any) {
            const existingEndpoint = this.endpoints?.find(e => e.path === path);
            if (existingEndpoint) {
                for (let method of existingEndpoint.methods) {
                    const param = method.parameters.find(p => p.name === parameterName);
                    if (param) {
                        param.value = value;
                        console.log("set value for parameter")
                        return; // Exit the function once the parameter is found and set
                    }
                }
                console.log("Parameter not found in the methods");
            }
        },
        async initializeSwaggerDoc(){
            const swaggerStore = useSwaggerStore();
            await swaggerStore.fetchSwaggerJSON()
                .then(res => this.endpoints = JSON.parse(JSON.stringify(swaggerStore.swaggerJSON)))
        }
    }
});