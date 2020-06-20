
import React from "react"
import { ConsoleLogModel } from "models/ConsoleLogModel"
import CustomGrid from "components/DataGrid/Grid"
import Container from "components/Content/Container"
import { DetailRow } from '@syncfusion/ej2-react-grids';
import ReactJson from 'react-json-view'




const ConsoleLogs = (props) => {

    const logDetails = (props) => {
        var json = JSON.parse(props.data);
        return <ReactJson src={json} />
    }

    return (
        // <Container title={ConsoleLogModel.Title} description={"All console logs you see here"} >
            <CustomGrid
                gridConfig={{ detailTemplate: logDetails }}
                gridService={[DetailRow]}
                edit={false}
                delete={false} model={ConsoleLogModel} />
        // </Container>

    )
}

export default ConsoleLogs;