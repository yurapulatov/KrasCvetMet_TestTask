import React, {ChangeEvent, useState} from "react";
import Controller from "../../../Controller";
import "./FilterForm.less"
import {InputDataTypeEnum} from "../../../models/InputDataTypeEnum";

interface InputDataFormProps {
    sessionId: string;
    sendCalculate: () => void;
}

export default function InputDataForm(props: InputDataFormProps) {
    const [machineToolsIsLoading, setIsLoadingMachineTools] = useState(false);
    const [executeTimeIsLoading, setIsLoadingExecuteTimes] = useState(false);
    const [nomenclatureIsLoading, setIsLoadingNomenclature] = useState(false);
    const [partyIsLoading, setIsLoadingParties] = useState(false);
    
    function uploadFile(event: ChangeEvent<HTMLInputElement>, type: InputDataTypeEnum, updateState: (value) => void) {
        /*validate input files*/
        var file = event.target.files[0];
        Controller.PostInputUserFile(file, type, props.sessionId).then(
            (data: number) => {
                switch (data) {
                    case 0:
                    {
                        updateState(true);
                        break;
                    }
                    case -1:
                    {
                        break;
                    }
                    case -2:
                    {
                        break;
                    }
                }
            }
        )
    }
    
    function canSend() : boolean {
        return partyIsLoading && machineToolsIsLoading && nomenclatureIsLoading && executeTimeIsLoading;
    }
    
    return <div className={"input_data_form"}>
        <div>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными о номенклатуре</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.Nomenclature, setIsLoadingNomenclature)}/>
        </div>
        <div>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными об оборудовании</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.MachineTool, setIsLoadingMachineTools)}/>
        </div>
        <div>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными о времени выполении</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.ExecuteTime, setIsLoadingExecuteTimes)}/>
        </div>
        <div>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными о партиях</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.Party, setIsLoadingParties)}/>
        </div>
        <div className={"input_data_form__button_submit"}>
            <button disabled={!canSend()} onClick={() => props.sendCalculate()}>Составить расписание</button>
        </div>
        
    </div>
}