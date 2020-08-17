import Party from "./Party";
import MachineTool from "./MachineTool";

export default class Schedule {
    public dateTimeFrom: string;
    public dateTimeTo: string;
    public partyId: number;
    public party: Party;
    public machineTool: MachineTool;
}