export interface SwaggerEndpoint {
    path: string;
    methods: SwaggerMethod[];
}

export interface SwaggerMethod {
    name: string;
    summary: string;
    description: string;
    parameters: SwaggerParams[];
    responses: SwaggerResponse[];
}

export interface SwaggerParams {
    name: string;
    required?: boolean;
    description?: string;
    in: string;
    style: string;
    schema: SwaggerSchema;
    value?: any
}

export interface SwaggerSchema {
    type: string;
    format: string | null;
}

export interface SwaggerResponse {
    code: number;
    description: string;
}