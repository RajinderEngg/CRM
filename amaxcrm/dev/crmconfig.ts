let crmConfig = {
    version: "1.0.0.22",
    minSvcVersion: "",
    minDbsVersion: "",
    falbackLanguage: "he"
};
let serviceConfig = {
    serviceBaseUrl: "http://localhost/amaxweb/Api.svc/",
    ImageUrl: "http://localhost:57998/AmutotFiles/",
    serviceApiUrl: "http://localhost:57998/API/",
    AppUrl: "http://localhost:3000/#/",
    accesTokenStoreName:"XToken",
    accesTokenRequestHeader:"X-Token",
    accesTokenResponceHeader:"XToken",
    

    authenticationMode:"JWT-Token"
}
export var LocalDict = {
    userCredential: "userCredential",
    selectedLanguage: "preferedLanguage",
    languageResource: "languageResource",
    SmsSettings: "smsSettings"
}

export var SessionlDict = {

}
if (window.location.href.indexOf("127.0.0.1") > -1)
    serviceConfig.serviceBaseUrl ="http://localhost:57998/Api.svc/";
export {
serviceConfig,
crmConfig
}