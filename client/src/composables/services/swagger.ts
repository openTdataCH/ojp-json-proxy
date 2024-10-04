import {useAxios} from "@/composables/services/axios";
import useSwaggerHelper from "@/composables/helpers/swagger";
import type {SwaggerEndpoint} from "@/types/SwaggerModels";
import {useUiStore} from "@/stores/ui";

export class SwaggerService {
    private static baseUrl = import.meta.env.VITE_SWAGGER_BASEURL;
    private static swaggerJsonUrl = '/swagger.json';

    public async getSwaggerJson(): Promise<SwaggerEndpoint[]> {
        const uiStore = useUiStore();
        const apiVersion = uiStore.apiVersion;
        try {
            const res = await useAxios().get('', {baseURL: SwaggerService.baseUrl + '/' + apiVersion + SwaggerService.swaggerJsonUrl});
            return useSwaggerHelper().swaggerJsonToModel(res.data);
        } catch (error) {
            console.error('Error fetching Swagger JSON:', error);
            throw error;
        }
    }
}

const instance = new SwaggerService();

export default function useSwaggerService(): SwaggerService {
    return instance;
}