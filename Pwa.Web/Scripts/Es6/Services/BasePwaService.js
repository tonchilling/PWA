export class BasePwaService {
    constructor() {
        this.serviceDomainName = 'http://119.59.115.68/';
    }
    getServiceDomain(){
        return this.serviceDomainName ;
    }
    getServicePath(url){
        return `${this.getServiceDomainName()}/${url}`;
    }

}