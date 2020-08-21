import {InputDataTypeEnum} from "./models/InputDataTypeEnum";
import Schedule from "./models/Schedule";

export default class Controller {
    
    public static getHost() {
        return location.origin;
    }
    

    public static async PostInputUserFile(formData: FormData) {
        return this.sendFormQuery(`${this.getHost()}/simulation/add_data`, formData);
    }

    public static async getSchedule(sessionId: string, algorithmType: number) {
        return this.sendQuery(`${this.getHost()}/simulation/schedule?sessionId=${sessionId}&algorithmType=${algorithmType}`, "GET");
    }
    
    public static async exportToExcel(schedules: Schedule[]) {
        return this.sendQuery(`${this.getHost()}/simulation/export`, "POST", schedules, true);
    }
    
    private static async sendQuery(url: string, method: string, body?: any, resultIsBlob: boolean = false) : Promise<any> {

        let response = await fetch(url, {
            method: method,
            headers: Object.assign(
                {'Content-Type': 'application/json;charset=utf-8'}
            ),
            body: JSON.stringify(body)
        });
        if (resultIsBlob) {
            return response.blob();
        }
        else {
            return response.json();
        }

    }
    
    private static async sendFormQuery(url: string, formData: any) : Promise<any> {
        const response = await fetch(url, {
            method: 'POST',
            body: formData
        });
        return response.json();
    }
    
}
