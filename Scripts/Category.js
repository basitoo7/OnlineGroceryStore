﻿var Categories = Categories || {};

Categories.Structure = {

    productItemTemplate: {},
    productList: [],
    Customer: {},
    productListForDivOne: [],
    //if your products / items are more than 8,     than you need to add      productListForDivTwo: []
    productListForDivTwo: [],
    //if your products / items are more than 16,     than you need to add      productListForDivThree: []

    getProductsByCategory: function (categoryId) {
        //$.get('@Url.Action("WelcomeMsg","[controller]")', { input: name }, function (data) {
        //    Categories.Structure.productList = data;
        //    //$("#rData").html(data);
        //});
        $.ajax({
            
            dataType: "json",
            type: "GET",
            //url: '@Url.Action("getProductsByCategory", "Product")',
            url: "/Product/getProductsByCategory",
            data: { categoryId: categoryId },
                    
            contentType: 'application/json; charset=utf-8',
            cache: false,
            //data: {},
            success: function (response) {

                Categories.Structure.productList = response.Records;
                Categories.Structure.Customer.DBProductList = Categories.Structure.productList;
                localStorage.setItem("customer", JSON.stringify(Categories.Structure.Customer));
                var Customer = JSON.parse(localStorage.getItem("customer"));


                $.each(Categories.Structure.productList, function (key, value) {

                    var divContainingListOne = Categories.Structure.productItemTemplate.clone();
                    $(divContainingListOne).data("dataItem", value);
                    $(divContainingListOne).attr("id", value.Id);
                    //$(divContainingListOne).attr("class", "dynamicallyAddedDiv");

                    var category = "";
                    if (value.catagory_id == 1) {
                        category = "Baverages";
                        $("#divProducts").parent().find("h3").text("Products Page - " + category);
                    }

                    
                    $(divContainingListOne).addClass("col-md-3 top_brand_left dynamicallyAddedDiv");
                    
                    $(divContainingListOne).find("img.productImage").attr("src", value.image_path);
                    $(divContainingListOne).find("p.productName").text(value.name);
                    $(divContainingListOne).find("span.productPrice").text(value.unit_price);
                    
                    $("#divProducts").append(divContainingListOne);
                    $(".dynamicallyAddedDiv").show();
                });
                
            },
            error: function (xhr) {
                alert(xhr.responseText);
            }
        });
    },

    populateMiniCartDialogItems: function (dataSourceDiv) {

        var DivData = $(dataSourceDiv).data("dataItem");

        //var productId = $(dataSourceDiv).attr("id");
        var productId = DivData.id;
        var productImage = $(dataSourceDiv).find("img").attr("src");
        var productTitle = $(dataSourceDiv).find(".productName").text();
        //var productQtyText = $(dataSourceDiv).find(".quantity").val();
        //var productQtyValue = parseFloat(productQtyText);
        //var productUnitText = $(dataSourceDiv).find(".unitText").text();
        var productUnitPriceText = $(dataSourceDiv).find(".productPrice").text();;
        var productUnitPriceValue = parseFloat(productUnitPriceText);
        var productValueRounded = productUnitPriceValue.toFixed(2);
        var productQtyValue = 1;

        var trHTML = "";
        //trHTML += '<tr id="' + productId + '" class="dynamicRow"><td><a href="javascript:void(0);" class="yourorderclosebtn"><img alt="" src="images/closebtn.png" /></a><span class="yourorderimg"><img alt="" src="' + productImage + '" /></span><span class="yourorderproname>' + productTitle + '</span></td><td class="blankTD unitPrice">' + productUnitPriceValue + '</td><td class="qty blankTD"><input type="text" class="yourorderquantity txtQty qtySpinner" value="' + productQtyValue + '" maxlength="5" style="width: 70px" /></td><td class="specific rowTotal blankTD">' + productValue + '</td></tr>';
        //trHTML += '<tr id="' + productId + '" class="dynamicRow"><td><a href="javascript:void(0);" class="yourorderclosebtn"><img alt="" src="images/closebtn.png" class="mCS_img_loaded" /></a><span class="yourorderimg"><img class="mCS_img_loaded alt="" src="' + productImage + '" style="width: 40px; height: 40px;" /></span><span class="yourorderproname>' + productTitle + '</span></td><td class="blankTD unitPrice">' + productUnitPriceValue + '</td><td class="qty blankTD"><input type="text" class="yourorderquantity txtQty qtySpinner" value="' + productQtyValue + '" maxlength="5" style="width: 70px" /></td><td class="specific rowTotal blankTD">' + productValue + '</td></tr>';

        trHTML += '<tr id="' + productId + '" ><td style="width: 40px;"><a href="javascript:void(0);" class="yourorderclosebtn" ><img alt="" src="/images/closebtn.png" class="mCS_img_loaded removeProduct" /></a><span class="yourorderimg" ><img class="mCS_img_loaded alt="" src="' + productImage + '" style="width: 40px; height: 40px;" /></span><span class="yourorderproname"> ' + productTitle + ' </span></td><td class="unitPrice" >' + productUnitPriceValue + '</td><td class="qty" ><input type="text" class="yourorderquantity txtQty" value="' + productQtyValue + '" maxlength="3" style="width: 70px" /></td><td class="specific rowTotal">' + productValueRounded + '</td></tr>';

            
        $(".yourordertable tbody.tbodyRow").append(trHTML);
        //$(".yourordertable tbody.tbodyRow tr.trGrandTotal").before(trHTML);
        //$(".containerDivCart").show();
        $(".ui-dialog-titlebar-close img").remove();
        
        Categories.Structure.displayCartBox();
        $(".ui-dialog-titlebar-close").append('<img src="/images/closebtn.png" />');
        Categories.Structure.calculateOrderTotal();
        

    },

    displayCartBox: function(){
        $(".containerDivCart").dialog({
            modal: true,
            title: "",
            width: 800,
            height: 300,
            autoOpen: true,
            resizable: false,
            draggable: false,

        });
    },

    calculateOrderTotal: function () {
        var subTotal = 0;
        var tdData = [];
        tdData = $(".yourordertable td.rowTotal").toArray();
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

        $("div.divGrandTotal span.spnGrandTotal").text(subTotalRoundedValue);
    },

    //getCartProductList: function () {

    //}
}

$(document).ready(function () {
    try {

        Categories.Structure.productItemTemplate = $(".productItemTemplate").clone().removeClass("productItemTemplate");


    }
    catch (e) {

    }
});