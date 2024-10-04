export interface APIParameters {
    [key: string]: APIParameterDetail
}

export interface APIParameterDetail {
    value: string
    mandatory: boolean
    type: string
}