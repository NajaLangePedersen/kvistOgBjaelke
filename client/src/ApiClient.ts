import { Api } from "@/api/Api";

const apiClient = new Api({
    securityWorker: (token) => {
        if (token) {
            return {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            };
        }
    },
});

export default apiClient;