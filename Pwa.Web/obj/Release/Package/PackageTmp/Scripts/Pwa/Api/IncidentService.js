

var IncidentService = {

    search: (criteria,success,error) => {
        

        $.ajax({
            url: ServicesEnv.getServicePath('api/Incident/GetincidentCases'),
            type: 'GET',
            dataType: 'json',
            async: true,
            success: function (data, textStatus, xhr) {
                success(data, textStatus, xhr);
            },
            error: function (xhr, textStatus, errorThrown) {
                error(xhr, textStatus, errorThrown);
            }
        });

    },
    add: (incident, success, error) => {

        $.ajax({
            url: ServicesEnv.getServicePath('api/Incident/AddcidentCase'),
            type: 'POST',
            dataType: 'json',
            data: incident,
            async: true,
            success: function (data, textStatus, xhr) {
                success(data, textStatus, xhr);
            },
            error: function (xhr, textStatus, errorThrown) {
                error(xhr, textStatus, errorThrown);
            }
        });

       
    }
}