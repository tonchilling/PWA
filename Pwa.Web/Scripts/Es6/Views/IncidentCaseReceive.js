import { IncidentCaseReceiveService } from '../Services/IncidentCaseReceiveService';




let incidentForm = null;
var incidentModal = null;
var _map = null;
var _showLayers = null;

var _baseUrl = getBaseUrl();



let IncidentCaseReceive = {
    init: () => {
        
        
        incidentModal = new Vue({
            el: '#frmReceiveIncident',
            data: {
                message: '',
                errors: [],
                view: {
                    incidentTypes: _incidentTypes,
                    incidentCategories: [],
                    incidentSubjects: []
                },
                incident: {
                    PwaIncidentID: 0,
                    PwaIncidentNo: 'Auto',

                    PwaInformReceiver: '',
                    PwaInformDate: IncidentCaseReceive.getCurrentDate(),

                    PwaIncidentTypeID: 7,
                    PwaIncidentGroupID: 1,
                    PwaInformChannel: 1,
                    CaseTitle: '1',
                    CaseTitleDetail: '',
                    CaseDetail: '',
                    ResolvedDetail: '',
                    Sla: true,
                    SlaDetail: '',
                    ReceivedCaseDate: IncidentCaseReceive.getCurrentDateTime(0),
                    CompletedCaseDate: IncidentCaseReceive.getCurrentDateTime(1),
                    CaseLatitude: '',
                    CaseLongtitude: '',
                    PwsIncidentAddress:'',

                    BracnchNo: '',
                    Recorder: '',
                    RecordDate: IncidentCaseReceive.getCurrentDate(),
                    IncidentStatus : 1,

                    InformID:0,
                    CustomerID: '',
                    Title: '1',
                    FirstName: '',
                    LastName: '',
                    MeterNo: '',
                    InformChannelID: 1,
                    InformReference: '',
                    Telephone: '',

                    Province: 1,
                    District: 1,
                    SubDistrict: 1,
                    CustomerAddress : '',

                    ImageFile:''

                }
            },
            methods: {



                incidentTypeChange: function () {
                    try {
                        let incType = this.incident.PwaIncidentTypeID; 

                        let target = _incidentTypes.find(inc => inc.ID == incType);
                        
                        this.view.incidentCategories = target.Categories;
                        setTimeout(function () {
                            $('#ddlIncidentCategory').selectpicker('refresh');
                            
                        }, 1000);
                        
                    } catch (e) {
                        alert(e.message);
                    }
                    
                    /*alert(JSON.stringify(target));*/

                },
                incidentCategoryChange: function () {
                    try {
                        let incType = this.incident.PwaIncidentGroupID;
                        let target = this.view.incidentCategories.find(inc => inc.ID == incType);

                        this.view.incidentSubjects = target.Subjects;
                        setTimeout(function () { $('#ddlIncidentTitle').selectpicker('refresh'); }, 1000);

                    } catch (e) {
                        alert(e.message);
                    }
                },
                doSubmit: function (event) {
                    if (_pageInfo.Mode == 'EDIT') {
                        this.submitEdit(event);
                    } else {
                        this.submit(event);
                    }
                    
                },
                submit: function (event) {
                    try {
                        
                        

                        incidentModal.incident.Informers = [];
                        incidentModal.incident.Informers.push({
                            CustomerID: incidentModal.incident.CustomerID,
                            Title: incidentModal.incident.Title,
                            FirstName: incidentModal.incident.FirstName,
                            LastName: incidentModal.incident.LastName,
                            MeterNo: incidentModal.incident.MeterNo,
                            Telephone: incidentModal.incident.Telephone,
                            InformChannelID: incidentModal.incident.InformChannelID,
                            InformReference: incidentModal.incident.InformReference,
                            

                            InformID: incidentModal.incident.InformID,
                            Province: incidentModal.incident.Province,
                            District: incidentModal.incident.District,
                            SubDistrict: incidentModal.incident.SubDistrict,
                            CustomerAddress: incidentModal.incident.CustomerAddress

                        });

                        var jsonData = JSON.stringify(incidentModal.incident);
                        var data = JSON.parse(jsonData);

                        data.CaseLatitude = $('#ddlCaseLatitude').val();
                        data.CaseLongtitude = $('#ddlCaseLongtitude').val()

                        data.FirstName = $('#ddlCustomer').val();
                        data.BracnchNo = $('#ddlBranch').val();
                        data.CustomerID = GetIdBySelect2($('#ddlCustomer'));

                        data.CaseLatitude = $("#ddlCaseLatitude").val(),
                            data.CaseLongtitude = $("#ddlCaseLongtitude").val(),
                            data.FirstName = $("#ddlCustomer").val(),
                            data.PwaInformReceiver = GetIdBySelect2($("#ddlPwaInformReceiver")),
                            data.SlaDetail = GetIdBySelect2($("#ddlSlaDetail")),
                            data.BracnchNo = GetIdBySelect2($("#ddlBranch")),
                            data.InformChannelID = GetIdBySelect2($("#ddlInformChannel")),
                            data.PwaIncidentTypeID = GetIdBySelect2($("#ddlIncidentType")),
                            data.PwaIncidentGroupID = GetIdBySelect2($("#ddlIncidentCategory")),
                            data.CaseTitle = GetIdBySelect2($("#ddlIncidentTitle")),
                            data.SubDistrict = GetIdBySelect2($("#ddlSubDistrict")),
                            data.Recorder = GetIdBySelect2($("#ddlRecorder")),
                            data.Informers[0].FirstName = data.FirstName,
                            data.Informers[0].SubDistrict = GetIdBySelect2($("#ddlSubDistrict")),
                            data.Informers[0].CustomerID = GetIdBySelect2($("#ddlCustomer")),
                            data.Informers[0].CustCode = GetIdBySelect2($("#ddlCustCode"));
                        data.Informers[0].CustomerAddress = $('textarea#txtAddress').val();
                        data.Informers[0].Telephone = $('#txtTelephone').val();

                        data.Informers[0].FirstName = data.FirstName;

                       

                        var fileUpload = $("#fIncidentImage").get(0);
                        var files = fileUpload.files;
                        
                        //incidentModal.incident.ImageFile = files[0];

                        var formData = new FormData();


                        formData.append("model", JSON.stringify(data));
                        formData.append("file", files[0]);
                        //formData.append("model", 'xxxx');
                        //formData.append("studImg", files[0]);

                        


                        PwaManager.PwaWaiting(true);
                        $.ajax({

                            url: _baseUrl+'/api/Incident/AddIncident',
                            data: formData,
                            type: "POST",
                            //dataType: "json",
                            processData: false,
                            contentType: false,
                            error: function (request, status, error) {
                                alert(request.responseText);
                            },
                            success: function (data) {
                                
                                PwaManager.PwaWaiting(false);
                                swal(
                                    {
                                        title: "สำเร็จ!",
                                        text: "บันทึกข้อมูลเรียบร้อย!",
                                        timer: 1500
                                    });

                                setTimeout(function () {
                                    document.location.href = _baseUrl + '/Incident/index'
                                }, 1500);
                            }
                        });


                    } catch (ex) {
                        alert(ex.message);
                    }
                },
                submitEdit: function (event) {
                    try {
                        

                        incidentModal.incident.Informers = [];
                        incidentModal.incident.Informers.push({
                            CustomerID: incidentModal.incident.CustomerID,
                            Title: incidentModal.incident.Title,
                            FirstName: incidentModal.incident.FirstName,
                            LastName: incidentModal.incident.LastName,
                            MeterNo: incidentModal.incident.MeterNo,
                            Telephone: incidentModal.incident.Telephone,
                            InformChannelID: incidentModal.incident.InformChannelID,
                            InformReference: incidentModal.incident.InformReference,


                            InformID: incidentModal.incident.InformID,
                            Province: incidentModal.incident.Province,
                            District: incidentModal.incident.District,
                            SubDistrict: incidentModal.incident.SubDistrict,
                            CustomerAddress: incidentModal.incident.CustomerAddress

                        });

                        var jsonData = JSON.stringify(incidentModal.incident);
                        var data = JSON.parse(jsonData);

                        data.FirstName = $('#ddlCustomer').val();
                        data.BracnchNo = $('#ddlBranch').val();
                        data.CustomerID = GetIdBySelect2($('#ddlCustomer'));

                        data.CaseLatitude = $("#ddlCaseLatitude").val(),
                            data.CaseLongtitude = $("#ddlCaseLongtitude").val(),
                            data.FirstName = $("#ddlCustomer").val(),
                            data.PwaInformReceiver = GetIdBySelect2($("#ddlPwaInformReceiver")),
                            data.SlaDetail = GetIdBySelect2($("#ddlSlaDetail")),
                            data.BracnchNo = GetIdBySelect2($("#ddlBranch")),
                            data.InformChannelID = GetIdBySelect2($("#ddlInformChannel")),
                            data.PwaIncidentTypeID = GetIdBySelect2($("#ddlIncidentType")),
                            data.PwaIncidentGroupID = GetIdBySelect2($("#ddlIncidentCategory")),
                            data.CaseTitle = GetIdBySelect2($("#ddlIncidentTitle")),
                            data.SubDistrict = GetIdBySelect2($("#ddlSubDistrict")),
                            data.Recorder = GetIdBySelect2($("#ddlRecorder")),
                            data.Informers[0].FirstName = data.FirstName,
                            data.Informers[0].SubDistrict = GetIdBySelect2($("#ddlSubDistrict")),
                            data.Informers[0].CustomerID = GetIdBySelect2($("#ddlCustomer")),
                            data.Informers[0].CustCode = GetIdBySelect2($("#ddlCustCode"));
                        data.Informers[0].CustomerAddress = $('textarea#txtAddress').val();
                        data.Informers[0].Telephone = $('#txtTelephone').val();

                        data.Informers[0].FirstName = data.FirstName;
                        
                        var fileUpload = $("#fIncidentImage").get(0);
                        var files = fileUpload.files;

                        //incidentModal.incident.ImageFile = files[0];

                        var formData = new FormData();


                        formData.append("model", JSON.stringify(data));
                        formData.append("file", files[0]);
                        


                        PwaManager.PwaWaiting(true);
                        $.ajax({

                            url: _baseUrl + '/api/Incident/SaveEditIncident',
                            data: formData,
                            type: "POST",
                            //dataType: "json",
                            processData: false,
                            contentType: false,
                            error: function (request, status, error) {
                                alert(request.responseText);
                            },
                            success: function (data) {

                                PwaManager.PwaWaiting(false);
                                swal(
                                    {
                                        title: "สำเร็จ!",
                                        text: "บันทึกข้อมูลเรียบร้อย!",
                                        timer: 1500
                                    });
                                setTimeout(function () {
                                    document.locatiodata.href = _baseUrl + '/Incident/index'
                                }, 1500);
                            }
                        });


                    } catch (ex) {
                        alert(ex.message);
                    }
                }
            },
            watch: {
                /*bindIncidentCategorie: function (value) {
                    alert(incident.PwaIncidentTypeID);

                }*/
                
            }
        });

        $('.incdatetime').datetimepicker({
            format: 'DD/MM/YYYY HH:mm',
            showTodayButton :true
        });
       
        $('#ddlIncidentType').html("");
        $.each(_incidentTypes, function (i, item) {
            $('#ddlIncidentType').append($('<option>', {
                value: item.ID,
                text: item.Name
            }));
        });

       

        
        /*$('#ddlIncidentType').selectpicker('refresh');*/

        onIncidentTypeChange();

        $('#ddlIncidentType').change(function () {
            onIncidentTypeChange();
        });
        
        $('#ddlIncidentCategory').change(function () {
            onIncidentCategoryChange();
        });


        $('#ddlProviceSearch').html("");
        $.each(_provinces, function (i, item) {
            $('#ddlProviceSearch').append($('<option>', {
                value: item.ID,
                text: item.Name
            }));
        });

      //  $('#ddlProviceSearch').select2();

        $('#ddlProvince').html("");
        $.each(_provinces, function (i, item) {
            $('#ddlProvince').append($('<option>', {
                value: item.ID,
                text: item.Name
            }));
        });
        /*$('#ddlProvince').selectpicker('refresh');*/
    //    onProvinceChange();
        

     /*   $('#ddlProvince').change(function () {
            onProvinceChange();
           
        });*/

        /*$('#ddlDistrict').selectpicker('refresh');*/
       
    /*    $('#ddlDistrict').change(function () {
            onDistrictChange();
        });
        */

     //   $('#ddlIncidentType').select2();
       

        /*$('#ddlSubDistrict').selectpicker('refresh');*/

        /**/


        $('#ddlIncidentType').val('7');
        $('#ddlIncidentType').change();

        $('#ddlInformChannel').val('1004');


        $('#mdMap').on('show.bs.modal', function () {
            try {
                SearchBranchIncident.autoSearch();
            } catch (e) {
                alert(e.message);
            }

        });

        var countries = [
            { value: 'Andorra', data: 'AD' },
            // ...
            { value: 'Zimbabwe', data: 'ZZ' }
        ];

        var selectLayer;

        $('.txtSearch').focus(function () { $(this).select(); });
        $('.txtSearch').on('input', function () {

            //autocompletex(this.value);
            GeoSearch.search(this.value, (data) => {
                var list = [];
                
                $.each(data.data, function (i, item) {
                    list.push({
                        'Text': item.FormattedAddress,
                        'Value': item.LocationID
                    })
                });
                console.log(list);
                PwaManager.PwaAutoComplete($(".txtSearch"), list,(val)=> {
                    
                   
                    GeoSearch.getDetail(val, (result) => {
                        if (result.success) {
                            let lat_long = '';
                            let lat = '';
                            let long = '';

                            if (selectLayer != null) {
                                selectLayer.remove();
                            }
                            for (let key in result.data) {
                                if (key =='LAT_LON' && result.data.hasOwnProperty(key)) {
                                    lat_long = result.data[key];
                                }
                            }

                            if (lat_long != null && lat_long != '') {
                                var splitResult = lat_long.split(',');
                                lat = splitResult[0];
                                long = splitResult[1];
                                selectLayer =    L.marker([lat * 1, long * 1]).addTo(map).bindPopup(this.value);
                                map.setView([lat * 1, long * 1], 12);
                            }

                        }
                    });
                  
                });

            });
            /*if (this.value.length > 3) {
                GeoSearch.search(this.value);
            }*/
        });

       
        

        if (_pageInfo.Mode == 'EDIT') {
            doInitEditData();
        }
    },
    test: () => {
        alert('xxxxx');
    },
    getCurrentDateTime: (hour) => {
        var currentdate = new Date();
        var datetime = currentdate.getDate() + "/"
            + currentdate.getMonth() + "/"
            + currentdate.getFullYear() + " "
            + (currentdate.getHours() + hour) + ":"
            + currentdate.getMinutes();
        return datetime;
    },
    getCurrentDate: (hour) => {
        var currentdate = new Date();
        var datetime = currentdate.getDate() + "/"
            + currentdate.getMonth() + "/"
            + currentdate.getFullYear() ;
        return datetime;
    },
    getData(module, parentid, onSuccess, onError) {

        var pData = { 'Module' :module, 'ParentID' : parentid };
        
        $.ajax({

            url: _baseUrl +'api/Incident/GetData/',
            data: pData,
            type: "POST",
            dataType: "json",
           
            error: function (request, status, error) {
                onError(request);
            },
            success: function (data) {
                onSuccess(data);
            }
        });
    }
}

