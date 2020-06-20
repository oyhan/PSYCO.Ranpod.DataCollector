import React from "react"
import { PropType } from "./Types";
import { BaseModel } from "./BaseModel";
import OpenInNewIcon from '@material-ui/icons/OpenInNew';
import { UserService } from "services/UserService";
import { formExtractor } from "infrastructure/Helper/formExtracor";
import { MainService } from "services/MainService";
import { Urls } from "infrastructure/Helper/urls";



export class LoginModel {

    static get Title() {
        return "ورود به سیستم";
    }

    static get Icon() {
        return <OpenInNewIcon />;
    }

    static get properties() {

        return new Promise((resolve)=>resolve([
            
            { Name: "Email", Type: PropType.Text, DisplayName: "ایمیل" ,Required :true },
            { Name: "Password", Type: PropType.Password, DisplayName: "کلمه عبور" ,Required :true },

        ])) 
    }

    // static list(){
    //     return LoginService.GetAll().then((response)=>{
    //         console.log("reponse from get all");
    //         console.log(response);
    //         return response;
    //     });
    // }


    static handleSubmit(model) {
      

    
       return  MainService.New("/api/account/login", JSON.stringify(model)).then(result=>{
            console.log("login respons :" ,result)
            const user = Object.assign({},result,{isAuthenticated : true});
            UserService.singin(user);  

            return user;
        }
            );
    //     if (postdata.userName === "ali" && postdata.passWord === '123') {
    //         console.log("ok");
    //          UserService.singin({Name:"admin",isAuthenticated :true});
    //          return Promise.resolve({ redirect: undefined, message: "عملیات  با موفقیت انجام شد" });
    //     // return LoginService.New(postdata)
    //     //     .then((response) => {
    //     //         console.log("response");
    //     //         console.log(response);

    //     //         // UserService.singin(response);
    //     //         return Promise.resolve({ redirect: "/", message: "عملیات  با موفقیت انجام شد" });
    //     //     }, () => Promise.reject("ok"))


    //     return Promise.resolve({ redirect: "/border", message: "عملیات  با موفقیت انجام شد" });

    //     return Promise.resolve({ message: 'admin loged in !', redirect: "/" })

    // }
    

    return Promise.resolve({ redirect: "/border", message: "عملیات  با موفقیت انجام نشد" });
       
        // return Promise.reject("error")
        
}}
