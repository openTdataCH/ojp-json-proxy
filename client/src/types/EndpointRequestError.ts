import type {AxiosCustomError} from "@/composables/services/axios";

export interface EndpointRequestError {
    message: string | null;
    axiosError: AxiosCustomError | null;
}