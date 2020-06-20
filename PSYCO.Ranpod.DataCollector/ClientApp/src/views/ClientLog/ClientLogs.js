
import React from "react"
import { LogModel } from "models/LogModel"
import CustomGrid from "components/DataGrid/Grid"
import Container from "components/Content/Container"
import { DetailRow } from '@syncfusion/ej2-react-grids';
import ReactJson from 'react-json-view'



const ClientLogs = (props) => {

    const logDetails = (props)=> {
        console.log('props.Data: ', props.Data);
        var json = JSON.parse(props.Data);
        
         return<ReactJson src={json} />
    }  

    return (
        <Container title={LogModel.Title} description={"All clients logs you see here"} >
            <CustomGrid
                gridConfig={{ detailTemplate: logDetails }}
                model={LogModel}
                gridService={[DetailRow]} 
                edit={false}
            />
        </Container>

    )
}

export default ClientLogs;