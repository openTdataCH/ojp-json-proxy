<script setup lang="ts">
import {computed, onMounted, type PropType, reactive, ref, watch} from "vue";
import type {SwaggerMethod, SwaggerParams} from "@/types/SwaggerModels";
import VueJsonPretty from "vue-json-pretty";
import {InputType} from "@/types/DevMode/InputType";
import {type AxiosCustomError, useAxios} from "@/composables/services/axios";
import {useAuthStore} from "@/stores/auth";
import {useI18n} from "vue-i18n";
import EndpointRequestParamInput from "@/components/Demo/DevMode/EndpointRequestParamInput.vue";
import EndpointRequestButton from "@/components/Demo/DevMode/EndpointRequestButton.vue";
import type {ResponseTimes} from "@/types/ResponseTimes";
import EndpointRequestResponseError from "@/components/Demo/DevMode/EndpointRequestResponseError.vue";
import type {EndpointRequestError} from "@/types/EndpointRequestError";
import EndpointRequestResponseTime from "@/components/Demo/DevMode/EndpointRequestResponseTime.vue";
import 'vue-json-pretty/lib/styles.css';

const axios = useAxios();
const authStore = useAuthStore();
const {t} = useI18n();

const props = defineProps({
    endpoint: {type: String, required: true},
    method: {type: Object as PropType<SwaggerMethod>, required: true}
})

const baseUrl = import.meta.env.VITE_API_BASEURL as string;
const displayMsg = ref<boolean>(false);
const userInputParameterValues = reactive<{ [key: string]: any }>({});
const response = ref<string | null>(null);
const responseTimes = ref<ResponseTimes>({total: '', ojp: []} as ResponseTimes);
const endpointRequestError = ref<EndpointRequestError>({message: null, axiosError: null} as EndpointRequestError);

const methodBlock = ref();
const paperPlane = ref();
const loading = ref(false);

onMounted(() => {
    paperPlane.value.style.right = '2.8em';
    paperPlane.value.style.display = 'none';
})

const relevantParameters = computed(() => {
    return props.method?.parameters.filter(param => param.in !== 'header')
})

const fullURL = computed(() => {
    let paramChain = "";
    Object.keys(userInputParameterValues).forEach(key => {
        if (userInputParameterValues[key] !== null && userInputParameterValues[key] !== '') {
            const chainChar = paramChain.includes('?') ? '&' : '?';
            paramChain += chainChar + key + '=' + userInputParameterValues[key];
        }
    })

    return baseUrl + props.endpoint + paramChain;
});

function getInputType(param: SwaggerParams): InputType {
    switch (param.schema.type) {
        case 'integer':
            return InputType.Number;
        case 'string':
            if (param.name === 'datetime') {
                return InputType.DateTime;
            } else {
                return InputType.String;
            }
        default:
            return InputType.String;
    }
}

function sendRequest(): void {
    // Reset validation error and remove animations
    resetResponses();
    methodBlock.value.classList.remove('shake-animation', 'pop-animation');

    if (paramMissing.value.length > 0) {
        methodBlock.value.classList.add('shake-animation');
        endpointRequestError.value!.message = paramMissing.value.length === 1
            ? `Param "${paramMissing.value[0].name}" is required!`
            : `Following params are required: ${paramMissing.value.map(param => param.name).join(', ')}`;
        return;
    }

    loading.value = true;
    paperPlane.value.style.display = 'flex';
    paperPlane.value.style.transform = 'rotate(-180deg)';
    paperPlane.value.style.transition = 'right 5s linear';

    setTimeout(() => {
        paperPlane.value.style.right = '25em';
    }, 100);

    axios.get(props.endpoint, {params: userInputParameterValues})
        .then(res => {
            responseTimes.value.total = res.headers['x-response-time-ms'];
            const ojpResponseTimes = Object.keys(res.headers).filter(key => key.includes('ojp-service'));
            ojpResponseTimes.forEach(key => responseTimes.value.ojp.push(res.headers[key]));
            handleSuccessResponse(res);
        })
        .catch((err: AxiosCustomError) => {
            //handleErrorResponse(err.httpStatus + ' (' + err.httpStatusText + '): ' + err.canonicalErrorMsg);
            handleErrorResponse(err);
        });
}

