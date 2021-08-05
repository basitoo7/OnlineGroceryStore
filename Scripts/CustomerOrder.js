var CustomerOrder = CustomerOrder || {};

CustomerOrder.Structure = {

    orderDetailList: {},

    getorderdetailbycustomer: function (customerInfoId) {
        

        $.ajax({
            cache: false,
            async: true,
            //data: {
            //    CustomerInfoId: customerInfoId,
            //    method: 'GetOrderDetailByCustomer'

            //},
            dataType: "json",
            type: "POST",
            url: "/Customer/GetOrderDetailByCustomer",
            data: { CustomerInfoId: customerInfoId },
            success: function (response) {
                //alert(response);
                CustomerOrder.Structure.orderDetailList = response.Records;
                
                var trHTML = "";

                $.each(CustomerOrder.Structure.orderDetailList, function (key, value) {

                    if (value.OrderStatus == 1) {
                        value.OrderStatus = "Accepted";
                    }
                    else if (value.OrderStatus == 2) {
                        value.OrderStatus = "OrderPacking";
                    }
                    else if (value.OrderStatus == 3) {
                        value.OrderStatus = "OrderDispatched";
                    }
                    else if (value.OrderStatus == 4) {
                        value.OrderStatus = "OrderDelivered";
                    }
                    else if (value.OrderStatus == 5) {
                        value.OrderStatus = "OrderCancelled";
                    }
                    
                    var productTotal = value.Qty * value.UnitPrice;

                    var productTotalRounded = productTotal.toFixed(2);

                    trHTML = '<tr><td>' + value.name + '</td><td>' + value.Qty + '</td><td>' + value.UnitPrice + '</td><td>' + value.unit_text + '</td><td>' + productTotalRounded + '</td></tr>';
                    //trHTML = '<tr><td>' + value.Title + '</td><td>' + value.Qty + '</td><td>' + value.UnitText + '</td><td>' + value.UnitPrice + '</td><td>' + value.OrderStatus + '</td><td>' + value.OrderMainId + '</td></tr>';
                    //trHTML = '<tr><td>' + value.Title + '</td><td>' + value.Qty + '</td><td>' + value.UnitText + '</td><td>' + value.UnitPrice + '</td><td>' + "Pending" + '</td><td>' + value.OrderMainId + '</td><td>' + BK.OrderDetail.orderTrackingNumber + '</td></tr>';

                    //$("#body").append(trHTML);
                    $("#tblCustomerDetail tbody").append(trHTML);

                });


            }
        });
    },

}

$(document).ready(function () {

    //var Customer = JSON.parse(localStorage.getItem("customer"));

    //CustomerOrder.Structure.getorderdetailbycustomer(Customer.CustomerInfoId);
});