
import {useAxios} from "@/composables/services/axios";
import type {DiDok} from "@/types/old/DiDok";
import type {LIR} from "@/types/LIR";
import {useUiStore} from "@/stores/ui";

export class LIRService {

    public async getLIRForLocation(locationName: string, limit?: number): Promise<LIR> {
        const uiStore = useUiStore();
        const apiVersion = uiStore.apiVersion;
        console.log("used lir service");
        try {
            return (await useAxios().get<LIR>(`/api/${apiVersion}/locationInformation`, {
                params: {
                    locationName: locationName,
                    limit: limit
                }
            })).data;
            //return response.data;
        } catch (error) {
            console.error('Error fetching lir:', error);
            Promise.reject(error);
            throw new Error('Failed to fetch lir for ' + locationName);
        }
    }

    public async getSingleLIRForLocation(locationName: string) {
        const uiStore = useUiStore();
        const apiVersion = uiStore.apiVersion;
        console.log("used single lir service");
        try {
            const responseData = (await useAxios().get<LIR>(`/api/${apiVersion}/locationInformation`, {
                params: {
                    locationName: locationName,
                }
            })).data;
            return responseData.locations.slice(0)
            //return response.data;
        } catch (error) {
            console.error('Error fetching lir:', error);
            Promise.reject(error);
            throw new Error('Failed to fetch lir for ' + locationName);
        }
    }
}

const instance = new LIRService();

export default function useLIRService(): LIRService {
    return instance;
}