function handleSuccessResponse(res: any) {
    animatePaperPlane('right 0.75s linear', '42em', () => {
        resetPaperPlane();
        methodBlock.value.classList.add('pop-animation');
        loading.value = false;
        response.value = res.data;
    });
}

function handleErrorResponse(err: AxiosCustomError) {
    animatePaperPlane('right 0.75s linear', '42em', () => {
        resetPaperPlane();
        methodBlock.value.classList.add('shake-animation');
        endpointRequestError.value.axiosError = err;
        loading.value = false;
    });
}

function animatePaperPlane(transition: string, position: string, callback: any) {
    paperPlane.value.style.transition = transition;
    setTimeout(() => {
        paperPlane.value.style.right = position;
    }, 100);
    setTimeout(callback, 850);
}

function resetPaperPlane() {
    paperPlane.value.style.display = 'none';
    paperPlane.value.style.right = '2.8em';
}


async function copyToClipBoard() {
    const curlCmd = `curl --location '${fullURL.value}' --header 'Authorization: Bearer ${authStore.ojpToken}'`;

    await navigator.clipboard.writeText(curlCmd);
    displayMsg.value = true;
    setTimeout(() => {
        displayMsg.value = false;
    }, 2000);
}

const paramMissing = computed<SwaggerParams[]>(() => {
    const result = [] as SwaggerParams[];
    const requiredParams = relevantParameters.value.filter(param => param.required);
    requiredParams.forEach(param => {
        if (userInputParameterValues[param.name] === null || userInputParameterValues[param.name].replace(/\s/g, '') === '') result.push(param);
    })
    return result;
})

function beforeEnter(el: any): void {
    el.style.height = '0';
}

function enter(el: any, done: any): void {
    el.style.height = (el.scrollHeight + 15) + 'px';
    el.addEventListener('transitionend', done);
}

function leave(el: any, done: any): void {
    el.style.height = el.scrollHeight + 'px';
    // Force reflow
    getComputedStyle(el).height;
    el.style.height = '0';
    el.addEventListener('transitionend', done);
}

function resetResponses(): void {
    responseTimes.value = {total: '', ojp: []};
    response.value = null;
    endpointRequestError.value = {message: null, axiosError: null};
}

watch(() => relevantParameters.value, (value) => {
    value.forEach(key => {
        userInputParameterValues[key.name] = null;
    })
}, {immediate: true, deep: true});

</script>

