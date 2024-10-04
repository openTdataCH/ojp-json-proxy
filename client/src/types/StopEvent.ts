import type {Connection} from "@/types/Connection";

export interface StopEvent{
    "responseTimeStamp": Date,
    "connections": Connection[]
}