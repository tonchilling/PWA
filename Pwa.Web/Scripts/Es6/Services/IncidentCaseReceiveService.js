
import { BasePwaService } from './BasePwaService';

export class IncidentCaseReceiveService extends BasePwaService{
    constructor() {
        super();
    }
    getName() {
        return super.getServiceDomain();
    }

    addIncident() {
        var url = super.getServicePath('/api/Complaint/hello2');
      //  alert(url);
        
        $.ajax({

            url: url,
            //data: { name: productName },
            type: "GET",
            dataType: "json",
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (data) {
             //   alert('xxxxxxxxxxxxxxxxxxxxxx');
            }
        });


    }
}