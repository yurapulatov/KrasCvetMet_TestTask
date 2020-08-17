import React, {useState} from "react";
import "./MainPage.less"
import TableResultForm from "./TableResultForm/TableResultForm";
import InputDataForm from "./InputDataForm/InputDataForm";

export default function MainPage() {

    const [schedule, setSchedule] = useState([]);
    
    return <div className={"main_page"}>
        <InputDataForm sessionId={""}/>
        <TableResultForm schedule={schedule}/>
    </div>
}