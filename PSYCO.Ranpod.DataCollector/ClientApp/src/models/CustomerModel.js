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
console.log('host: ', host);
export class CustomerModel extends BaseModel {

    constructor(){
        this.NewUrl = this.NewUrl.bind(this)
    }

    static  GetClients(customerId){
        console.log('customerId: ', this);

        var url = host+Urls.Customer.Clients+customerId;
        console.log('url: ', url);

        return MainService.Get(url)
    }
    static get Title() {
        return ModelResources.Customer.Title
    }

    static get Icon() {
        return <OpenInNewIcon />;
    }

    static get ListUrl(){
        return host + Urls.Customer.GetAll
    } 
    static get DeleteUrl(){
        return host + Urls.Customer.Delete
    } 
    
    static get EditUrl(){
        return host + Urls.Customer.Edit
    } 
    static get NewUrl(){
        return host + Urls.Customer.New
    }
    static get BatchUrl(){
        return host + Urls.Customer.Batch
    } 
    


    static list(){
        return MainService.GetAll(host + Urls.Customer.GetModel).then((response)=>{
            return response;
        });
    }
    static  get  properties() {

        
      
       return MainService.GetModel(host + Urls.Customer.GetModel);
      
        return [
            
            { Name: "name", Type: PropType.Text, DisplayName: "نام مرز" ,Required :true , value :"مقدار"},
            ...super.properties,

        ];
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
        
        // console.log('this: ', this);
        //if (postdata["Username"] === "ali" && postdata["Password"] === '123') {
            //
             //UserService.singin({Name:"admin",isAuthenticated :true});
        return MainService.New(CustomerModel.NewUrl, postdata)
            .then((response) => {
                
                

                // UserService.singin(response);
                return Promise.resolve({ redirect: "/Customer/list", message: "عملیات  با موفقیت انجام شد" });
            }, (error) => Promise.reject(error))



        return Promise.resolve({ message: 'admin Customered in !', redirect: "/" })

    }
    

       
        // return Promise.reject("error")
        
}
