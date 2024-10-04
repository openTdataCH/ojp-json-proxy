export interface Connection {
    "name": string,
    "category": string,
    "number": number,
    "operator": string,
    "to": string,
    "stop": {
    "station": string | null,
        "arrival": Date,
        "departure": Date,
        "delay": number | null,
        "platform": number | null,
        "prognosis": number | null
    }
}