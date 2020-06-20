import React from "react"
import { PropType } from "./Types";
import { BaseModel } from "./BaseModel";
import OpenInNewIcon from '@material-ui/icons/OpenInNew';
import { Urls } from "infrastructure/Helper/urls";
import { MainService } from "services/MainService";
import ModelResources from "infrastructure/Resources/ModelResources";
import { formExtractor } from "infrastructure/Helper/formExtracor";
import AppConfiguration from "app.config";

var host = AppConfiguration.Server.Socket();

export class SegmentModel extends BaseModel {

    constructor() {
        this.NewUrl = this.NewUrl.bind(this)
    }

    static GetClients(customerId) {


        var url = host + Urls.Segment.Clients + customerId;


        return MainService.Get(url)
    }
    static get Title() {
        return ModelResources.Segment.Title
    }

    static get Icon() {
        return <OpenInNewIcon />;
    }

    static get ListUrl() {
        return host + Urls.Segment.GetAll
    }
    static get DeleteUrl() {
        return host + Urls.Segment.Delete
    }

    static get EditUrl() {
        return host + Urls.Segment.Edit
    }
    static get NewUrl() {
        return host + Urls.Segment.New
    }
    static get BatchUrl() {
        return host + Urls.Segment.Batch
    }



    static list() {
        return MainService.GetAll(host + Urls.Segment.GetModel).then((response) => {
            return response;
        });
    }
    static get properties() {



        return MainService.GetModel(host + Urls.Segment.GetModel);
        

        return [

            { Name: "name", Type: PropType.Text, DisplayName: "نام مرز", Required: true, value: "مقدار" },
            ...super.properties,

        ];
    }

    static list() {
        return MainService.GetAll(this.ListUrl).then((response) => {


            return response;
        });
    }

    static get NewProperties() {

        return MainService.GetModel(host + Urls.Segment.GetModel).
        then(props=> props.filter(f=>f.Name !== 'Id'));
    }
    static get EditProperties() {

        return MainService.GetModel(host + Urls.Segment.GetEditModel);
    }
    static handleSubmit(event) {
        event.preventDefault();
        // let { from } = this.props.location.state || { from: { pathname: "/" } };

        var postdata = formExtractor(event.target);
        // postdata = JSON.parse(postdata);

        // 
        //if (postdata["Username"] === "ali" && postdata["Password"] === '123') {
        //
        //UserService.singin({Name:"admin",isAuthenticated :true});
        return MainService.New(SegmentModel.NewUrl, postdata)
            .then(() => {



                // UserService.singin(response);
                return Promise.resolve({ redirect: "/Segment/list", message: "عملیات  با موفقیت انجام شد" });
            }, (error) => Promise.reject(error))



        return Promise.resolve({ message: 'admin Segmented in !', redirect: "/" })

    }



    // return Promise.reject("error")

}
