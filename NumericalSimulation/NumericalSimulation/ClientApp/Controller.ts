import {InputDataTypeEnum} from "./models/InputDataTypeEnum";

export default class Controller {
    
    public static getHost() {
        return location.origin;
    }
    

    public static async PostInputUserFile(formData: FormData) {
        return this.sendFormQuery(`${this.getHost()}/simulation/add_data`, formData);
    }

    public static async getSchedule( sessionId: string) {
        return this.sendQuery(`${this.getHost()}/simulation/schedule?sessionId=${sessionId}`, "GET");
    }
    
    private static async sendQuery(url: string, method: string, body?: any) : Promise<any> {

        let response = await fetch(url, {
            method: method,
            headers: Object.assign(
                {'Content-Type': 'application/json;charset=utf-8'}
            ),
            body: JSON.stringify(body)
        });
        return response.json();
    }
    
    private static async sendFormQuery(url: string, formData: any) : Promise<any> {
        const response = await fetch(url, {
            method: 'POST',
            body: formData
        });
        return response.json();
    }
}