<template>
    <div class="border border-black rounded bg-black">
        <!-- Request Bar -->
        <div class="flex flex-row gap-1.5 w-full rounded-t items-center border-b border-black p-1.5 bg-white relative">

            <div ref="paperPlane" class="align-middle absolute">
                <img src="/src/assets/icons/paperplane.svg" width="21" height="21">
            </div>

            <div ref="methodBlock" class="flex-grow-0 p-1 bg-black text-white rounded">
                <span class="text-center font-bold px-2">{{ method.name.toUpperCase() }}</span>
            </div>
            <div class="flex-grow max-w-full text-nowrap overflow-x-scroll scrollbar-hide">
                <span class="italic" v-if="displayMsg">copied to Clipboard!</span>
                <span v-else>{{ fullURL }}</span>
            </div>
            <div class="flex-grow-0 flex-shrink-0 flex flex-row items-center gap-3 mr-1.5">

                <Transition>
                  <EndpointRequestButton v-if="!loading" :tooltip-msg="t('action.send')"
                                           icon-path="/src/assets/icons/paperplane.svg" @click="sendRequest"/>
                </Transition>
                <EndpointRequestButton :tooltip-msg="t('action.curlToClipBoard')" icon-path="/src/assets/icons/copy.svg"
                                       @click="copyToClipBoard"/>

            </div>
        </div>

        <Transition name="expand"
                    @before-enter="beforeEnter"
                    @enter="enter"
                    @leave="leave">
            <div v-if="endpointRequestError.message !== null || endpointRequestError.axiosError !== null"
                 class="font-bold bg-red-100 text-red-600 p-3 border-b border-black">
                <EndpointRequestResponseError v-if="endpointRequestError.axiosError !== null" :error="endpointRequestError.axiosError" />
                <span v-else>{{ endpointRequestError.message }}</span>
            </div>
        </Transition>

        <!-- Parameters -->
        <div class="flex flex-col gap-3 w-full p-3 bg-white rounded-b">
            <span class="font-bold mb-1.5">Parameters</span>

            <div v-for="parameter in relevantParameters" class="grid grid-cols-[1fr] md:grid-cols-[1fr_1fr_1fr]">
                <div class="flex flex-col">
                    <div>
                        <span class="font-bold">{{ parameter.name }}</span>
                        <span v-if="parameter.required !== undefined && parameter.required"
                              class="text-red-500">*</span>
                        <span class="text-purple ml-2">{{ parameter.schema.type }}</span>
                    </div>
                    <p v-if="parameter.description !== undefined" class="m-0">
                        {{ parameter.description }}
                    </p>
                </div>
                <EndpointRequestParamInput v-if="userInputParameterValues !== undefined" :type="getInputType(parameter)"
                                           :required="parameter.required"
                                           v-model="userInputParameterValues[parameter.name]"/>
            </div>
        </div>

        <!-- Response -->
        <Transition name="expand"
                    @before-enter="beforeEnter"
                    @enter="enter"
                    @leave="leave">
            <div v-if="response !== null" class="rounded-b px-3 py-6">
                <div class="flex flex-col pb-3">
                    <div class="flex flex-row justify-between">
                        <span class="font-bold text-white">Response</span>
                        <button title="collapse" class="response__collapse-button mr-1.5" @click="response = null">
                            <img src="/src/assets/icons/x_white.svg" width="14" height="14">
                        </button>
                    </div>
                    <EndpointRequestResponseTime :response-times="responseTimes"/>
                </div>
                <div class="api_response__response bg-black-darken rounded mt-3 mb-12 text-gray-300">
                    <vue-json-pretty theme="dark" :data="response"></vue-json-pretty>
                </div>
            </div>
        </Transition>
    </div>
</template>

<style scoped lang="scss">
@import "../../../assets/scss/variables";


.api-request__button:hover {
    filter: brightness(3);
}

.response__collapse-button:hover {
    filter: grayscale(1);
}

.api_response__response {
    height: 580px;
    overflow-y: scroll;
    tab-size: 3;
}

.expand-enter-active,
.expand-leave-active {
    transition: height 0.5s ease;
}

.expand-enter-from,
.expand-leave-to {
    height: 0;
}

/* For Webkit-based browsers (Chrome, Safari and Opera) */
.scrollbar-hide::-webkit-scrollbar {
    display: none;
}

/* For IE, Edge and Firefox */
.scrollbar-hide {
    -ms-overflow-style: none;  /* IE and Edge */
    scrollbar-width: none;  /* Firefox */
}

.pop-animation {
    transition: transform .3s ease-in-out;
    animation: pop 500ms ease-in-out;
}

@keyframes pop {
    0% {
        transform: scale(1);
    }
    50% {
        transform: scale(1.2); /* Scale up to 1.2 times its original size */
    }
    100% {
        transform: scale(1); /* Scale back to original size */
    }
}

.shake-animation {
    animation: shake 300ms ease-in-out;
    background-color: theme('colors.red.500');
}

@keyframes shake {
    0% {
        transform: translateX(0);
    }
    20% {
        transform: translateX(-5px);
    }
    40% {
        transform: translateX(5px);
    }
    60% {
        transform: translateX(-5px);
    }
    80% {
        transform: translateX(5px);
    }
    100% {
        transform: translateX(0);
    }
}

</style>