function onIncidentTypeChange() {
    let incType = $('#ddlIncidentType').val()*1;
    let target = _incidentTypes.find(inc => inc.ID == incType);

    $('#ddlIncidentCategory').html("");
    $.each(target.Categories, function (i, item) {
        $('#ddlIncidentCategory').append($('<option>', {
            value: item.ID,
            text: item.Name
        }));
    });
    /*$('#ddlIncidentCategory').selectpicker('refresh');*/
    onIncidentCategoryChange();
}

function onIncidentCategoryChange() {
    let incType = $('#ddlIncidentType').val() * 1;
    let target = _incidentTypes.find(inc => inc.ID == incType);


    let incCategory = $('#ddlIncidentCategory').val()*1;
    let targetCategory = target.Categories.find(inc => inc.ID == incCategory);

    
    $('#ddlIncidentTitle').html("");
    $.each(targetCategory.Subjects, function (i, item) {
        $('#ddlIncidentTitle').append($('<option>', {
            value: item.ID,
            text: item.Name
        }));
    });
    /*$('#ddlIncidentTitle').selectpicker('refresh');*/

   

     
}

function onProvinceChange() {
    var province = $('#ddlProvince').val();
    IncidentCaseReceive.getData('DISTRICT', province, (data) => {
         
        if (data.length > 0) {
            $('#ddlDistrict').html("");
            $.each(data, function (i, item) {
                $('#ddlDistrict').append($('<option>', {
                    value: item.ID,
                    text: item.Name
                }));
            });
            $('#ddlDistrict').selectpicker('refresh');
            onDistrictChange();
        }

    }, (error => {
        alert(error.responseText);
    }));
}


