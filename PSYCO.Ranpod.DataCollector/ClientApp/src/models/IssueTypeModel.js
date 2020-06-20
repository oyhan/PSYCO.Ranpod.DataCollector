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

export class IssueTypeModel extends BaseModel {

    constructor() {
        this.NewUrl = this.NewUrl.bind(this)
    }

    static GetClients(customerId) {


        var url = host + Urls.IssueType.Clients + customerId;


        return MainService.Get(url)
    }
    static get Title() {
        return ModelResources.IssueType.Title
    }

    static get Icon() {
        return <OpenInNewIcon />;
    }

    static get ListUrl() {
        return host + Urls.IssueType.GetAll
    }
    static get DeleteUrl() {
        return host + Urls.IssueType.Delete
    }

    static get EditUrl() {
        return host + Urls.IssueType.Edit
    }
    static get NewUrl() {
        return host + Urls.IssueType.New
    }
    static get BatchUrl() {
        return host + Urls.IssueType.Batch
    }



    static list() {
        return MainService.GetAll(host + Urls.IssueType.GetModel).then((response) => {
            return response;
        });
    }
    static get properties() {

        console.log("getiing props...");

        return MainService.GetModel(host + Urls.IssueType.GetModel).then(props => {
            props.map(p => {
                if (p.Name == 'SegmentId') p.IsForeignKey = true;

            })

            return props
        }

        );

        return [

            { Name: "name", Type: PropType.Text, DisplayName: "نام مرز", Required: true, value: "مقدار" },
            ...super.properties,

        ];
    }

    static NewProperties() {
        return MainService.GetModel(host + Urls.IssueType.GetModel).then(props => {
            props.map(p => {
                if (p.Name == 'SegmentId') p.IsForeignKey = true;

            })

            return props
        })
    }

    static list() {
        return MainService.GetAll(this.ListUrl).then((response) => {


            return response;
        });
    }


    static handleSubmit(model) {


        // let { from } = this.props.location.state || { from: { pathname: "/" } };

        // var postdata = formExtractor(event.target);
        // postdata = JSON.parse(postdata);

        // 
        //if (postdata["Username"] === "ali" && postdata["Password"] === '123') {
        //
        //UserService.singin({Name:"admin",isAuthenticated :true});
        return MainService.New(IssueTypeModel.NewUrl, model)
            .then((response) => {



                // UserService.singin(response);
                return Promise.resolve({ redirect: "/IssueType/list", message: "عملیات  با موفقیت انجام شد" });
            }, (error) => Promise.reject(error))



        return Promise.resolve({ message: 'admin IssueTypeed in !', redirect: "/" })

    }
    static handleEdit(model) {


        // let { from } = this.props.location.state || { from: { pathname: "/" } };

        // var postdata = formExtractor(event.target);
        // postdata = JSON.parse(postdata);

        // 
        //if (postdata["Username"] === "ali" && postdata["Password"] === '123') {
        //
        //UserService.singin({Name:"admin",isAuthenticated :true});
        return MainService.New(IssueTypeModel.EditUrl, model)
            .then((response) => {



                // UserService.singin(response);
                return Promise.resolve({ redirect: "/IssueType/list", message: "عملیات  با موفقیت انجام شد" });
            }, (error) => Promise.reject(error))



        return Promise.resolve({ message: 'admin IssueTypeed in !', redirect: "/" })

    }



    // return Promise.reject("error")

}
