import { Api } from "./myApi";

class ApiClient {
    public api: Api;

    constructor() {
        this.api = new Api();
    }
}

export default new ApiClient();