function onDistrictChange() {
    var district = $('#ddlDistrict').val(); 
    IncidentCaseReceive.getData('SUBDISTRICT', district, (data) => {
         
        
        if (data.length > 0) {
            $('#ddlSubDistrict').html("");
            $.each(data, function (i, item) {
                $('#ddlSubDistrict').append($('<option>', {
                    value: item.ID,
                    text: item.Name
                }));
            });
            $('#ddlSubDistrict').selectpicker('refresh');
        }

    }, (error => {
        alert(error.responseText);
    }));
}

function doInitEditData() {
    try {
         

      /*  $('#ddlProvince').val( _pageInfo.EditModel.Province);
        $('#ddlProvince').trigger("change");

        $('#ddlDistrict').val( _pageInfo.EditModel.District);
        $('#ddlDistrict').trigger("change");
        */
        
        
        $('#ddlSubDistrict').val(_pageInfo.EditModel.SubDistrict);

        if (_pageInfo.EditModel.CaseLatitude != null && _pageInfo.EditModel.CaseLatitude != "" &&
            _pageInfo.EditModel.CaseLongtitude != null && _pageInfo.EditModel.CaseLongtitude != ""
        ) {
            L.marker([_pageInfo.EditModel.CaseLatitude * 1, _pageInfo.EditModel.CaseLongtitude*1]).addTo(map);
        }
/*
        $('#ddlIncidentType').val(_pageInfo.EditModel.PwaIncidentTypeID);
        $('#ddlIncidentType').trigger("change");

        $('#ddlIncidentCategory').val(_pageInfo.EditModel.PwaIncidentGroupID);
        $('#ddlIncidentCategory').trigger("change");
        
        $('#ddlIncidentTitle').val(_pageInfo.EditModel.CaseTitle);*/
       

        incidentModal.incident = _pageInfo.EditModel;
        
        $('#ddlCustomer').val(incidentModal.incident.FirstName).trigger("change");
        $("#ddlIncidentType").val(incidentModal.PwaIncidentTypeID).trigger("change");




    } catch (ex) {
        alert(ex.message);
    }
}

