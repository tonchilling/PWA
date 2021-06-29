var mapApi = {
    getServerPath: function () {
        return '';
        // return '';
    }
}


function hideAlert() {
    window.setTimeout(function () {
        $(".alert").fadeOut(300)
    }, 3000);
} 

function toggleAlert(pass, msg) {
    $(".alert").text(msg);
    if (pass) {
        $(".alert").removeClass("alert-danger").addClass("alert-success");
    } else {
        $(".alert").removeClass("alert-success").addClass("alert-danger");
    }
    $(".alert").fadeIn();
    hideAlert();
    return false; // Keep close.bs.alert event from removing from DOM
}

$(document).ready(function () {
    Site.run();
    $("#btnLogin").on("click", function () {

        PageController.Login($('#txtUser').val(), $('#txtPassword').val(), function (data) {

            if (data != null) {
                if (data.Status == "Failed") {
                    toggleAlert(false,"Login Failed");
                }
                else {
                    toggleAlert(false,"Login Pass");
                }
            } else {
                toggleAlert(false,"Login Failed");
            }
          
        });
        return false;
    });




    });


var PageController ={
    Login: function (user, pass, fnSuccess) {
        var criteria = {
            UserLogin: user,
            Password: pass
        }

        $.ajax({
            type: 'POST',
            url: mapApi.getServerPath() + "/User/VerifyLogin",
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