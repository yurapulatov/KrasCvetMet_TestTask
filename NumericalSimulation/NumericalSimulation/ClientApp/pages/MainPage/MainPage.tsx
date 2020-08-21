import React, {useState} from "react";
import "./MainPage.less"
import TableResultForm from "./TableResultForm/TableResultForm";
import InputDataForm from "./InputDataForm/InputDataForm";
import Helper from "../../Helper";
import Controller from "../../Controller";
import Schedule from "../../models/Schedule";

export default function MainPage() {

    const [schedule, setSchedule] = useState<Schedule[]>([]);
    const [sessionId, setSessionId] = useState(Helper.generateUid());
    
    function sendCalculate(algorithmType: number) {
        Controller.getSchedule(sessionId, algorithmType).then(
            (data: Schedule[]) => { setSchedule(data) }
        )
    }
    
    return <div className={"main_page"}>
        <InputDataForm sessionId={sessionId} sendCalculate={(algorithmType) => sendCalculate(algorithmType)}/>
        <TableResultForm schedule={schedule}/>
    </div>
}