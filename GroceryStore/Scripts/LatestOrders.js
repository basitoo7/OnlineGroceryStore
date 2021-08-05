var GS = GS || {};

GS.LatestOrder = {

    //productBoxItemTemplate: {},
    orderList: [],

    getAllOrder: function () {

        $.ajax({

            dataType: "json",
            type: "GET",
            //url: '@Url.Action("getProductsByCategory", "Product")',
            url: "/LastestOrder/GetLatestOrders",


            contentType: 'application/json; charset=utf-8',
            cache: false,
            //data: {},
            success: function (response) {

                GS.LatestOrder.orderList = response.Records;

                var trHTML = "";

                $.each(GS.LatestOrder.orderList, function (key, value) {

                    if (value.OrderStatus == 1) {
                        value.OrderStatus = "Accepted";
                    }
                    else if (value.OrderStatus == 2) {
                        value.OrderStatus = "InKitchen";
                    }
                    else if (value.OrderStatus == 3) {
                        value.OrderStatus = "CookingInProgress";
                    }
                    else if (value.OrderStatus == 4) {
                        value.OrderStatus = "OrderPacking";
                    }
                    else if (value.OrderStatus == 5) {
                        value.OrderStatus = "OrderDispatched";
                    }
                    else if (value.OrderStatus == 6) {
                        value.OrderStatus = "OrderDelivered";
                    }
                    else if (value.OrderStatus == 7) {
                        value.OrderStatus = "Cancelled";
                    }

                    trHTML = '<tr><td>' + value.CustomerName + '</td><td>' + value.CustomerPhone + '</td><td>' + value.CustomerEmail + '</td><td>' + value.OrderTotal + '</td><td>' + value.TrackingNumber + '</td><td class="tdOrderStatus">' + value.OrderStatus + '</td><td class="OrderMainID">' + value.OrderMainId + '</td><td class="btnOrderProgress"><input type=button class="btnAccepted" value="Accepted" /></td><td class="btnOrderProgress"><input type=button class="btnInKitchen" value="In Kitchen" /></td><td class="btnOrderProgress"><input type=button class="btnCookingInProgress" value="Cooking In Progress" /></td><td class="btnOrderProgress"><input type=button class="btnOrderPacking" value="Packing" /></td><td class="btnOrderProgress"><input type=button class="btnOrderDispatched" value="Dispatched" /></td><td class="btnOrderProgress"><input type=button class="btnOrderDelivered" value="Delivered" /></td><td class="btnOrderProgress"><input type=button class="btnOrderCancel" value="Cancel" /></td></tr>';
                    //trHTML = '<tr><td>' + value.Title + '</td><td>' + value.Qty + '</td><td>' + value.UnitText + '</td><td>' + value.UnitPrice + '</td><td>' + "Pending" + '</td><td>' + value.OrderMainId + '</td><td>' + GS.LatestOrderDetail.orderTrackingNumber + '</td></tr>';

                    $("#body").append(trHTML);
                    $("#tblOrderList, #tblOrderList thead, #tblOrderList tr, #tblOrderList td").css("border", "1px solid black");
                    //$("#trackrecord .container").show();
                    $(".container").show();


                    $("#body .tdOrderStatus").each(function () {
                        var orderStatus = $(this).text();
                        if (orderStatus == "InKitchen") {
                            $(this).siblings("td.btnOrderProgress").find("input.btnInKitchen").parent().prevAll("td.btnOrderProgress").find("input").attr("disabled", "disabled");
                        }
                        else if (orderStatus == "CookingInProgress") {
                            $(this).siblings("td.btnOrderProgress").find("input.btnCookingInProgress").parent().prevAll("td.btnOrderProgress").find("input").attr("disabled", "disabled");
                        }
                        else if (orderStatus == "OrderPacking") {
                            $(this).siblings("td.btnOrderProgress").find("input.btnOrderPacking").parent().prevAll("td.btnOrderProgress").find("input").attr("disabled", "disabled");
                        }
                        else if (orderStatus == "OrderDispatched") {
                            $(this).siblings("td.btnOrderProgress").find("input.btnOrderDispatched").parent().prevAll("td.btnOrderProgress").find("input").attr("disabled", "disabled");
                        }
                        else if (orderStatus == "OrderDelivered") {
                            $(this).siblings("td.btnOrderProgress").find("input.btnOrderDelivered").parent().prevAll("td.btnOrderProgress").find("input").attr("disabled", "disabled");
                        }
                        else if (orderStatus == "Cancelled") {
                            $(this).siblings("td.btnOrderProgress").find("input.btnOrderCancel").parent().prevAll("td.btnOrderProgress").find("input").attr("disabled", "disabled");
                        }


                    });

                });
            },
            error: function (xhr) {
                alert(xhr.responseText);
            }
        });




    },



};

$(document).ready(function () {
    try {
        //GS.LatestOrder.productBoxItemTemplate = $(".productTemplate").clone().removeClass("productTemplate");
        //GS.LatestOrder.productBoxItemTemplate = $(".productTemplate").clone().removeClass("productTemplate").addClass("odw_productbox");


    }
    catch (e) {

    }

});

