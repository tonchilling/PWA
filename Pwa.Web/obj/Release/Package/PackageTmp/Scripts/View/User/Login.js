var mapApi = {
    getServerPath: function () {
        return '';
        // return '';
    }
}

function HideAlert() {
    window.setTimeout(function () {
        $(".alert").fadeOut(300)
    }, 3000);
}

function MyAlert(pass, msg) {
    $(".alert").text(msg);
    if (pass) {

        $(".alert").removeClass("alert-danger").addClass("alert-success");
    } else {
        $(".alert").removeClass("alert-success").addClass("alert-danger");
    }
    window.setTimeout(function () {
        $(".alert").fadeIn();
    }, 600);

    HideAlert();
    return false; // Keep close.bs.alert event from removing from DOM
}



$(document).ready(function () {
    Site.run();
    $("#btnLogin").on("click", function () {
        Waiting(true);
        var nonEmp = ($("#cb_nonEmp").is(":checked") ? "1" : "0");
        PageController.Login($('#txtUser').val(), $('#txtPassword').val(), nonEmp, function (data) {

            window.setTimeout(function () {
                Waiting(false);
            }, 1000);

            if (data != null) {
                if (data.Status == null) {
                    MyAlert(false, "Login Failed");
                }
                else if (data.Status == "Failed") {
                    MyAlert(false,"Login Failed");
                }
                else {
                    MyAlert(true, "Login Pass");

                    LinkUrl = LinkUrl.replace("Controller", data.MvcController).replace("Action", data.MvcAction);// '@Url.Action("' + data.MvcAction + '", "' + data.MvcController+'")';

                    $("#frmLogin").attr("action", LinkUrl)
                    window.setTimeout(function () {
                        $("#frmLogin").submit();
                    }, 1000);
                }
            } else {
                window.setTimeout(function () {
                    Waiting(false);
                }, 1000);
            

                MyAlert(false,"Login Failed");
            }
          
        });
        return false;
    });




    });


var PageController ={
    Login: function (user, pass, nonEmp, fnSuccess) {
        var criteria = {
            UserLogin: user,
            Password: pass,
            NonEmp: nonEmp
        }

        $.ajax({
            type: 'POST',
            url: VerifyUrl,
            data: criteria,
            cache: false,
            async: true,
            dataType: 'json',
            traditional: true,
            success: function (data) {

                fnSuccess(data);
                console.log(data);
             
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                fnSuccess({ Status: 'Failed', Message: errorThrown});
            }
        });

     
    },


}