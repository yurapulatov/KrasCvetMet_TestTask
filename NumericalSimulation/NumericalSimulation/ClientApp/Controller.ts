import {InputDataTypeEnum} from "./models/InputDataTypeEnum";

export default class Controller {
    
    public static getHost() {
        return location.origin;
    }
    

    public static async PostInputUserFile(file: File, type: InputDataTypeEnum, sessionId: string) {
        return this.sendQuery(`${this.getHost()}/rates?sessionId=${sessionId}&type=${type}`, "POST", file);
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
}
