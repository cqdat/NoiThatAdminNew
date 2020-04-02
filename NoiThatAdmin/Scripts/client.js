$(document).ready(function () {

        

    $(document).on("click", ".updaterole", function () {
        var roleid = $(this).attr("roleid");

        $("#hdRoleID").val(roleid);

        $.ajax({
            url: '/Users/GetUpdateRole',
            contentType: 'application/html; charset=utf-8',
            data: { roleid: roleid },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $("#listupdaterole").html(data);

                $('#UpdateRole').modal({ backdrop: 'static', keyboard: false });

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {

            }
        });

    });

    $(document).on("click", ".AddPermission", function () {

        var permissionid = $(this).attr("permissionid");

        var roleid = $("#hdRoleID").val();

        $.ajax({
            url: '/Users/SetNewPermission',
            contentType: 'application/html; charset=utf-8',
            data: { roleid: roleid, permissionid: permissionid },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $("#listupdaterole").html(data);

                //$('#UpdateRole').modal({ backdrop: 'static', keyboard: false });

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {

            }
        });

    });


    $(document).on("click", ".DeletePermission", function () {

        var permissionid = $(this).attr("permissionid");

        var roleid = $("#hdRoleID").val();

        $.ajax({
            url: '/Users/SetDeleteRole',
            contentType: 'application/html; charset=utf-8',
            data: { roleid: roleid, permissionid: permissionid },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $("#listupdaterole").html(data);

                //$('#UpdateRole').modal({ backdrop: 'static', keyboard: false });

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {

            }
        });

    });


    $('#UpdateRole').on('hidden.bs.modal', function (e) {
        location.reload();
    });

    $('#SetRole').on('hidden.bs.modal', function (e) {
        location.reload();
    });


    $(document).on("click", ".UpdateRoleUser", function () {

        var userid = $(this).attr("userid");

        $("#hdUserID").val(userid);

        $.ajax({
            url: '/Users/GetRoleUser',
            contentType: 'application/html; charset=utf-8',
            data: { userid: userid },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $(".listuserrole").html(data);

                $('#SetRole').modal({ backdrop: 'static', keyboard: false });

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {

            }
        });

    });

    $(document).on("click", ".DeleteRole", function () {

        var roleid = $(this).attr("roleid");

        var userid = $("#hdUserID").val();

        $.ajax({
            url: '/Users/SetDeleteRoleUser',
            contentType: 'application/html; charset=utf-8',
            data: { roleid: roleid, userid: userid },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $(".listuserrole").html(data);

                //$('#UpdateRole').modal({ backdrop: 'static', keyboard: false });

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {

            }
        });

    });


    $(document).on("click", ".AddRole", function () {

        var roleid = $(this).attr("roleid");

        var userid = $("#hdUserID").val();

        $.ajax({
            url: '/Users/SetNewRole',
            contentType: 'application/html; charset=utf-8',
            data: { roleid: roleid, userid: userid },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $(".listuserrole").html(data);

                //$('#UpdateRole').modal({ backdrop: 'static', keyboard: false });

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {

            }
        });

    });




});