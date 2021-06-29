var Incident = {
    init: () => {
        $(document).ready(function () {

            var mySwitch = new Switchery($('.js-switch'), {
                size: "small",
                color: '#0D74E9'
            });

            var elem = document.querySelector('.js-switch');
            var init = new Switchery(elem);

            $('.btnSearch').click(() => {
                Incident.onSearch();
            });

            $('.btnAdd').click(() => {
                Incident.onAdd();
            });

            

            $('.btnSave').click(() => {
                Incident.onSave();
            });
            /*
            $.fn.serializeFormJSON = function () {

                var o = {};
                var a = this.serializeArray();
                $.each(a, function () {
                    if (o[this.name]) {
                        if (!o[this.name].push) {
                            o[this.name] = [o[this.name]];
                        }
                        o[this.name].push(this.value || '');
                    } else {
                        o[this.name] = this.value || '';
                    }
                });
                return o;
            };*/

        });
    },
    onSearch: () => {

        Waiting(true);
        try {
            IncidentService.search(null, (data, textStatus, xhr) => {
                /*on success*/
                Waiting(false);
               
                if (data.Success) {
                    
                    var html = '';

                    $.each(data.Incidents, function (index, item) {

                        html += '<tr data-url="panel.tpl">';
                        html += '<td class="pre-cell"></td>';
                        html += '<td>';
                        html += '<span class="checkbox-custom checkbox-primary checkbox-lg">';
                        html += '<input type="checkbox" class="contacts-checkbox selectable-item" id="contacts_1" />';
                        html += '<label for="contacts_1"></label>';
                        html += '</span >';
                        html += '</td > ';


                        html += '<td>' + item.complainant_id + '</td>';
                        html += '<td>' + item.title + '</td>';
                        html += '<td>' + item.tel + '</td>';
                        html += '<td>' + item.email + '</td>';
                        html += '<td><button type="button" class="btn btn-floating btn-success btn-sm btnEdit"><i class="icon wb-pencil" aria-hidden="true"></i></button></td>';
                        html += '<td><button type="button" class="btn btn-floating btn-danger btn-sm btnDelete"><i class="icon wb-close" aria-hidden="true"></i></button></td>';
                        html += '</tr>';
                    });

                    $("#tbIncident > tbody").html(html);

                    $('.btnEdit').click(() => {
                        Incident.onEdit();
                    });

                    $('.btnDelete').click(() => {
                        Incident.onDelete();
                    });

                } else {
                    swal(
                        {
                            title: "Error!",
                            text: data.message,
                            timer: 800
                        });
                }

            },
            (xhr, textStatus, errorThrown) => {
                /*on error*/
                alert(xhr);
            });
        } catch (e) {
            alert(e.message);
        } finally {
            
        }

        
    },
    onAdd: () => {
        $("#InputForm").modal({ backdrop: 'static', keyboard: false });
    },
    onEdit: () => {
        

        $("#InputForm").modal({ backdrop: 'static', keyboard: false });
    },
    onDelete: () => {
        PwaConfirm("บันทึก", "คุณต้องการบันทึกหรือไม่", function (isConfirm) {
            if (isConfirm) {
                swal(
                    {
                        title: "สำเร็จ!",
                        text: "บันทึกข้อมูลเรียบร้อย!",
                        timer: 800
                    });
                
            } else {
                swal.close();
                $("#InputForm").modal({ backdrop: 'static', keyboard: false });
            }
        });
    },
    onSave: () => {

        var incInfo = {
            'complainant_id': '',
            'pwa_id': '',
            'meter_id': '',
            'title': $('#txtName').val(),
            'firstname': '',
            'lastname': '',
            'citizen_id': '',
            'tel': '',
            'address_no': '',
            'street': '',
            'sub_district': '',
            'district': '',
            'province': '',
            'postal_code': '',
            'email': ''

        };


        PwaConfirm("บันทึก", "คุณต้องการบันทึกหรือไม่", function (isConfirm) {
            if (isConfirm) {

                try {
                    IncidentService.add(incInfo, (data, textStatus, xhr) => {
                        swal(
                            {
                                title: "สำเร็จ!",
                                text: "บันทึกข้อมูลเรียบร้อย!",
                                timer: 800
                            });
                        /*on success*/
                        Incident.onSearch();
                        
                        
                    }, (xhr, textStatus, errorThrown) => {
                        /*on error*/
                        alert(xhr);
                    });
                } catch (e) {
                    alert(e.message);
                }


                

            } else {
                swal.close();
                
            }
        });
    }
    
}