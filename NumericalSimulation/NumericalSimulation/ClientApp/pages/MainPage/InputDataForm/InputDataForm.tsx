import React, {ChangeEvent, useState} from "react";
import Controller from "../../../Controller";
import "./InputDataForm.less"
import {InputDataTypeEnum} from "../../../models/InputDataTypeEnum";

interface InputDataFormProps {
    sessionId: string;
    sendCalculate: (algorithmType: number) => void;
}

const algorithmTypesList = [
    {label: "Простой метод построения расписания", value: 0},
    {label: "Метод Петрова-Соколицына", value: 1}
] 

export default function InputDataForm(props: InputDataFormProps) {
    const [machineToolsIsLoading, setIsLoadingMachineTools] = useState(false);
    const [executeTimeIsLoading, setIsLoadingExecuteTimes] = useState(false);
    const [nomenclatureIsLoading, setIsLoadingNomenclature] = useState(false);
    const [algorithmType, setAlgorithmType] = useState(0);
    
    const [partyIsLoading, setIsLoadingParties] = useState(false);
    
    function uploadFile(event: ChangeEvent<HTMLInputElement>, type: InputDataTypeEnum, updateState: (value) => void) {
        /*validate input files*/
        var file = event.target.files[0];
        const formData = new FormData();
        formData.append('fileData', file);
        formData.append('type', type.valueOf().toString());
        formData.append('sessionId', props.sessionId);
        
        Controller.PostInputUserFile(formData).then(
            (data: number) => {
                switch (data) {
                    case 0:
                    {
                        updateState(true);
                        break;
                    }
                    case -1:
                    {
                        alert("Проблема с загрузкой файла. Неверный формат или файл является пустым.")
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
        <div className={"input_data_form__item"}>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными о номенклатуре</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.Nomenclature, setIsLoadingNomenclature)}/>
        </div>
        <div className={"input_data_form__item"}>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными об оборудовании</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.MachineTool, setIsLoadingMachineTools)}/>
        </div>
        <div className={"input_data_form__item"}>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными о времени выполении</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.ExecuteTime, setIsLoadingExecuteTimes)}/>
        </div>
        <div className={"input_data_form__item"}>
            <label className={"input_data_form__title"}>Выберите файл xls-файл с данными о партиях</label>
            <input type={"file"} onChange={(e) => uploadFile(e, InputDataTypeEnum.Party, setIsLoadingParties)}/>
        </div>
        <div className={"input_data_form__item"}>
            <select value={algorithmType} onChange={(e) => setAlgorithmType(parseInt(e.target.value))}>
                {algorithmTypesList.map(x => {
                    return <option value={x.value}>{x.label}</option>
                })}
            </select>
        </div>
        <div className={"input_data_form__button_submit"}>
            <button disabled={!canSend()} onClick={() => props.sendCalculate(algorithmType)}>Составить расписание</button>
        </div>
        
    </div>
}