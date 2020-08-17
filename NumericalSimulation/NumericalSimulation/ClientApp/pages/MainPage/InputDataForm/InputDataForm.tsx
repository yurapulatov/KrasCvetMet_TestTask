import React, {useState} from "react";
import Controller from "../../../Controller";
import "./FilterForm.less"
import {InputDataTypeEnum} from "../../../models/InputDataTypeEnum";

interface InputDataFormProps {
    sessionId: string;
}

export default function InputDataForm(props: InputDataFormProps) {
    const [machineToolsIsLoading, setIsLoadingMachineTools] = useState(false);
    const [executeTimeIsLoading, setIsLoadingExecuteTimes] = useState(false);
    const [nomenclatureIsLoading, setIsLoadingNomenclature] = useState(false);
    const [partyIsLoading, setIsLoadingParties] = useState(false);
    
    function uploadFile(file: File, type: InputDataTypeEnum) {
        Controller.PostInputUserFile(file, type, props.sessionId).then(
            () => {}
        )
    }
    
    return <div>
        <div>
            <label>Выберите файл xls-файл с данными о номенклатуре</label>
            <input type={"file"} onChange={(e) => uploadFile(e.target.files[0], InputDataTypeEnum.Nomenclature)}/>
        </div>
        <div>
            <label>Выберите файл xls-файл с данными об оборудовании</label>
            <input type={"file"} onChange={(e) => uploadFile(e.target.files[0], InputDataTypeEnum.MachineTool)}/>
        </div>
        <div>
            <label>Выберите файл xls-файл с данными о времени выполении</label>
            <input type={"file"} onChange={(e) => uploadFile(e.target.files[0], InputDataTypeEnum.ExecuteTime)}/>
        </div>
        <div>
            <label>Выберите файл xls-файл с данными о партиях</label>
            <input type={"file"} onChange={(e) => uploadFile(e.target.files[0], InputDataTypeEnum.Party)}/>
        </div>
        <div>
            
        </div>
        
    </div>
}