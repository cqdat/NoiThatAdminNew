$(document).ready(function () {

    $(document).on("click", ".UpdatePassword", function() {

        var userid = $(this).attr("userid");

        $("#hdUserIDEdit").val(userid);

        $('#ChangePassword').modal({ backdrop: 'static', keyboard: false });

    });


    $(document).on("change", "#ParentSelect", function () {

        var cateid = $(this).children("option:selected").val();


        if (cateid > 0) {
            $.ajax({
                url: '/CategoryImage/GetChildCategory',
                contentType: 'application/json; charset=utf-8',
                data: { cateid: cateid },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {


                    $("#ChildSelect").empty();

                    var html = "<option value=0>-- Chọn loại sản phẩm con --</option>"

                    $.each(data, function (id, dt) {
                  
                        html += "<option value=" + dt.CateID + ">" + dt.CateName + "</option>";
                      
                    });


                    $("#ChildSelect").html(html);

                    $("#parentid").val(cateid);

                    $("#FormUpload").hide();


                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {

                }
            });
        }
        else {
            $("#ChildSelect").empty();

            var html = "<option value=0>-- Chọn loại sản phẩm con --</option>"

            $("#ChildSelect").html(html);

            $("#FormUpload").hide();
        }


    });

    $(document).on("change", "#ChildSelect", function () {

        var cateid = $(this).children("option:selected").val();

        if (cateid > 0) {
            $("#childid").val(cateid);
            $("#FormUpload").show();

        }
        else {
            $("#FormUpload").hide();
        }


    });


    $(document).on("click", "#btnxoagroup", function () {

        var groupid = $(this).attr("groupid");

        var conf = confirm("Bạn có chắc muốn xóa !");

        if (conf) {
            $.ajax({
                url: '/products/XoaGroupProduct',
                contentType: 'application/html; charset=utf-8',
                data: { groupid: groupid },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {


                    if (data == "DONE") {

                        location.reload();
                    }
                    else {
                        alert("Có lỗi khi ghi data lên hệ thống ! Vui lòng thử lại !");
                    }
                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {

                }
            });
        }

    });

    $(document).on("click", "#btnSaveGroupProduct", function () {

        var groupcode = $(this).attr("groupcode");

        var groupid = $(this).attr("groupid");

        var xaction = $(this).attr("xaction");

        var productid = $("#hdProductIDDetail").val().trim();


        $.ajax({
            url: '/products/SaveGroupProduct',
            contentType: 'application/html; charset=utf-8',
            data: { productid: productid, groupcode: groupcode, groupid: groupid, xaction: xaction },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $("#groupcheck").html(data);


                $('#AddGroupProduct').modal({ backdrop: 'static', keyboard: false });

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");
            },
            complete: function (xhr, status) {

            }
        });

    });

    $(document).on("click", "#btnAddGroupProduct", function () {

        var hdProductIDDetail = $("#hdProductIDDetail").val().trim();

        $.ajax({
            url: '/products/GetGroupProduct',
            contentType: 'application/html; charset=utf-8',
            data: { productid: hdProductIDDetail },
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $("#groupcheck").html(data);


                $('#AddGroupProduct').modal({ backdrop: 'static', keyboard: false });
               
            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");
            },
            complete: function (xhr, status) {

            }
        });


        

    });

    $(document).on("click", ".LockAccount", function() {

        var userid = $(this).attr("userid");

        var xaction = parseInt($(this).attr("action"));

        var conf;

        if (xaction == 1) {
            conf = confirm("Bạn có chắc muốn khóa tài khoản này !");
        }
        else {
            conf = confirm("Bạn có chắc muốn mở khóa tài khoản này !");
        }        

        if (conf) {
            $.ajax({
                url: '/users/LockOrUnlockAccount',
                contentType: 'application/html; charset=utf-8',
                data: { userid: userid, xaction: xaction},
                type: 'GET',
                dataType: 'json'
                , success: function(data) {


                    if (data == "DONE") {

                        location.reload();
                    }
                    else {
                        alert("Có lỗi khi ghi data lên hệ thống ! Vui lòng thử lại !");
                    }
                },
                error: function(xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function(xhr, status) {

                }
            });
        }

    });

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

    $(document).on("click", "#btnUploadImage", function() {
        var productid = $(this).attr("productid");

        //alert("sdsad");

        $("#txtProductIDUpload").val(productid);

        $('#UploadImageData').modal({ backdrop: 'static', keyboard: false });

    });

    $('#tabHinhAnh').on('shown.bs.tab', function (e) {
        //var target = $(e.target).attr("href") // activated tab
        //alert(target);

        var productid = $("#hdProductIDDetail").val().trim();

        $.ajax({
            url: '/products/GetImageforProduct',
            contentType: 'application/html; charset=utf-8',
            data: { productid: productid},
            type: 'GET',
            dataType: 'html'
            , success: function (data) {

                $("#uploadhinhanh").html(data);


            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {

            }
        });

    });


    $('#UploadImageData').on('hidden.bs.modal', function () {
        location.reload();
    });

    $(document).on("click", ".edittitleimage", function () {

        var imageid = $(this).attr("imageid");
        var title = $(this).parent().parent().find("td.imgtitle").text().trim();
        $("#txtTitleEdit").val(title);
        $("#hdImageID").val(imageid);
        $('#EditTitle').modal({ backdrop: 'static', keyboard: false });
    });


    $(document).on("click", ".deleteimage", function () {

        var imageid = $(this).attr("imageid");

       
        var conf = confirm("Bạn có chắc muốn xóa hình ảnh này !");
        

        if (conf) {
            $.ajax({
                url: '/products/DeleteProductImage',
                contentType: 'application/html; charset=utf-8',
                data: { imageid: imageid },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {


                    if (data == "DONE") {

                        location.reload();
                    }
                    else {
                        alert("Có lỗi khi ghi data lên hệ thống ! Vui lòng thử lại !");
                    }
                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {

                }
            });
        }

    });

    $(document).on("click", ".changeavatar", function () {

        var imageid = $(this).attr("imageid");


        var conf = confirm("Bạn có chắc muốn chọn hình ảnh này làm đại diện !");


        if (conf) {
            $.ajax({
                url: '/products/ChangeProductAvatar',
                contentType: 'application/html; charset=utf-8',
                data: { imageid: imageid },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {


                    if (data == "DONE") {

                        location.reload();
                    }
                    else {
                        alert("Có lỗi khi ghi data lên hệ thống ! Vui lòng thử lại !");
                    }
                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {

                }
            });
        }

    });
    $(document).on("click", "#btnaddnewslide", function () {

        $('#NewSlide').modal({ backdrop: 'static', keyboard: false });

    });

    $(document).on("click", "#btnaddnewslide2", function () {

        $('#NewSlideCate').modal({ backdrop: 'static', keyboard: false });

    });

    $(document).on("click", "#btnaddnewslide3", function () {

        $('#NewSlide3').modal({ backdrop: 'static', keyboard: false });

    });


    $(document).on("click", "#btnAddSlide", function () {


        if (window.FormData !== undefined) {

      
            var fileUpload = $("#FileUpload").get(0);

            if (fileUpload.files.length === 0) {
                alert("Vui lòng chọn file upload !");
            }
            else {

                var title = $("#txtTitleSlide").val().trim();
                var slogan1 = $("#txtSlogan1").val().trim();
                var slogan2 = $("#txtSlogan2").val().trim();
                var link = $("#txtLinkBanner").val().trim();
                var target = $("#slTargetLink").children("option:selected").val().trim();
                var sort = $("#txtSort").val().trim(); 

                if (title == "" || slogan1 == "" || slogan2 == "" || link == "" || sort == "") {
                    alert("Vui lòng điền đầy đủ các trường phía trên");
                }
                else {

                    var files = fileUpload.files;

                    var fileData = new FormData();

                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                    }

                    fileData.append('username', 'tanthoi');

                    //loadingstart();

                    $.ajax({
                        url: '/slides/UploadImage',
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fileData,
                        success: function (result) {

                            if (result != "FAIL") {

                                $.ajax({
                                    url: '/slides/createslide',
                                    contentType: 'application/html; charset=utf-8',
                                    data: { urlimg: result, target: target, sort: sort, link: link, slogan2: slogan2, slogan1: slogan1, title: title},
                                    type: 'GET',
                                    dataType: 'json'
                                    , success: function (data) {

                                        if (data == "DONE") {
                                            location.reload();
                                        }
                                        else {
                                            alert("Có lỗi trong quá trình save data !");
                                        }

                                    },
                                    error: function (xhr, status) {
                                        alert("Fail connect to system server. Please try again or check internet connection.");
                                    },
                                    complete: function (xhr, status) {
                                        //loadingstop();
                                    }
                                });

                            }
                        },
                        error: function (err) {
                            alert(err.statusText);
                        }
                    });

                }
            }

            
        } else {
            alert("FormData is not supported.");
        }


    });



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    $(document).on("click", "#btnAddSlideCate", function () {


        if (window.FormData !== undefined) {


            var fileUpload = $("#FileUpload").get(0);

            if (fileUpload.files.length === 0) {
                alert("Vui lòng chọn file upload !");
            }
            else {

                var title = $("#txtTitleSlide").val().trim();
                var slogan1 = $("#txtSlogan1").val().trim();
                var slogan2 = $("#txtSlogan2").val().trim();
                var link = $("#txtLinkBanner").val().trim();
                var target = $("#slTargetLink").children("option:selected").val().trim();
                var sort = $("#txtSort").val().trim();
                var category = $("#slCategorySlide").children("option:selected").val().trim();

                if (title == "" || slogan1 == "" || slogan2 == "" || link == "" || sort == "" || category == 0) {
                    alert("Vui lòng điền đầy đủ các trường phía trên !");
                }
                else {

                    var files = fileUpload.files;

                    var fileData = new FormData();

                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                    }

                    fileData.append('username', 'tanthoi');

                    //loadingstart();

                    $.ajax({
                        url: '/slides/UploadImage',
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fileData,
                        success: function (result) {

                            if (result != "FAIL") {

                                $.ajax({
                                    url: '/slides/createslide2',
                                    contentType: 'application/html; charset=utf-8',
                                    data: { urlimg: result, target: target, sort: sort, link: link, slogan2: slogan2, slogan1: slogan1, title: title, category: category },
                                    type: 'GET',
                                    dataType: 'json'
                                    , success: function (data) {

                                        if (data == "DONE") {
                                            location.reload();
                                        }
                                        else {
                                            alert("Có lỗi trong quá trình save data !");
                                        }

                                    },
                                    error: function (xhr, status) {
                                        alert("Fail connect to system server. Please try again or check internet connection.");
                                    },
                                    complete: function (xhr, status) {
                                        //loadingstop();
                                    }
                                });

                            }
                        },
                        error: function (err) {
                            alert(err.statusText);
                        }
                    });

                }
            }


        } else {
            alert("FormData is not supported.");
        }


    });


    ///////////////////////////////////////////////////////////////////////////////////////////////

    $(document).on("click", "#btnAddSlide3", function () {


        if (window.FormData !== undefined) {


            var fileUpload = $("#FileUpload").get(0);

            if (fileUpload.files.length === 0) {
                alert("Vui lòng chọn file upload !");
            }
            else {

                var title = $("#txtTitleSlide").val().trim();
                var slogan1 = $("#txtSlogan1").val().trim();
                var slogan2 = $("#txtSlogan2").val().trim();
                var link = $("#txtLinkBanner").val().trim();
                var target = $("#slTargetLink").children("option:selected").val().trim();
                var sort = $("#txtSort").val().trim();

                if (title == "" || slogan1 == "" || slogan2 == "" || link == "" || sort == "") {
                    alert("Vui lòng điền đầy đủ các trường phía trên");
                }
                else {

                    var files = fileUpload.files;

                    var fileData = new FormData();

                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                    }

                    fileData.append('username', 'tanthoi');

                    //loadingstart();

                    $.ajax({
                        url: '/slides/UploadImage',
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fileData,
                        success: function (result) {

                            if (result != "FAIL") {

                                $.ajax({
                                    url: '/slides/createslide3',
                                    contentType: 'application/html; charset=utf-8',
                                    data: { urlimg: result, target: target, sort: sort, link: link, slogan2: slogan2, slogan1: slogan1, title: title },
                                    type: 'GET',
                                    dataType: 'json'
                                    , success: function (data) {

                                        if (data == "DONE") {
                                            location.reload();
                                        }
                                        else {
                                            alert("Có lỗi trong quá trình save data !");
                                        }

                                    },
                                    error: function (xhr, status) {
                                        alert("Fail connect to system server. Please try again or check internet connection.");
                                    },
                                    complete: function (xhr, status) {
                                        //loadingstop();
                                    }
                                });

                            }
                        },
                        error: function (err) {
                            alert(err.statusText);
                        }
                    });

                }
            }


        } else {
            alert("FormData is not supported.");
        }


    });

});