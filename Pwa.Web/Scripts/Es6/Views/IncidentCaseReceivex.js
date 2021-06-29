import { IncidentCaseReceiveService } from '../Services/IncidentCaseReceiveService';

let _incService = new IncidentCaseReceiveService();
let incidentForm = null;
let incidentModal = null;
var _map = null;
var _showLayers = null;
let IncidentCaseReceive = {
    init: () => {

        try {



            $.each(_incidentTypes, function (index, t) {
                $('#ddlIncidentType').append($('<option>').text(t.Name).attr('value', t.Id));
            });
            /*
            $('#ddlIncidentType').change(function () {
                var id = $('#ddlIncidentType').val();
                $.each(_incidentTypes, function (index, t) {
                    if (t.Id == parseInt(id)) {
                       
                        //$('#ddlIncidentCategory').html('');
                        $.each(t.Categories, function (index, c) {
                            
                            $('#ddlIncidentCategory').append($('<option>').text(c.name).attr('value', c.id));
                        });
                        $('#ddlIncidentCategory').selectpicker('refresh');
                    }
         
                });
            });*/

            _map = L.map('mapid').setView([13, 100], 6);
            L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
            }).addTo(_map);

            _showLayers = new L.FeatureGroup();
            _map.addLayer(_showLayers);

            $('#ddlInformChannel').change(() => {
                //alert('xxxx');
            });


        } catch (e) {
            alert(e.message);
        }


    },
    test: () => {
        alert('xxxxx');
    },
    displayIncidents: () => {
        //

        $.ajax({

            url: 'http://localhost:26189/api/Incident/GetIncidents',
            //data: incidentModal.Incident,
            type: "GET",
            dataType: "json",
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (data) {

                let htmlTr = '';
                if (data.Success) {
                    _showLayers.clearLayers();
                    $.each(data.Incidents, function (index, t) {
                        htmlTr += '<tr>';
                        htmlTr += '<td onclick="javascript:showMapBookmark();">';
                        htmlTr += '<table class="table table-borderless">';
                        htmlTr += '   <tbody>';
                        htmlTr += '        <tr><td><b>รายการ :</b>' + t.RequestType + '-' + t.RequestCategory + '</td></tr>';
                        htmlTr += '        <tr><td><b>เรื่อง :</b>' + t.RequestCategorySubject + '</td></tr>';
                        htmlTr += '        <tr><td><b>สถานะ :</b> รอดำเนินการ </td></tr>';
                        htmlTr += '   </tbody>';
                        htmlTr += ' </table>';
                        htmlTr += '</td>';
                        htmlTr += '</tr>';

                        showMapBookmark();
                    });


                    var html = '<table class="table table-hover">';
                    html += '<thead>';
                    html += '        <tr>';
                    html += '            <th scope="col">รายการค้นหา</th>';
                    html += '        </tr>';
                    html += '    </thead>';
                    html += '    <tbody>';
                    html += htmlTr;
                    html += '    </tbody>';
                    html += '</table>';

                    $('#dvIncidentsList').html(html);
                }
            }
        });
    }
}

$(document).ready(() => {
    console.log("document ready!");

    try {
        IncidentCaseReceive.init();
        IncidentCaseReceive.displayIncidents();
    } catch (e) {
        alert('error :' + e.message);
    }


});


function LoadMap() {

    try {
        var map = L.map('mapid').setView([13, 100], 6);
        L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(map);


    } catch (e) {
        alert(e.message);
    }

}

function random(min, max) {

    var randomnum = (Math.random() * (max - min) + min).toFixed(4)
    console.log(randomnum)
    return randomnum;
}

function showMapBookmark() {
    //_showLayers.clearLayers();
    _showLayers.addLayer(L.marker([random(14.00, 14.400), random(100.00, 100.600)]));
}