import authService from './components/api-authorization/AuthorizeService';
export class ApiClientBase {
    protected async transformOptions(options: RequestInit): Promise<RequestInit> {
        const token = await authService.getAccessToken();
        options.headers = { ...options.headers, authorization: `Bearer ${token}` };
        return Promise.resolve(options);
    }
}
