import {useAxios} from "@/composables/services/axios";
import type {StopEvent} from "@/types/StopEvent";
import type {DiDok} from "@/types/old/DiDok";
import type {StationBoard} from "@/types/StationBoard";
import {useUiStore} from "@/stores/ui";

export class StationBoardService{

    public async getStationBoardForLocation(station: string, limit?: number, id?: number, datetime?: string ): Promise<StationBoard>{
        const uiStore = useUiStore();
        const apiVersion = uiStore.apiVersion;
        console.log("used get station-board service");
        try {
            const response = await useAxios().get<StationBoard>(`/api/${apiVersion}/stationBoard`, {
                params: {
                    station: station,
                    limit: limit,
                    id: id,
                    datetime: datetime
                }
            });
            return response.data; // Return the fetched data
        } catch (error) {
            console.error('Error fetching station board:', error);
            throw new Error('Failed to fetch station board');
        }
    }
}

const instance = new StationBoardService();

export default function useStationBoardService(): StationBoardService {
    return instance;
}