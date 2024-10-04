export interface LIR {
    "locations": Location[],
}

export interface Location {
    "id": string,
    "name": string,
    "transportType": string,
    "probability": number
}