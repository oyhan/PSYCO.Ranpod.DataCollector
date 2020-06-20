
import React, { useState, useEffect } from "react"
import { LogModel } from "models/LogModel"
import CustomGrid from "components/DataGrid/Grid"
import Container from "components/Content/Container"
import { RegisterModel } from "models/RegisterModel"
import { InputRenderer } from "infrastructure/FormMaker/InputRenderer"
import GridItem from "components/Grid/GridItem"
import GridContainer from "components/Grid/GridContainer"
import Button from "components/CustomButtons/Button.js";
import WithData from "components/WithDataC/WithData"
import VerificationSent from "./VerificationSent"
import AppLoader from "components/AppLoader/AppLoader"
import { ShowSnack } from "infrastructure/Helper/Showsnack"







const RegisterPage = (props) => {



    var [cols, setCols] = useState([]);

    const { data, onSubmit } = props;
    const [emialSent, setEmailSent] = React.useState(false);
    const  [loading,setLoading] = useState(false);
    
    const submit = async (e) => {

        e.preventDefault();
        setLoading(true);

        RegisterModel.handleSubmit(e)
            .then((respons) => {
                setLoading(false);


                setEmailSent(true);
            } , error =>{
                
                setLoading(false);
                
                ShowSnack.Error(error)
            })
    }
    const footer = ()=>{

        return(
            loading ? <AppLoader /> :

            <Button type="submit" color="primary">ثبت نام</Button>
        )
    }


    useEffect(() => {

        RegisterModel.properties.then(items => setCols(items))
    }
        , [])



    return (
        emialSent ? <VerificationSent /> :
            <form onSubmit={submit} >
                {/* <Container title={RegisterModel.Title} description={"Add a new customer"} footer={footer} > */}
                {/* <Container title={RegisterModel.Title} description={""} > */}
                <GridContainer>


                    {

                        cols.map((item, key) => {

                            return (
                                <GridItem xs={12} sm={12} md={12}>

                                    <InputRenderer key={key} {...item} />
                                </GridItem>
                            )

                        })
                    }
                </GridContainer>
                {footer()}
                {/* </Container> */}





                {/* </Container> */}
            </form>

    )
}
export default RegisterPage;


