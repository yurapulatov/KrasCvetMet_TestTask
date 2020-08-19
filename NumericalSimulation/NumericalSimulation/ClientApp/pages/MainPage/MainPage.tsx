import React, {useState} from "react";
import "./MainPage.less"
import TableResultForm from "./TableResultForm/TableResultForm";
import InputDataForm from "./InputDataForm/InputDataForm";
import Helper from "../../Helper";
import Controller from "../../Controller";

export default function MainPage() {

    const [schedule, setSchedule] = useState([]);
    const [sessionId, setSessionId] = useState(Helper.generateUid());
    
    function sendCalculate() {
        Controller.
    }
    
    return <div className={"main_page"}>
        <InputDataForm sessionId={sessionId} sendCalculate={() => sendCalculate()}/>
        <TableResultForm schedule={schedule}/>
    </div>
}