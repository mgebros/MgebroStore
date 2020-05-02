// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).ready(function () {

    var prods = [];


    function addToCart(prod, prodCount) {

        if (prodCount < 1) return;

        var found = prods.find(p => p.id == prod.value);

        if (found) {
            found.count += parseInt(prodCount);
        } else {
            prods.push({
                id: prod.value,
                name: prod.options[prod.selectedIndex].text,
                count: parseInt(prodCount),
            });
        }

        refreshCart();
    }



    function removeFromCart(prodId) {
        prods = prods.filter(p => p.id != prodId);

        refreshCart();
    }



    function refreshCart() {
        if (prods.length < 1) {
            $("#cart").html("ცარიელი");
            return;
        }

        var htmlText = "";

        var ids = [];
        var quantity = [];


        for (var i = 0; i < prods.length; i++) {
            ids.push(prods[i].id);
            quantity.push(prods[i].count);



            htmlText +=
                `
<div class="row">
                <div class="col-md-4">
                    <b>` + prods[i].name + `</b>
                </div>
                <div class="col-md-2">
                    <b>` + prods[i].count + `</b>
                </div>
                <div class="col-md-2">
                    <input type="button" value="ამოგდება" class="form-control btn btn-danger" onclick="removeFromCart(` + prods[i].id + `)" />
                </div>

            </div>

            <br /><br />`;
        }


        htmlText += '<input type="hidden" name="ProductIDsText" value="' + ids + '">';
        htmlText += '<input type="hidden" name="ProductQuantityText" value="' + quantity + '">';

        $("#cart").html(htmlText);

    }

//});