function searchBranchIncident() {
    try {
        SearchBranchIncident.searcByClick();
    } catch (e) {
        alert(e.message);
    }
    
}

let SearchBranchIncident = {
    autoSearch : () => {
        let customerCode = $('#ddlCustCode').val();
        let province = $('#ddlProvince').val();

        SearchBranchIncident.search(customerCode, province,'','',null,null);
    },
    searcByClick : () => {
        let customerCode = $('#ddlCustCode').val();
        let province = $('#ddlProviceSearch').val();
        let area = $('#txtAreaSearch').val();
        let startDate = $('#txtSartDateSearch').val();
        let endDate = $('#txtEndDateSearch').val();
        let status = $('#ddlStatusSearch').val();
        
        SearchBranchIncident.search(customerCode, province, detail, area, status, endDate);

    },
    search: (customer, provinceId,detail,area,startDate,endDate) => {

        var criteria = {
            CustomerNo : customer,
            ProvinceID: provinceId,
            Detail: detail,
            Area  : area,
            StartDate : startDate,
            EndDate: endDate,

        };

        PwaManager.PwaWaiting(true);
        $.ajax({

            url: _baseUrl + '/api/Incident/SearchBranchIncidents',
            data: criteria,
            type: "POST",
            dataType: "json",
            
            error: function (request, status, error) {
                alert(request.responseText);
            },
            success: function (data) {

                $('#divResult').html('');
                var html = '<table class="table table-striped table table-hover" data-animate="fade">';
                html += '    <thead>';
                html += '            <tr>';
                    
                html += '                <th class="" scope="col">วันเวลาที่รับแจ้ง</th>';
                html += '                <th class="" scope="col">เลขที่รับแจ้ง</th>';
                html += '                <th class="" scope="col">รายละเอียด</th>';
                html += '                        </thead>';
                html += '            <tbody>';
                if (data.Result != null && data.Result.Incidents != null && data.Result.Incidents.length > 0) {
                    $.each(data.Result.Incidents, function (index, item) {
                        html += '                <tr>';
                        html += '                    <td>' + item.ReceivedCaseDate + '</td>';
                        html += '                    <td>' + item.PwaIncidentNo + '</td>';
                        html += '                    <td>' + item.RequestCategory + '</td>';
                        html += '                </tr>';
                    });
          
                }
                html += '     </table>';
                $('#divResult').append(html);
                
                $('#divResult  table').DataTable({ "searching": false, "bLengthChange": false, "pageLength": 5});

                PwaManager.PwaWaiting(false);
               
                if (data.Result != null && data.Result.Incidents != null && data.Result.Incidents.length > 0) {
                    $.each(data.Result.Incidents, function (index, item) {
                        if (item.CaseLatitude != null && item.CaseLatitude != "" &&
                            item.CaseLongtitude != null && item.CaseLongtitude != ""
                        ) {
                            L.marker([item.CaseLatitude * 1, item.CaseLongtitude * 1]).addTo(map)
                                .bindPopup(`<b>เลขที่รับแจ้ง :  ${item.PwaIncidentNo}</b><br /><b>สาเหตุ :  ${item.RequestCategory}</b><br /><b>รายละเอียด :  ${item.RequestCategorySubject}</b>`).openPopup(); 
                        }
                    });

                     
                }
                 
                
                
            }
        });
    }
}



