import React from "react";
import moment from "moment";
import "./TableResultForm.less"
import Schedule from "../../../models/Schedule";

export interface TableResultFormProps {
    schedule: Schedule[];
}

export default function TableResultForm (props: TableResultFormProps) {


    function renderHeaderTable() {
        return <thead></thead>
    }
    
    function renderTotalValue() {
        return <tfoot></tfoot>
    }
    
    
    function renderTableValue() {
        return <tbody></tbody>
    }
    if (props.schedule.length > 0) {
        return <div className={"table_result_form"}>
            <table>
                {renderHeaderTable()}
                {renderTableValue()}
                {renderTotalValue()}
            </table>
        </div>
    }
    else {
        return <div className={"table_result_form table_result_form__nothing"}>
                Nothing to show. Fill your filter and search.
        </div>
    }

}