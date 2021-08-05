var CheckOut = CheckOut || {};

CheckOut.Structure = {

    CheckOutProductList: [],
    cartProductsList: [],

    finalOrderedProductsList: [],

    customer: {},
    product: {},

    populateCartDetail: function () {
        var Customer = JSON.parse(localStorage.getItem("customer"));
        CheckOut.Structure.cartProductsList = Customer.CartProductList;
        
        var trHTML = "";
        $.each(CheckOut.Structure.cartProductsList, function (index, value) {
            var serialNo = index + 1;
            //trHTML = '<tr id="' + value.id + '" ><td style="text-align:center">' + serialNo + '</td><td class="invert-image" ><img src="' + value.image_path + '" style="width:40px;height:30px;" alt=" " class="img-responsive" /></td><td class="invert" ><input style="text-align: center;" class="tdQuantity" type="text" value="' + value.quantity + '" maxlength="3" /></td><td class="unitPrice">' + value.unit_price + '</td><td class="invert tdProductName">' + value.name + '</td><td class="invert tdTotalPrice">' + value.TotalPrice + '</td><td class="invert"><div class="rem"><div class="close1 removeProduct"></div></div></td></tr>';
            trHTML = '<tr id="' + value.id + '" ><td class="invert-image" ><img src="' + value.image_path + '" style="width:40px;height:30px;" alt=" " class="img-responsive" /></td><td class="invert" ><input style="text-align: center;" class="tdQuantity" type="text" value="' + value.quantity + '" maxlength="3" /></td><td class="unitPrice">' + value.unit_price + '</td><td class="invert tdProductName">' + value.name + '</td><td class="invert tdTotalPrice">' + value.TotalPrice + '</td><td class="invert"><div class="rem"><div class="close1 removeProduct"></div></div></td></tr>';

            $(".timetable_sub tbody.tableBody").append(trHTML);
            //$(".dynamicallyAddedDiv").show();
        });
        var count = $(".timetable_sub tbody.tableBody tr").length; 
        $(".productCount").text(count + " Products");
        CheckOut.Structure.calculateCustomerOrderTotal();
    },
    calculateCustomerOrderTotal: function () {
        var subTotal = 0;
        var tdData = [];
        tdData = $(".timetable_sub tbody.tableBody tr td.tdTotalPrice").toArray();
        for (var i = 0; i < tdData.length; i++) {
            if (tdData[i].innerHTML == "") {
                tdData[i].innerHTML = "0";
                subTotal += parseFloat(tdData[i].innerHTML);
            }
            else {
                subTotal += parseFloat(tdData[i].innerHTML);
            }

        }

        var subTotalRoundedValue = subTotal.toFixed(2)

        $("div.checkout-right span.orderTotal").text(subTotalRoundedValue);
    },


    //for validating email provided by the user
    validateEmail: function (email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{0,4})?$/;
        return emailReg.test(email);
    },

    saveCustomerOrder: function () {

        //var totalPrice = $("#orderTotal").text();
        var totalBill = $("div.checkout-right span.orderTotal").text();
        var name = $.trim($("#txtFullName").val());
        var email = $.trim($("#txtEmail").val());
        var phone = $.trim($("#txtMobile").val());
        var address = $("#txtAddress").val();
        
        if (totalBill.length <= 0 || totalBill == "" || totalBill == "0.00" || totalBill === "undefined") {
            alert("go back to home, select at least one product, than proceed to checkout");
            //$(".warning_dialogBox").css('z-index', 9999);
            return;
        }
        
        else if (name.length <= 0 || name == "" || name === "undefined") {
            alert("Full Name is required field");
            
            $("#txtName").focus();
            //$(".warning_dialogBox").css('z-index', 9999);
            return;
        }
        else if (email.length <= 0 || email === "undefined" || (email.length > 0 && !CheckOut.Structure.validateEmail(email))) {
            alert("valid email is required");
            
            $("#txtEmail").focus();
            //$("#txtEmail").addClass("failedValidation");
            //$(".warning_dialogBox").css('z-index', 9999);
            return;
        }
        else if (phone.length <= 0 || phone == "" || phone === "undefined") {
            alert("Phone is required field");
            
            $("#txtPhone").focus();
            //$("#txtPhone").addClass("failedValidation");
            //$(".warning_dialogBox").css('z-index', 9999);
            return;
        }

            
        else if (address.length <= 0 || address == "" || address === "undefined") {
            alert("Address is required field");
            
            return;
        }
        else {
            var orderTableTrs = [];
            //var cartProductList = [];
            var Customer = JSON.parse(localStorage.getItem("customer"));

            orderTableTrs = $("table.timetable_sub tbody.tableBody tr").toArray();
            for (var i = 0; i < orderTableTrs.length; i++) {
                //CheckOut.Structure.product.Id = parseInt($(orderTableTrs[i]).attr("id"));

                //CheckOut.Structure.finalOrderedProductsList.push(CheckOut.Structure.product);
                $.grep(Customer.DBProductList, function (item, index) {
                    //if (orderTableTrs[i].id == item.id) {
                    if (parseInt($(orderTableTrs[i]).attr("id")) == item.id) {
                        var Product = {};
                        //CheckOut.Structure.product.Id = item.id;
                        Product.Id = item.id;
                        Product.CategoryId = item.catagory_id;
                        Product.Name = item.name;
                        Product.IsDeleted = item.is_deleted;
                        Product.CreatedDate = item.create_date;
                        Product.UnitPrice = item.unit_price;
                        Product.Description = item.description;
                        Product.ImagePath = item.image_path;
                        Product.IsActive = item.is_active;
                        Product.UnitText = item.unit_text;
                        Product.Quantity = item.quantity;
                        Product.UnitId = item.unit_id;
                        Product.TotalPrice = item.TotalPrice;
                        //var priceText = $(orderTableTrs[i]).find("td.rowTotal").text();
                        //var priceValue = parseFloat(priceText);
                        //item.unit_price = priceValue.toFixed(2);
                        //item.TotalPrice = priceValue.toFixed(2);
                        //item.quantity = $(orderTableTrs[i]).find("td.qty").text();
                        //item.quantity = parseFloat($(orderTableTrs[i]).find("input.txtQty").val());

                        //Sitem.create_date = item.create_dateString;

                        //var dateString = item.create_dateString;
                        //updatedDateString = dateString.replace("/", "-");
                        //item.create_date = updatedDateString;
                        //item.create_dateString = updatedDateString;

                        //new Date(item.create_date);

                        //CheckOut.Structure.finalOrderedProductsList.push(CheckOut.Structure.product);
                        CheckOut.Structure.finalOrderedProductsList.push(Product);
                        //CheckOut.Structure.finalOrderedProductsList.push(item);
                        //for (var i = 0; i < CheckOut.Structure.finalOrderedProductsList.length; i++) {
                        //    var currentItemInList = CheckOut.Structure.finalOrderedProductsList[i];

                            //$.grep(Customer.DBProductList, function (item, index) {
                            //    if (orderTableTrs[i].id == item.id) {
                            //        //var priceText = $(orderTableTrs[i]).find("td.rowTotal").text();
                            //        //var priceValue = parseFloat(priceText);
                            //        //item.unit_price = priceValue.toFixed(2);
                            //        //item.TotalPrice = priceValue.toFixed(2);
                            //        //item.quantity = $(orderTableTrs[i]).find("td.qty").text();
                            //        //item.quantity = parseFloat($(orderTableTrs[i]).find("input.txtQty").val());
                            //        CheckOut.Structure.finalOrderedProductsList.push(item);

                            //    }
                            //});
                        //}
                    }
                });
            }
                
                //CheckOut.Structure.CheckOutProductList = cartProductList;
                //Categories.Structure.Customer.CartProductList = cartProductList;
                //localStorage.setItem("customer", JSON.stringify(Categories.Structure.Customer));
                //var Customer = JSON.parse(localStorage.getItem("customer"));
                //location.href = '@Url.Action("Index", "CheckOut")';

                //methodName = 'SaveCustomerOrder';
        }

        $.ajax({
            cache: false,
            async: true,
            data: {
                Name: name,
                Phone: phone,
                Email: email,
                TotalPrice: totalBill,
                Address: address,
                Products: JSON.stringify(CheckOut.Structure.finalOrderedProductsList).replace(/\/Date/g, "\\\/Date").replace(/\)\//g, "\)\\\/")
            },
            dataType: "json",
            type: "POST",
            url: "/Customer/SaveCustomerOrder",
            success: function (response) {
                //alert(response);
                if (response.Result == "SUCCESS") {

                    //Customer.Structure.customer.CustomerInfoId = response.Records.OrderMainId;
                    var strId = response.Records.OrderMainId.toString();
                    var intId = parseInt(strId);
                    CheckOut.Structure.customer.CustomerInfoId = intId;
                    CheckOut.Structure.customer.Name = name;
                    CheckOut.Structure.customer.Phone = phone;
                    CheckOut.Structure.customer.Email = email;
                    CheckOut.Structure.customer.OrderTotal = totalBill;
                    CheckOut.Structure.customer.TrackingNumber = response.Records.TrackingNumber;
                    CheckOut.Structure.customer.OrderStatus = response.Records.OrderMainStatus;
                    localStorage.setItem("customer", JSON.stringify(CheckOut.Structure.customer));

                    var Customer = JSON.parse(localStorage.getItem("customer"));
                    //location.href = '@Url.Action("Index", "Customer")';
                    //var myURL = $("#customerControllerURL").val();
                    var myURL = "/Customer/Index";
                    location.href = myURL;
                }
                else {
                    alert("something went wrong");
                }

            }
        });

    },
}

$(document).ready(function () {
    
    CheckOut.Structure.populateCartDetail();
});