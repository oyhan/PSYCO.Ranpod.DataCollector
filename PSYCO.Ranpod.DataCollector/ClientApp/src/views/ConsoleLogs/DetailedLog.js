
import React from "react"
import CustomGrid from "components/DataGrid/Grid"
import Container from "components/Content/Container"
import { DetailRow } from '@syncfusion/ej2-react-grids';
import ReactJson from 'react-json-view'
import { DetailedLogModel } from "models/DetailedLogModel";




const DetailedLog = (props) => {

    const logDetails = (props) => {
        var json = JSON.parse(props.data);
        return <ReactJson src={json} />
    }

    return (
        // <Container title={DetailedLogModel.Title} description={"All console logs you see here"} >
            <CustomGrid
                gridConfig={{ detailTemplate: logDetails }}
                gridService={[DetailRow]}
                edit={false}
                delete={false} model={DetailedLogModel} />
        // </Container>

    )
}

export default DetailedLog;