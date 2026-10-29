import { Api } from "@/api/Api.ts";

class ApiClient {
    public api;

    constructor() {
        const currentHost = window.location.hostname;

        const apiBaseUrl =
            currentHost === "localhost" || currentHost === "127.0.0.1"
                ? "http://localhost:5012"
                : `http://${currentHost}:8000`;

        this.api = new Api<string>({
            baseUrl: apiBaseUrl,

            securityWorker: (token) => {
                if (!token) {
                    return {};
                }

                return {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                };
            },
        });

        this.api.setSecurityData(localStorage.getItem("token"));
    }
}

export default new ApiClient();