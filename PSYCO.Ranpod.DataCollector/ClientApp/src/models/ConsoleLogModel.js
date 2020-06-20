import React from "react"
import { PropType } from "./Types";
import { BaseModel } from "./BaseModel";
import OpenInNewIcon from '@material-ui/icons/OpenInNew';
import { resolve } from "url";
import { Urls } from "infrastructure/Helper/urls";
import { MainService } from "services/MainService";
import { URL } from "infrastructure/Helper/UrlHelper";
import ModelResources from "infrastructure/Resources/ModelResources";
import { formExtractor } from "infrastructure/Helper/formExtracor";
import AppConfiguration from "app.config";

var host = AppConfiguration.Server.Socket();
export class ConsoleLogModel extends BaseModel {

    static get Title() {
        return ModelResources.ConsoleLog.Title
    }

    static get Icon() {
        return <OpenInNewIcon />;
    }

    static get ListUrl(){
        return host + Urls.ConsoleLog.GetAll
    } 
    static get DeleteUrl(){
        return host + Urls.ConsoleLog.Delete
    } 
    
    static get EditUrl(){
        return host + Urls.ConsoleLog.Edit
    } 
    static get BatchUrl(){
        return host + Urls.ConsoleLog.Batch
    } 
    
    static list(){
        return MainService.GetAll(host + Urls.ConsoleLog.GetModel).then((response)=>{
            return response;
        });
    }
    static  get  properties() {

        
      
    //    return MainService.GetModel(host + Urls.ConsoleLog.GetModel);
      
        return new Promise(resolve=>resolve([
            
            { Name: "Data", Type: PropType.Text, DisplayName: "Data" ,Required :true , hidden : true , value :"مقدار"},
            { Name: "IsPMDLProRunning", Type: PropType.Text, DisplayName: "IsPMDLProRunning" ,Required :true ,},
            { Name: "GetPMDLProStatusString0", Type: PropType.Text, DisplayName: "GetPMDLProStatusString0" ,Required :true ,},
            { Name: "GetPMDLProStatusString1", Type: PropType.Text, DisplayName: "GetPMDLProStatusString1" ,Required :true ,},
            { Name: "GetPMDLProStatusString2", Type: PropType.Text, DisplayName: "GetPMDLProStatusString2" ,Required :true ,},
            { Name: "RegistryAppID", Type: PropType.Text, DisplayName: "RegistryAppID" ,Required :true ,},
            { Name: "RegistryServerID", Type: PropType.Text, DisplayName: "RegistryServerID" ,Required :true ,},
            { Name: "RegistryOrgID", Type: PropType.Text, DisplayName: "RegistryOrgID" ,Required :true ,},
            { Name: "RegistryDefaultProtectionMode", Type: PropType.Text, DisplayName: "RegistryDefaultProtectionMode" ,Required :true ,},
            { Name: "RegistryLicenseData", Type: PropType.Text, DisplayName: "RegistryLicenseData" ,Required :true ,},
            { Name: "RegistryExpireDate", Type: PropType.Text, DisplayName: "RegistryExpireDate" ,Required :true ,},
            { Name: "RegistryPassword", Type: PropType.Text, DisplayName: "RegistryPassword" ,Required :true ,},
            { Name: "RegistryRestrictedPath", Type: PropType.Text, DisplayName: "RegistryRestrictedPath" ,Required :true ,},
            { Name: "RegistryTrustedPath", Type: PropType.Text, DisplayName: "RegistryTrustedPath" ,Required :true ,},
            ...super.properties,

        ]));
    }

    static list(){
        return MainService.GetAll(this.ListUrl).then((response)=>{
            
            
            return response;
        });
    }


    static handleSubmit(event) {
        event.preventDefault();
        // let { from } = this.props.location.state || { from: { pathname: "/" } };

        var postdata = formExtractor(event.target);
        // postdata = JSON.parse(postdata);
        

        //if (postdata["Username"] === "ali" && postdata["Password"] === '123') {
            //
             //UserService.singin({Name:"admin",isAuthenticated :true});
        return MainService.New(postdata)
            .then((response) => {
                
                

                // UserService.singin(response);
                return Promise.resolve({ redirect: "/ConsoleLog/list", message: "عملیات  با موفقیت انجام شد" });
            }, (error) => Promise.reject(error))



        return Promise.resolve({ message: 'admin loged in !', redirect: "/" })

    }
    

       
        // return Promise.reject("error")
        
}
