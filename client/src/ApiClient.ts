import {Api} from "@/api/Api.ts";

class ApiClient {
    public api: Api<any>;

    constructor() {
        this.api = new Api();
    }
}

export default new ApiClient();