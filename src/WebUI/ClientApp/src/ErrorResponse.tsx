export interface IErrorResponse {
    type?: string;
    title?: string;
    status?: number;
    detail?: string | undefined;
    errors?: {} | undefined;
}