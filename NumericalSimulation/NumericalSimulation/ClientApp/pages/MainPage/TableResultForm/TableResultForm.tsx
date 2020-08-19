import React, {useState} from "react";
import moment from "moment";
import "./TableResultForm.less"
import Schedule from "../../../models/Schedule";
import MachineTool from "../../../models/MachineTool";

export interface TableResultFormProps {
    schedule: Schedule[];
}

export default function TableResultForm (props: TableResultFormProps) {
    const [headerItems, setHeaderItems] = useState<MachineTool[]>([]);

    function renderHeaderTable() {
        var machineIdList = props.schedule.map(x => x.machineToolId);
        var uniqueMachineIdList = Array.from(new Set(machineIdList));
        var machineTools : MachineTool[] = [];
        uniqueMachineIdList.forEach(x => {
            machineTools.push(props.schedule.find(schedule => schedule.machineToolId == x).machineTool);
        })
        setHeaderItems(machineTools);
        return <thead>
        <tr>
            <td className={"table_result_form__cell__value-header"}>Партии</td>
            {machineTools.map(x => {
                return <td className={"table_result_form__cell__value-header"}>{x.name}</td>
            })}
        </tr>
        </thead>
    }
    
    function renderTableValue() {
        var partiesIdList = props.schedule.map(x => x.partyId);
        var uniquePartiesIdList = Array.from(new Set(partiesIdList));
        return <tbody>
        {uniquePartiesIdList.map(row => {
            var party = props.schedule.find(x => x.partyId == row).party;
            return <tr>
                <td className={"table_result_form__cell__value-header"}>{`Партия №${party.id}(${party.nomenclature.name})`}</td>
                {headerItems.map(header => {
                    var schedule = props.schedule.find(x => x.partyId == row && x.machineToolId == header.id);
                    var item = schedule == null ? "-" : `${moment(schedule.dateTimeFrom).format("hh:mm")} - ${moment(schedule.dateTimeTo).format("hh:mm")}`
                    return <td className={"table_result_form__cell__value"}>{item}</td>
                })}
            </tr>
        })}
        </tbody>
    }
    
    if (props.schedule.length > 0) {
        return <div className={"table_result_form"}>
            <table>
                {renderHeaderTable()}
                {renderTableValue()}
            </table>
        </div>
    }
    else {
        return <div className={"table_result_form table_result_form__nothing"}>
                Nothing to show. Fill your filter and search.
        </div>
    }

}