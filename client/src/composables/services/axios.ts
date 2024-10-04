import axios, {AxiosError, AxiosHeaders, type AxiosInstance} from 'axios';
import {useAuthStore} from "@/stores/auth";
import {useUiStore} from "@/stores/ui";
import {useI18n} from "vue-i18n";
import i18n from "@/plugins/i18n";

const baseUrl = import.meta.env.VITE_API_BASEURL as string;

//create axios instance
const axiosInstance: AxiosInstance = axios.create({
    baseURL: baseUrl,
});

//request interceptors
axiosInstance.interceptors.request.use(
    (config) => {
        const authStore = useAuthStore();
        const uiStore = useUiStore();

        uiStore.queriedEndpoint = config.url ?? null;

        //prepare headers if necessary
        if (config.headers === undefined) {
            config.headers = new AxiosHeaders();
        }

        if (authStore.ojpToken !== null && !config.headers.has('Authorization')) {
            config.headers.set('Authorization', `Bearer ${authStore.ojpToken}`);
        }
        console.log(config);
        return config;
    },
    (error) => Promise.reject(error),
);

//response interceptors
axiosInstance.interceptors.response.use(
    (response) => {
        const uiStore = useUiStore();
        uiStore.axiosError = null;
        return response;
    },
    (error: AxiosError<any, any>): any => {
        const uiStore = useUiStore();


        console.log(error)

        const serverError: OjpProxyErrorResponse = error.response?.data;
        const customError = {
            canonicalErrorMsg: i18n.global.t(serverError.canonical),
            errorMessage: error.message,
            errorCode: error.code,
            httpStatus: error.response?.status,
            httpStatusText: error.response?.statusText,
            response: serverError.exception
        } as AxiosCustomError;

        console.log(customError);
        uiStore.axiosError = customError;
        throw customError;
    },
);

export function useAxios(): AxiosInstance {
    return axiosInstance;
}

export interface AxiosCustomError {
    canonicalErrorMsg: string;
    errorMessage: string;
    errorCode: string;

    httpStatus: number;
    httpStatusText: string;
    response: string | null;
}

export interface OjpProxyErrorResponse {
    canonical: string;
    exception: string | null;
}