let GeoSearch = {
    token: "08a38faa15b0302ae1e415d18db8d78f3e3fabbd409c10e5de1ab15b8aa2f67573ad03bede273520",
    search: (searchKey,onSuccess) => {
        let searchData = {
            keyword: searchKey,
            key: GeoSearch.token,
            maxResult: 20
        }
        console.log(searchData);
        
        /**/
        $.ajax({

            url: "https://geosearch.cdg.co.th/g/search/autocomplete",
            data: searchData,
            type: "POST",
            dataType: "json",

            error: function (request, status, error) {
                
                console.log(request);
            },
            success: function (data) {
                console.log(data);
                onSuccess(data);
            }
        });

        


    },
    getDetail: (key, onSuccess) => {
        let searchData = {
            locationid: key,
            key: GeoSearch.token
        }
       
        /**/
        $.ajax({

            url: "https://geosearch.cdg.co.th/g/search/details",
            data: searchData,
            type: "POST",
            dataType: "json",

            error: function (request, status, error) {

                console.log(request);
            },
            success: function (data) {
                console.log(data);
                onSuccess(data);
            }
        });




    }
}


function autocompletex(val) {

    let apiKey = "08a38faa15b0302ae1e415d18db8d78f3e3fabbd409c10e5de1ab15b8aa2f67573ad03bede273520"
    let autocompleteUrl = "https://geosearch.cdg.co.th/g/search/autocomplete"

    $.post(autocompleteUrl,
        {
            keyword: val,
            key: apiKey,
            maxResult: 5
        }, result => {
            console.log(result);
            
        }, "json")
}
                            
$(document).ready(() => {
    console.log("document ready!");
    
    try {
        IncidentCaseReceive.init();
        
    } catch (e) {
        alert('error :'+ e.message);
    }

   
});

