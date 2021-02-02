export interface IErrorResponse {
    type?: string;
    title?: string;
    status?: number;
    errors?: {} | undefined;
}