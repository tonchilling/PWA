<<<<<<< .mine
! function (e) {
    var t = {};

    function n(a) {
        if (t[a]) return t[a].exports;
        var i = t[a] = {
            i: a,
            l: !1,
            exports: {}
        };
        return e[a].call(i.exports, i, i.exports, n), i.l = !0, i.exports
    }
    n.m = e, n.c = t, n.d = function (e, t, a) {
        n.o(e, t) || Object.defineProperty(e, t, {
            enumerable: !0,
            get: a
        })
    }, n.r = function (e) {
        "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {
            value: "Module"
        }), Object.defineProperty(e, "__esModule", {
            value: !0
        })
    }, n.t = function (e, t) {
        if (1 & t && (e = n(e)), 8 & t) return e;
        if (4 & t && "object" == typeof e && e && e.__esModule) return e;
        var a = Object.create(null);
        if (n.r(a), Object.defineProperty(a, "default", {
            enumerable: !0,
            value: e
        }), 2 & t && "string" != typeof e)
            for (var i in e) n.d(a, i, function (t) {
                return e[t]
            }.bind(null, i));
        return a
    }, n.n = function (e) {
        var t = e && e.__esModule ? function () {
            return e.default
        } : function () {
            return e
        };
        return n.d(t, "a", t), t
    }, n.o = function (e, t) {
        return Object.prototype.hasOwnProperty.call(e, t)
    }, n.p = "", n(n.s = 3)
}({
    3: function (e, t, n) {
        "use strict";
        n.r(t);
        var a = null,
            i = getBaseUrl(),
            d = {
                init: function () {
                    a = new Vue({
                        el: "#frmReceiveIncident",
                        data: {
                            message: "",
                            errors: [],
                            view: {
                                incidentTypes: _incidentTypes,
                                incidentCategories: [],
                                incidentSubjects: []
                            },
                            incident: {
                                PwaIncidentID: 0,
                                PwaIncidentNo: "Auto",
                                PwaInformReceiver: "",
                                PwaInformDate: d.getCurrentDate(),
                                PwaIncidentTypeID: 7,
                                PwaIncidentGroupID: 1,
                                PwaInformChannel: 1,
                                CaseTitle: "1",
                                CaseTitleDetail: "",
                                CaseDetail: "",
                                ResolvedDetail: "",
                                Sla: !0,
                                SlaDetail: "",
                                ReceivedCaseDate: d.getCurrentDateTime(0),
                                CompletedCaseDate: d.getCurrentDateTime(1),
                                CaseLatitude: "",
                                CaseLongtitude: "",
                                PwsIncidentAddress: "",
                                BracnchNo: "",
                                Recorder: "",
                                RecordDate: d.getCurrentDate(),
                                IncidentStatus: 1,
                                InformID: 0,
                                CustomerID: "",
                                Title: "1",
                                FirstName: "",
                                LastName: "",
                                MeterNo: "",
                                InformChannelID: 1,
                                InformReference: "",
                                Telephone: "",
                                Province: 1,
                                District: 1,
                                SubDistrict: 1,
                                CustomerAddress: "",
                                ImageFile: ""
                            }
                        },
                        methods: {
                            incidentTypeChange: function () {
                                try {
                                    var e = this.incident.PwaIncidentTypeID,
                                        t = _incidentTypes.find((function (t) {
                                            return t.ID == e
                                        }));
                                    this.view.incidentCategories = t.Categories, setTimeout((function () {
                                        $("#ddlIncidentCategory").selectpicker("refresh")
                                    }), 1e3)
                                } catch (e) {
                                    alert(e.message)
                                }
                            },
                            incidentCategoryChange: function () {
                                try {
                                    var e = this.incident.PwaIncidentGroupID,
                                        t = this.view.incidentCategories.find((function (t) {
                                            return t.ID == e
                                        }));
                                    this.view.incidentSubjects = t.Subjects, setTimeout((function () {
                                        $("#ddlIncidentTitle").selectpicker("refresh")
                                    }), 1e3)
                                } catch (e) {
                                    alert(e.message)
                                }
                            },
                            doSubmit: function (e) {
                                "EDIT" == _pageInfo.Mode ? this.submitEdit(e) : this.submit(e)
                            },
                            submit: function (e) {
                                try {
                                    a.incident.Informers = [], a.incident.Informers.push({
                                        CustomerID: a.incident.CustomerID,
                                        Title: a.incident.Title,
                                        FirstName: a.incident.FirstName,
                                        LastName: a.incident.LastName,
                                        MeterNo: a.incident.MeterNo,
                                        Telephone: a.incident.Telephone,
                                        InformChannelID: a.incident.InformChannelID,
                                        InformReference: a.incident.InformReference,
                                        InformID: a.incident.InformID,
                                        Province: a.incident.Province,
                                        District: a.incident.District,
                                        SubDistrict: a.incident.SubDistrict,
                                        CustomerAddress: a.incident.CustomerAddress
                                    });
                                    var t = JSON.stringify(a.incident),
                                        n = JSON.parse(t);
                                    n.CaseLatitude = $("#ddlCaseLatitude").val(), n.CaseLongtitude = $("#ddlCaseLongtitude").val(), n.FirstName = $("#ddlCustomer").val(), n.BracnchNo = $("#ddlBranch").val(), n.CustomerID = GetIdBySelect2($("#ddlCustomer")), n.CaseLatitude = $("#ddlCaseLatitude").val(), n.CaseLongtitude = $("#ddlCaseLongtitude").val(), n.FirstName = $("#ddlCustomer").val(), n.PwaInformReceiver = GetIdBySelect2($("#ddlPwaInformReceiver")), n.SlaDetail = GetIdBySelect2($("#ddlSlaDetail")), n.BracnchNo = GetIdBySelect2($("#ddlBranch")), n.InformChannelID = GetIdBySelect2($("#ddlInformChannel")), n.PwaIncidentTypeID = GetIdBySelect2($("#ddlIncidentType")), n.PwaIncidentGroupID = GetIdBySelect2($("#ddlIncidentCategory")), n.CaseTitle = GetIdBySelect2($("#ddlIncidentTitle")), n.SubDistrict = GetIdBySelect2($("#ddlSubDistrict")), n.Recorder = GetIdBySelect2($("#ddlRecorder")), n.Informers[0].FirstName = n.FirstName, n.Informers[0].SubDistrict = GetIdBySelect2($("#ddlSubDistrict")), n.Informers[0].CustomerID = GetIdBySelect2($("#ddlCustomer")), n.Informers[0].CustCode = GetIdBySelect2($("#ddlCustCode")), n.Informers[0].CustomerAddress = $("textarea#txtAddress").val(), n.Informers[0].Telephone = $("#txtTelephone").val(), n.Informers[0].FirstName = n.FirstName;
                                    var d = $("#fIncidentImage").get(0).files,
                                        r = new FormData;
                                    r.append("model", JSON.stringify(n)), r.append("file", d[0]), PwaManager.PwaWaiting(!0), $.ajax({
                                        url: i + "/api/Incident/AddIncident",
                                        data: r,
                                        type: "POST",
                                        processData: !1,
                                        contentType: !1,
                                        error: function (e, t, n) {
                                            alert(e.responseText)
                                        },
                                        success: function (e) {
                                            PwaManager.PwaWaiting(!1), swal({
                                                title: "??????????????????!",
                                                text: "???????????????????????????????????????????????????????????????!",
                                                timer: 1500
                                            }), setTimeout((function () {
                                                document.location.href = i + "/Incident/index"
                                            }), 1500)
                                        }
                                    })
                                } catch (e) {
                                    alert(e.message)
                                }
                            },
                            submitEdit: function (e) {
                                try {
                                    a.incident.Informers = [], a.incident.Informers.push({
                                        CustomerID: a.incident.CustomerID,
                                        Title: a.incident.Title,
                                        FirstName: a.incident.FirstName,
                                        LastName: a.incident.LastName,
                                        MeterNo: a.incident.MeterNo,
                                        Telephone: a.incident.Telephone,
                                        InformChannelID: a.incident.InformChannelID,
                                        InformReference: a.incident.InformReference,
                                        InformID: a.incident.InformID,
                                        Province: a.incident.Province,
                                        District: a.incident.District,
                                        SubDistrict: a.incident.SubDistrict,
                                        CustomerAddress: a.incident.CustomerAddress
                                    });
                                    var t = JSON.stringify(a.incident),
                                        n = JSON.parse(t);
                                    n.FirstName = $("#ddlCustomer").val(), n.BracnchNo = $("#ddlBranch").val(),  n.CaseLatitude = $("#ddlCaseLatitude").val(), n.CaseLongtitude = $("#ddlCaseLongtitude").val(), n.PwaInformReceiver = GetIdBySelect2($("#ddlPwaInformReceiver")), n.SlaDetail = GetIdBySelect2($("#ddlSlaDetail")), n.BracnchNo = GetIdBySelect2($("#ddlBranch")), n.InformChannelID = GetIdBySelect2($("#ddlInformChannel")), n.PwaIncidentTypeID = GetIdBySelect2($("#ddlIncidentType")), n.PwaIncidentGroupID = GetIdBySelect2($("#ddlIncidentCategory")), n.CaseTitle = GetIdBySelect2($("#ddlIncidentTitle")), n.SubDistrict = GetIdBySelect2($("#ddlSubDistrict")), n.Recorder = GetIdBySelect2($("#ddlRecorder")), n.Informers[0].FirstName = n.FirstName, n.Informers[0].SubDistrict = GetIdBySelect2($("#ddlSubDistrict")), n.Informers[0].CustomerID = GetIdBySelect2($("#ddlCustomer")), n.Informers[0].CustCode = GetIdBySelect2($("#ddlCustCode")), n.Informers[0].CustomerAddress = $("textarea#txtAddress").val(), n.Informers[0].Telephone = $("#txtTelephone").val(), n.Informers[0].FirstName = n.FirstName;
                                    var d = $("#fIncidentImage").get(0).files,
                                        r = new FormData;
                                    r.append("model", JSON.stringify(n)), r.append("file", d[0]), PwaManager.PwaWaiting(!0), $.ajax({
                                        url: i + "/api/Incident/SaveEditIncident",
                                        data: r,
                                        type: "POST",
                                        processData: !1,
                                        contentType: !1,
                                        error: function (e, t, n) {
                                            alert(e.responseText)
                                        },
                                        success: function (e) {
                                            PwaManager.PwaWaiting(!1), swal({
                                                title: "??????????????????!",
                                                text: "???????????????????????????????????????????????????????????????!",
                                                timer: 1500
                                            }), setTimeout((function () {
                                                document.locatiodata.href = i + "/Incident/index"
                                            }), 1500)
                                        }
                                    })
                                } catch (e) {
                                    alert(e.message)
                                }
                            }
                        },
                        watch: {}
                    }), $(".incdatetime").datetimepicker({
                        format: "DD/MM/YYYY HH:mm",
                        showTodayButton: !0
                    }), $("#ddlIncidentType").html(""), $.each(_incidentTypes, (function (e, t) {
                        $("#ddlIncidentType").append($("<option>", {
                            value: t.ID,
                            text: t.Name
                        }))
                    })), r(), $("#ddlIncidentType").change((function () {
                        r()
                    })), $("#ddlIncidentCategory").change((function () {
                        c()
                    })), $("#ddlProviceSearch").html(""), $.each(_provinces, (function (e, t) {
                        $("#ddlProviceSearch").append($("<option>", {
                            value: t.ID,
                            text: t.Name
                        }))
                    })), $("#ddlProvince").html(""), $.each(_provinces, (function (e, t) {
                        $("#ddlProvince").append($("<option>", {
                            value: t.ID,
                            text: t.Name
                        }))
                    })), $("#ddlIncidentType").val("7"), $("#ddlIncidentType").change(), $("#ddlInformChannel").val("1004"), $("#mdMap").on("show.bs.modal", (function () {
                        try {
                            o.autoSearch()
                        } catch (e) {
                            alert(e.message)
                        }
                    }));
                    var e;
                    $(".txtSearch").focus((function () {
                        $(this).select()
                    })), $(".txtSearch").on("input", (function () {
                        var t = this;
                        l.search(this.value, (function (n) {
                            var a = [];
                            $.each(n.data, (function (e, t) {
                                a.push({
                                    Text: t.FormattedAddress,
                                    Value: t.LocationID
                                })
                            })), console.log(a), PwaManager.PwaAutoComplete($(".txtSearch"), a, (function (n) {
                                l.getDetail(n, (function (n) {
                                    if (n.success) {
                                        var a = "",
                                            i = "",
                                            d = "";
                                        for (var r in null != e && e.remove(), n.data) "LAT_LON" == r && n.data.hasOwnProperty(r) && (a = n.data[r]);
                                        if (null != a && "" != a) {
                                            var c = a.split(",");
                                            i = c[0], d = c[1], e = L.marker([1 * i, 1 * d]).addTo(map).bindPopup(t.value), map.setView([1 * i, 1 * d], 12)
                                        }
                                    }
                                }))
                            }))
                        }))
                    })), "EDIT" == _pageInfo.Mode && function () {
                        try {
                            $("#ddlSubDistrict").val(_pageInfo.EditModel.SubDistrict), null != _pageInfo.EditModel.CaseLatitude && "" != _pageInfo.EditModel.CaseLatitude && null != _pageInfo.EditModel.CaseLongtitude && "" != _pageInfo.EditModel.CaseLongtitude && L.marker([1 * _pageInfo.EditModel.CaseLatitude, 1 * _pageInfo.EditModel.CaseLongtitude]).addTo(map), a.incident = _pageInfo.EditModel, $("#ddlCustomer").val(a.incident.FirstName).trigger("change"), $("#ddlIncidentType").val(a.PwaIncidentTypeID).trigger("change")
                        } catch (e) {
                            alert(e.message)
                        }
                    }()
                },
                test: function () {
                    alert("xxxxx")
                },
                getCurrentDateTime: function (e) {
                    var t = new Date;
                    return t.getDate() + "/" + t.getMonth() + "/" + t.getFullYear() + " " + (t.getHours() + e) + ":" + t.getMinutes()
                },
                getCurrentDate: function (e) {
                    var t = new Date;
                    return t.getDate() + "/" + t.getMonth() + "/" + t.getFullYear()
                },
                getData: function (e, t, n, a) {
                    var d = {
                        Module: e,
                        ParentID: t
                    };
                    $.ajax({
                        url: i + "api/Incident/GetData/",
                        data: d,
                        type: "POST",
                        dataType: "json",
                        error: function (e, t, n) {
                            a(e)
                        },
                        success: function (e) {
                            n(e)
                        }
                    })
                }
            };

        function r() {
            var e = 1 * $("#ddlIncidentType").val(),
                t = _incidentTypes.find((function (t) {
                    return t.ID == e
                }));
            $("#ddlIncidentCategory").html(""), $.each(t.Categories, (function (e, t) {
                $("#ddlIncidentCategory").append($("<option>", {
                    value: t.ID,
                    text: t.Name
                }))
            })), c()
        }

        function c() {
            var e = 1 * $("#ddlIncidentType").val(),
                t = _incidentTypes.find((function (t) {
                    return t.ID == e
                })),
                n = 1 * $("#ddlIncidentCategory").val(),
                a = t.Categories.find((function (e) {
                    return e.ID == n
                }));
            $("#ddlIncidentTitle").html(""), $.each(a.Subjects, (function (e, t) {
                $("#ddlIncidentTitle").append($("<option>", {
                    value: t.ID,
                    text: t.Name
                }))
            }))
        }
        var o = {
            autoSearch: function () {
                var e = $("#ddlCustCode").val(),
                    t = $("#ddlProvince").val();
                o.search(e, t, "", "", null, null)
            },
            searcByClick: function () {
                var e = $("#ddlCustCode").val(),
                    t = $("#ddlProviceSearch").val(),
                    n = $("#txtAreaSearch").val(),
                    a = ($("#txtSartDateSearch").val(), $("#txtEndDateSearch").val()),
                    i = $("#ddlStatusSearch").val();
                o.search(e, t, detail, n, i, a)
            },
            search: function (e, t, n, a, d, r) {
                var c = {
                    CustomerNo: e,
                    ProvinceID: t,
                    Detail: n,
                    Area: a,
                    StartDate: d,
                    EndDate: r
                };
                PwaManager.PwaWaiting(!0), $.ajax({
                    url: i + "/api/Incident/SearchBranchIncidents",
                    data: c,
                    type: "POST",
                    dataType: "json",
                    error: function (e, t, n) {
                        alert(e.responseText)
                    },
                    success: function (e) {
                        $("#divResult").html("");
                        var t = '<table class="table table-striped table table-hover" data-animate="fade">';
                        t += "    <thead>", t += "            <tr>", t += '                <th class="" scope="col">???????????????????????????????????????????????????</th>', t += '                <th class="" scope="col">???????????????????????????????????????</th>', t += '                <th class="" scope="col">??????????????????????????????</th>', t += "                        </thead>", t += "            <tbody>", null != e.Result && null != e.Result.Incidents && e.Result.Incidents.length > 0 && $.each(e.Result.Incidents, (function (e, n) {
                            t += "                <tr>", t += "                    <td>" + n.ReceivedCaseDate + "</td>", t += "                    <td>" + n.PwaIncidentNo + "</td>", t += "                    <td>" + n.RequestCategory + "</td>", t += "                </tr>"
                        })), t += "     </table>", $("#divResult").append(t), $("#divResult  table").DataTable({
                            searching: !1,
                            bLengthChange: !1,
                            pageLength: 5
                        }), PwaManager.PwaWaiting(!1), null != e.Result && null != e.Result.Incidents && e.Result.Incidents.length > 0 && $.each(e.Result.Incidents, (function (e, t) {
                            null != t.CaseLatitude && "" != t.CaseLatitude && null != t.CaseLongtitude && "" != t.CaseLongtitude && L.marker([1 * t.CaseLatitude, 1 * t.CaseLongtitude]).addTo(map).bindPopup("<b>??????????????????????????????????????? :  ".concat(t.PwaIncidentNo, "</b><br /><b>?????????????????? :  ").concat(t.RequestCategory, "</b><br /><b>?????????????????????????????? :  ").concat(t.RequestCategorySubject, "</b>")).openPopup()
                        }))
                    }
                })
            }
        },
            l = {
                token: "08a38faa15b0302ae1e415d18db8d78f3e3fabbd409c10e5de1ab15b8aa2f67573ad03bede273520",
                search: function (e, t) {
                    var n = {
                        keyword: e,
                        key: l.token,
                        maxResult: 20
                    };
                    console.log(n), $.ajax({
                        url: "https://geosearch.cdg.co.th/g/search/autocomplete",
                        data: n,
                        type: "POST",
                        dataType: "json",
                        error: function (e, t, n) {
                            console.log(e)
                        },
                        success: function (e) {
                            console.log(e), t(e)
                        }
                    })
                },
                getDetail: function (e, t) {
                    var n = {
                        locationid: e,
                        key: l.token
                    };
                    $.ajax({
                        url: "https://geosearch.cdg.co.th/g/search/details",
                        data: n,
                        type: "POST",
                        dataType: "json",
                        error: function (e, t, n) {
                            console.log(e)
                        },
                        success: function (e) {
                            console.log(e), t(e)
                        }
                    })
                }
            };
        $(document).ready((function () {
            console.log("document ready!");
            try {
                d.init()
            } catch (e) {
                alert("error :" + e.message)
            }
        }))
    }
});||||||| .r386
!function(e){var t={};function n(a){if(t[a])return t[a].exports;var i=t[a]={i:a,l:!1,exports:{}};return e[a].call(i.exports,i,i.exports,n),i.l=!0,i.exports}n.m=e,n.c=t,n.d=function(e,t,a){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:a})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var a=Object.create(null);if(n.r(a),Object.defineProperty(a,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var i in e)n.d(a,i,function(t){return e[t]}.bind(null,i));return a},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="",n(n.s=3)}({3:function(e,t,n){"use strict";n.r(t);var a=null,i=getBaseUrl(),r={init:function(){a=new Vue({el:"#frmReceiveIncident",data:{message:"",errors:[],view:{incidentTypes:_incidentTypes,incidentCategories:[],incidentSubjects:[]},incident:{PwaIncidentID:0,PwaIncidentNo:"Auto",PwaInformReceiver:"",PwaInformDate:r.getCurrentDate(),PwaIncidentTypeID:7,PwaIncidentGroupID:1,PwaInformChannel:1,CaseTitle:"1",CaseTitleDetail:"",CaseDetail:"",ResolvedDetail:"",Sla:!0,SlaDetail:"",ReceivedCaseDate:r.getCurrentDateTime(0),CompletedCaseDate:r.getCurrentDateTime(1),CaseLatitude:"",CaseLongtitude:"",PwsIncidentAddress:"",BracnchNo:"",Recorder:"",RecordDate:r.getCurrentDate(),IncidentStatus:1,InformID:0,CustomerID:"",Title:"1",FirstName:"",LastName:"",MeterNo:"",InformChannelID:1,InformReference:"",Telephone:"",Province:1,District:1,SubDistrict:1,CustomerAddress:"",ImageFile:""}},methods:{incidentTypeChange:function(){try{var e=this.incident.PwaIncidentTypeID,t=_incidentTypes.find((function(t){return t.ID==e}));this.view.incidentCategories=t.Categories,setTimeout((function(){$("#ddlIncidentCategory").selectpicker("refresh")}),1e3)}catch(e){alert(e.message)}},incidentCategoryChange:function(){try{var e=this.incident.PwaIncidentGroupID,t=this.view.incidentCategories.find((function(t){return t.ID==e}));this.view.incidentSubjects=t.Subjects,setTimeout((function(){$("#ddlIncidentTitle").selectpicker("refresh")}),1e3)}catch(e){alert(e.message)}},doSubmit:function(e){"EDIT"==_pageInfo.Mode?this.submitEdit(e):this.submit(e)},submit:function(e){try{a.incident.Informers=[],a.incident.Informers.push({CustomerID:a.incident.CustomerID,Title:a.incident.Title,FirstName:a.incident.FirstName,LastName:a.incident.LastName,MeterNo:a.incident.MeterNo,Telephone:a.incident.Telephone,InformChannelID:a.incident.InformChannelID,InformReference:a.incident.InformReference,InformID:a.incident.InformID,Province:a.incident.Province,District:a.incident.District,SubDistrict:a.incident.SubDistrict,CustomerAddress:a.incident.CustomerAddress});var t=JSON.stringify(a.incident),n=JSON.parse(t);n.CaseLatitude=$("#ddlCaseLatitude").val(),n.CaseLongtitude=$("#ddlCaseLongtitude").val(),n.FirstName=$("#ddlCustomer").val(),n.BracnchNo=$("#ddlBranch").val(),n.Informers[0].FirstName=n.FirstName;var r=$("#fIncidentImage").get(0).files,c=new FormData;c.append("model",JSON.stringify(n)),c.append("file",r[0]),PwaManager.PwaWaiting(!0),$.ajax({url:i+"/api/Incident/AddIncident",data:c,type:"POST",processData:!1,contentType:!1,error:function(e,t,n){alert(e.responseText)},success:function(e){PwaManager.PwaWaiting(!1),swal({title:"??????????????????!",text:"???????????????????????????????????????????????????????????????!",timer:1500}),setTimeout((function(){document.location.href=i+"/Incident/index"}),1500)}})}catch(e){alert(e.message)}},submitEdit:function(e){try{a.incident.Informers=[],a.incident.Informers.push({CustomerID:a.incident.CustomerID,Title:a.incident.Title,FirstName:a.incident.FirstName,LastName:a.incident.LastName,MeterNo:a.incident.MeterNo,Telephone:a.incident.Telephone,InformChannelID:a.incident.InformChannelID,InformReference:a.incident.InformReference,InformID:a.incident.InformID,Province:a.incident.Province,District:a.incident.District,SubDistrict:a.incident.SubDistrict,CustomerAddress:a.incident.CustomerAddress});var t=JSON.stringify(a.incident),n=JSON.parse(t);n.CaseLatitude=$("#ddlCaseLatitude").val(),n.CaseLongtitude=$("#ddlCaseLongtitude").val(),n.FirstName=$("#ddlCustomer").val(),n.BracnchNo=$("#ddlBranch").val(),n.Informers[0].FirstName=n.FirstName;var r=$("#fIncidentImage").get(0).files,c=new FormData;c.append("model",JSON.stringify(n)),c.append("file",r[0]),PwaManager.PwaWaiting(!0),$.ajax({url:i+"/api/Incident/SaveEditIncident",data:c,type:"POST",processData:!1,contentType:!1,error:function(e,t,n){alert(e.responseText)},success:function(e){PwaManager.PwaWaiting(!1),swal({title:"??????????????????!",text:"???????????????????????????????????????????????????????????????!",timer:1500}),setTimeout((function(){document.location.href=i+"/Incident/index"}),1500)}})}catch(e){alert(e.message)}}},watch:{}}),$(".incdatetime").datetimepicker({format:"DD/MM/YYYY HH:mm",showTodayButton:!0}),$("#ddlIncidentType").html(""),$.each(_incidentTypes,(function(e,t){$("#ddlIncidentType").append($("<option>",{value:t.ID,text:t.Name}))})),c(),$("#ddlIncidentType").change((function(){c()})),$("#ddlIncidentCategory").change((function(){d()})),$("#ddlProviceSearch").html(""),$.each(_provinces,(function(e,t){$("#ddlProviceSearch").append($("<option>",{value:t.ID,text:t.Name}))})),$("#ddlProviceSearch").select2(),$("#ddlProvince").html(""),$.each(_provinces,(function(e,t){$("#ddlProvince").append($("<option>",{value:t.ID,text:t.Name}))})),o(),$("#ddlProvince").change((function(){o()})),$("#ddlDistrict").change((function(){s()})),$("#ddlIncidentType").select2(),$("#ddlCustomer").select2(),$("#ddlIncidentType").val("7"),$("#ddlIncidentType").change(),$("#ddlInformChannel").val("1004"),$("#mdMap").on("show.bs.modal",(function(){try{l.autoSearch()}catch(e){alert(e.message)}}));$(".txtSearch").focus((function(){$(this).select()})),$(".txtSearch").on("input",(function(){var e=this;u.search(this.value,(function(t){var n=[];$.each(t.data,(function(e,t){n.push({Text:t.FormattedAddress,Value:t.LocationID})})),console.log(n),PwaManager.PwaAutoComplete($(".txtSearch"),n,(function(t){u.getDetail(t,(function(t){if(t.success){var n="",a="",i="";for(var r in t.data)"LAT_LON"==r&&t.data.hasOwnProperty(r)&&(n=t.data[r]);if(null!=n&&""!=n){var c=n.split(",");a=c[0],i=c[1],L.marker([1*a,1*i]).addTo(map).bindPopup(e.value),map.setView(n,7)}}}))}))}))})),"EDIT"==_pageInfo.Mode&&function(){try{$("#ddlProvince").val(_pageInfo.EditModel.Province),$("#ddlProvince").trigger("change"),$("#ddlDistrict").val(_pageInfo.EditModel.District),$("#ddlDistrict").trigger("change"),$("#ddlSubDistrict").val(_pageInfo.EditModel.SubDistrict),null!=_pageInfo.EditModel.CaseLatitude&&""!=_pageInfo.EditModel.CaseLatitude&&null!=_pageInfo.EditModel.CaseLongtitude&&""!=_pageInfo.EditModel.CaseLongtitude&&L.marker([1*_pageInfo.EditModel.CaseLatitude,1*_pageInfo.EditModel.CaseLongtitude]).addTo(map),a.incident=_pageInfo.EditModel,$("#ddlCustomer").val(a.incident.FirstName).trigger("change")}catch(e){alert(e.message)}}()},test:function(){alert("xxxxx")},getCurrentDateTime:function(e){var t=new Date;return t.getDate()+"/"+t.getMonth()+"/"+t.getFullYear()+" "+(t.getHours()+e)+":"+t.getMinutes()},getCurrentDate:function(e){var t=new Date;return t.getDate()+"/"+t.getMonth()+"/"+t.getFullYear()},getData:function(e,t,n,a){var r={Module:e,ParentID:t};$.ajax({url:i+"api/Incident/GetData/",data:r,type:"POST",dataType:"json",error:function(e,t,n){a(e)},success:function(e){n(e)}})}};function c(){var e=1*$("#ddlIncidentType").val(),t=_incidentTypes.find((function(t){return t.ID==e}));$("#ddlIncidentCategory").html(""),$.each(t.Categories,(function(e,t){$("#ddlIncidentCategory").append($("<option>",{value:t.ID,text:t.Name}))})),d()}function d(){var e=1*$("#ddlIncidentType").val(),t=_incidentTypes.find((function(t){return t.ID==e})),n=1*$("#ddlIncidentCategory").val(),a=t.Categories.find((function(e){return e.ID==n}));$("#ddlIncidentTitle").html(""),$.each(a.Subjects,(function(e,t){$("#ddlIncidentTitle").append($("<option>",{value:t.ID,text:t.Name}))}))}function o(){var e=$("#ddlProvince").val();r.getData("DISTRICT",e,(function(e){e.length>0&&($("#ddlDistrict").html(""),$.each(e,(function(e,t){$("#ddlDistrict").append($("<option>",{value:t.ID,text:t.Name}))})),$("#ddlDistrict").selectpicker("refresh"),s())}),(function(e){alert(e.responseText)}))}function s(){var e=$("#ddlDistrict").val();r.getData("SUBDISTRICT",e,(function(e){e.length>0&&($("#ddlSubDistrict").html(""),$.each(e,(function(e,t){$("#ddlSubDistrict").append($("<option>",{value:t.ID,text:t.Name}))})),$("#ddlSubDistrict").selectpicker("refresh"))}),(function(e){alert(e.responseText)}))}var l={autoSearch:function(){var e=$("#ddlCustCode").val(),t=$("#ddlProvince").val();l.search(e,t,"","",null,null)},searcByClick:function(){var e=$("#ddlCustCode").val(),t=$("#ddlProviceSearch").val(),n=$("#txtAreaSearch").val(),a=($("#txtSartDateSearch").val(),$("#txtEndDateSearch").val()),i=$("#ddlStatusSearch").val();l.search(e,t,detail,n,i,a)},search:function(e,t,n,a,r,c){var d={CustomerNo:e,ProvinceID:t,Detail:n,Area:a,StartDate:r,EndDate:c};PwaManager.PwaWaiting(!0),$.ajax({url:i+"/api/Incident/SearchBranchIncidents",data:d,type:"POST",dataType:"json",error:function(e,t,n){alert(e.responseText)},success:function(e){$("#divResult").html("");var t='<table class="table table-striped table table-hover" data-animate="fade">';t+="    <thead>",t+="            <tr>",t+='                <th class="" scope="col">???????????????????????????????????????????????????</th>',t+='                <th class="" scope="col">???????????????????????????????????????</th>',t+='                <th class="" scope="col">??????????????????????????????</th>',t+="                        </thead>",t+="            <tbody>",null!=e.Result&&null!=e.Result.Incidents&&e.Result.Incidents.length>0&&$.each(e.Result.Incidents,(function(e,n){t+="                <tr>",t+="                    <td>"+n.ReceivedCaseDate+"</td>",t+="                    <td>"+n.PwaIncidentNo+"</td>",t+="                    <td>"+n.RequestCategory+"</td>",t+="                </tr>"})),t+="     </table>",$("#divResult").append(t),$("#divResult  table").DataTable({searching:!1,bLengthChange:!1,pageLength:5}),PwaManager.PwaWaiting(!1),null!=e.Result&&null!=e.Result.Incidents&&e.Result.Incidents.length>0&&$.each(e.Result.Incidents,(function(e,t){null!=t.CaseLatitude&&""!=t.CaseLatitude&&null!=t.CaseLongtitude&&""!=t.CaseLongtitude&&L.marker([1*t.CaseLatitude,1*t.CaseLongtitude]).addTo(map).bindPopup("<b>??????????????????????????????????????? :  ".concat(t.PwaIncidentNo,"</b><br /><b>?????????????????? :  ").concat(t.RequestCategory,"</b><br /><b>?????????????????????????????? :  ").concat(t.RequestCategorySubject,"</b>")).openPopup()}))}})}},u={token:"08a38faa15b0302ae1e415d18db8d78f3e3fabbd409c10e5de1ab15b8aa2f67573ad03bede273520",search:function(e,t){var n={keyword:e,key:u.token,maxResult:20};console.log(n),$.ajax({url:"https://geosearch.cdg.co.th/g/search/autocomplete",data:n,type:"POST",dataType:"json",error:function(e,t,n){console.log(e)},success:function(e){console.log(e),t(e)}})},getDetail:function(e,t){var n={locationid:e,key:u.token};$.ajax({url:"https://geosearch.cdg.co.th/g/search/details",data:n,type:"POST",dataType:"json",error:function(e,t,n){console.log(e)},success:function(e){console.log(e),t(e)}})}};$(document).ready((function(){console.log("document ready!");try{r.init()}catch(e){alert("error :"+e.message)}}))}});=======
!function(e){var t={};function n(a){if(t[a])return t[a].exports;var i=t[a]={i:a,l:!1,exports:{}};return e[a].call(i.exports,i,i.exports,n),i.l=!0,i.exports}n.m=e,n.c=t,n.d=function(e,t,a){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:a})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var a=Object.create(null);if(n.r(a),Object.defineProperty(a,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var i in e)n.d(a,i,function(t){return e[t]}.bind(null,i));return a},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="",n(n.s=3)}({3:function(e,t,n){"use strict";n.r(t);var a=null,i=getBaseUrl(),r={init:function(){a=new Vue({el:"#frmReceiveIncident",data:{message:"",errors:[],view:{incidentTypes:_incidentTypes,incidentCategories:[],incidentSubjects:[]},incident:{PwaIncidentID:0,PwaIncidentNo:"Auto",PwaInformReceiver:"",PwaInformDate:r.getCurrentDate(),PwaIncidentTypeID:7,PwaIncidentGroupID:1,PwaInformChannel:1,CaseTitle:"1",CaseTitleDetail:"",CaseDetail:"",ResolvedDetail:"",Sla:!0,SlaDetail:"",ReceivedCaseDate:r.getCurrentDateTime(0),CompletedCaseDate:r.getCurrentDateTime(1),CaseLatitude:"",CaseLongtitude:"",PwsIncidentAddress:"",BracnchNo:"",Recorder:"",RecordDate:r.getCurrentDate(),IncidentStatus:1,InformID:0,CustomerID:"",Title:"1",FirstName:"",LastName:"",MeterNo:"",InformChannelID:1,InformReference:"",Telephone:"",Province:1,District:1,SubDistrict:1,CustomerAddress:"",ImageFile:""}},methods:{incidentTypeChange:function(){try{var e=this.incident.PwaIncidentTypeID,t=_incidentTypes.find((function(t){return t.ID==e}));this.view.incidentCategories=t.Categories,setTimeout((function(){$("#ddlIncidentCategory").selectpicker("refresh")}),1e3)}catch(e){alert(e.message)}},incidentCategoryChange:function(){try{var e=this.incident.PwaIncidentGroupID,t=this.view.incidentCategories.find((function(t){return t.ID==e}));this.view.incidentSubjects=t.Subjects,setTimeout((function(){$("#ddlIncidentTitle").selectpicker("refresh")}),1e3)}catch(e){alert(e.message)}},doSubmit:function(e){"EDIT"==_pageInfo.Mode?this.submitEdit(e):this.submit(e)},submit:function(e){try{a.incident.Informers=[],a.incident.Informers.push({CustomerID:a.incident.CustomerID,Title:a.incident.Title,FirstName:a.incident.FirstName,LastName:a.incident.LastName,MeterNo:a.incident.MeterNo,Telephone:a.incident.Telephone,InformChannelID:a.incident.InformChannelID,InformReference:a.incident.InformReference,InformID:a.incident.InformID,Province:a.incident.Province,District:a.incident.District,SubDistrict:a.incident.SubDistrict,CustomerAddress:a.incident.CustomerAddress});var t=JSON.stringify(a.incident),n=JSON.parse(t);n.CaseLatitude=$("#ddlCaseLatitude").val(),n.CaseLongtitude=$("#ddlCaseLongtitude").val(),n.FirstName=$("#ddlCustomer").val(),n.BracnchNo=$("#ddlBranch").val(),n.CustomerID=GetIdBySelect2($("#ddlCustomer")),n.Informers[0].FirstName=n.FirstName;var r=$("#fIncidentImage").get(0).files,c=new FormData;c.append("model",JSON.stringify(n)),c.append("file",r[0]),PwaManager.PwaWaiting(!0),$.ajax({url:i+"/api/Incident/AddIncident",data:c,type:"POST",processData:!1,contentType:!1,error:function(e,t,n){alert(e.responseText)},success:function(e){PwaManager.PwaWaiting(!1),swal({title:"??????????????????!",text:"???????????????????????????????????????????????????????????????!",timer:1500}),setTimeout((function(){document.location.href=i+"/Incident/index"}),1500)}})}catch(e){alert(e.message)}},submitEdit:function(e){try{a.incident.Informers=[],a.incident.Informers.push({CustomerID:a.incident.CustomerID,Title:a.incident.Title,FirstName:a.incident.FirstName,LastName:a.incident.LastName,MeterNo:a.incident.MeterNo,Telephone:a.incident.Telephone,InformChannelID:a.incident.InformChannelID,InformReference:a.incident.InformReference,InformID:a.incident.InformID,Province:a.incident.Province,District:a.incident.District,SubDistrict:a.incident.SubDistrict,CustomerAddress:a.incident.CustomerAddress});var t=JSON.stringify(a.incident),n=JSON.parse(t);n.CaseLatitude=$("#ddlCaseLatitude").val(),n.CaseLongtitude=$("#ddlCaseLongtitude").val(),n.FirstName=$("#ddlCustomer").val(),n.BracnchNo=$("#ddlBranch").val(),n.Informers[0].FirstName=n.FirstName;var r=$("#fIncidentImage").get(0).files,c=new FormData;c.append("model",JSON.stringify(n)),c.append("file",r[0]),PwaManager.PwaWaiting(!0),$.ajax({url:i+"/api/Incident/SaveEditIncident",data:c,type:"POST",processData:!1,contentType:!1,error:function(e,t,n){alert(e.responseText)},success:function(e){PwaManager.PwaWaiting(!1),swal({title:"??????????????????!",text:"???????????????????????????????????????????????????????????????!",timer:1500}),setTimeout((function(){document.location.href=i+"/Incident/index"}),1500)}})}catch(e){alert(e.message)}}},watch:{}}),$(".incdatetime").datetimepicker({format:"DD/MM/YYYY HH:mm",showTodayButton:!0}),$("#ddlIncidentType").html(""),$.each(_incidentTypes,(function(e,t){$("#ddlIncidentType").append($("<option>",{value:t.ID,text:t.Name}))})),c(),$("#ddlIncidentType").change((function(){c()})),$("#ddlIncidentCategory").change((function(){o()})),$("#ddlProviceSearch").html(""),$.each(_provinces,(function(e,t){$("#ddlProviceSearch").append($("<option>",{value:t.ID,text:t.Name}))})),$("#ddlProviceSearch").select2(),$("#ddlProvince").html(""),$.each(_provinces,(function(e,t){$("#ddlProvince").append($("<option>",{value:t.ID,text:t.Name}))})),$("#ddlIncidentType").select2(),$("#ddlCustomer").select2(),$("#ddlIncidentType").val("7"),$("#ddlIncidentType").change(),$("#ddlInformChannel").val("1004"),$("#mdMap").on("show.bs.modal",(function(){try{d.autoSearch()}catch(e){alert(e.message)}}));var e;$(".txtSearch").focus((function(){$(this).select()})),$(".txtSearch").on("input",(function(){var t=this;s.search(this.value,(function(n){var a=[];$.each(n.data,(function(e,t){a.push({Text:t.FormattedAddress,Value:t.LocationID})})),console.log(a),PwaManager.PwaAutoComplete($(".txtSearch"),a,(function(n){s.getDetail(n,(function(n){if(n.success){var a="",i="",r="";for(var c in null!=e&&e.remove(),n.data)"LAT_LON"==c&&n.data.hasOwnProperty(c)&&(a=n.data[c]);if(null!=a&&""!=a){var o=a.split(",");i=o[0],r=o[1],e=L.marker([1*i,1*r]).addTo(map).bindPopup(t.value),map.setView([1*i,1*r],12)}}}))}))}))})),"EDIT"==_pageInfo.Mode&&function(){try{$("#ddlSubDistrict").val(_pageInfo.EditModel.SubDistrict),null!=_pageInfo.EditModel.CaseLatitude&&""!=_pageInfo.EditModel.CaseLatitude&&null!=_pageInfo.EditModel.CaseLongtitude&&""!=_pageInfo.EditModel.CaseLongtitude&&L.marker([1*_pageInfo.EditModel.CaseLatitude,1*_pageInfo.EditModel.CaseLongtitude]).addTo(map),a.incident=_pageInfo.EditModel,$("#ddlCustomer").val(a.incident.FirstName).trigger("change")}catch(e){alert(e.message)}}()},test:function(){alert("xxxxx")},getCurrentDateTime:function(e){var t=new Date;return t.getDate()+"/"+t.getMonth()+"/"+t.getFullYear()+" "+(t.getHours()+e)+":"+t.getMinutes()},getCurrentDate:function(e){var t=new Date;return t.getDate()+"/"+t.getMonth()+"/"+t.getFullYear()},getData:function(e,t,n,a){var r={Module:e,ParentID:t};$.ajax({url:i+"api/Incident/GetData/",data:r,type:"POST",dataType:"json",error:function(e,t,n){a(e)},success:function(e){n(e)}})}};function c(){var e=1*$("#ddlIncidentType").val(),t=_incidentTypes.find((function(t){return t.ID==e}));$("#ddlIncidentCategory").html(""),$.each(t.Categories,(function(e,t){$("#ddlIncidentCategory").append($("<option>",{value:t.ID,text:t.Name}))})),o()}function o(){var e=1*$("#ddlIncidentType").val(),t=_incidentTypes.find((function(t){return t.ID==e})),n=1*$("#ddlIncidentCategory").val(),a=t.Categories.find((function(e){return e.ID==n}));$("#ddlIncidentTitle").html(""),$.each(a.Subjects,(function(e,t){$("#ddlIncidentTitle").append($("<option>",{value:t.ID,text:t.Name}))}))}var d={autoSearch:function(){var e=$("#ddlCustCode").val(),t=$("#ddlProvince").val();d.search(e,t,"","",null,null)},searcByClick:function(){var e=$("#ddlCustCode").val(),t=$("#ddlProviceSearch").val(),n=$("#txtAreaSearch").val(),a=($("#txtSartDateSearch").val(),$("#txtEndDateSearch").val()),i=$("#ddlStatusSearch").val();d.search(e,t,detail,n,i,a)},search:function(e,t,n,a,r,c){var o={CustomerNo:e,ProvinceID:t,Detail:n,Area:a,StartDate:r,EndDate:c};PwaManager.PwaWaiting(!0),$.ajax({url:i+"/api/Incident/SearchBranchIncidents",data:o,type:"POST",dataType:"json",error:function(e,t,n){alert(e.responseText)},success:function(e){$("#divResult").html("");var t='<table class="table table-striped table table-hover" data-animate="fade">';t+="    <thead>",t+="            <tr>",t+='                <th class="" scope="col">???????????????????????????????????????????????????</th>',t+='                <th class="" scope="col">???????????????????????????????????????</th>',t+='                <th class="" scope="col">??????????????????????????????</th>',t+="                        </thead>",t+="            <tbody>",null!=e.Result&&null!=e.Result.Incidents&&e.Result.Incidents.length>0&&$.each(e.Result.Incidents,(function(e,n){t+="                <tr>",t+="                    <td>"+n.ReceivedCaseDate+"</td>",t+="                    <td>"+n.PwaIncidentNo+"</td>",t+="                    <td>"+n.RequestCategory+"</td>",t+="                </tr>"})),t+="     </table>",$("#divResult").append(t),$("#divResult  table").DataTable({searching:!1,bLengthChange:!1,pageLength:5}),PwaManager.PwaWaiting(!1),null!=e.Result&&null!=e.Result.Incidents&&e.Result.Incidents.length>0&&$.each(e.Result.Incidents,(function(e,t){null!=t.CaseLatitude&&""!=t.CaseLatitude&&null!=t.CaseLongtitude&&""!=t.CaseLongtitude&&L.marker([1*t.CaseLatitude,1*t.CaseLongtitude]).addTo(map).bindPopup("<b>??????????????????????????????????????? :  ".concat(t.PwaIncidentNo,"</b><br /><b>?????????????????? :  ").concat(t.RequestCategory,"</b><br /><b>?????????????????????????????? :  ").concat(t.RequestCategorySubject,"</b>")).openPopup()}))}})}},s={token:"08a38faa15b0302ae1e415d18db8d78f3e3fabbd409c10e5de1ab15b8aa2f67573ad03bede273520",search:function(e,t){var n={keyword:e,key:s.token,maxResult:20};console.log(n),$.ajax({url:"https://geosearch.cdg.co.th/g/search/autocomplete",data:n,type:"POST",dataType:"json",error:function(e,t,n){console.log(e)},success:function(e){console.log(e),t(e)}})},getDetail:function(e,t){var n={locationid:e,key:s.token};$.ajax({url:"https://geosearch.cdg.co.th/g/search/details",data:n,type:"POST",dataType:"json",error:function(e,t,n){console.log(e)},success:function(e){console.log(e),t(e)}})}};$(document).ready((function(){console.log("document ready!");try{r.init()}catch(e){alert("error :"+e.message)}}))}});>>>>>>> .r420
