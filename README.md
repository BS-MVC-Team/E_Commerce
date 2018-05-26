# E_Commerce
```HTML
<script>

    $("#entertocar").on("click", function (event) {
        var id = $("#pid").text();
        console.log("id ="+ id)
        var size = $("#select_size").val();
        var color = $("#select_color").val();
        var price = $("#price").text();
        var totalQuality = $("#totalcount").val();
        var quality = $("#quality").val();
        if (quality == 0) {
            alert("很抱歉!該商品進貨中");
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Shopping/ShoppingCart1",
                data: {
                    productid: $("#pid").text(),
                    color: $("#select_color").val(),
                    size: $("#select_size").val(),
                    Quantity: $("#totalcount").val(),
                },
                success: function (result) {
                    location.href = '@Url.Action("ShoppingCart1","Shopping")';
                }
            });

        }
    });
    $(document).on("change", "#select_color", function (e) {
        console.log(e);
        quantity();
    })
    $(document).on("change", "#select_size", function (e) {
        console.log(e);
        quantity();
    })
    var quantity = function () {
        $("#quantry").text("");
        $.ajax({
            url: "/Home/Quantity",
            data: {
                color: $("#select_color").val(),
                size: $("#select_size").val(),
                productjson: $("#p").text(),
            },
            success: function (result) {
                $("#quantry").text(result);
            }
        });
    }
    $(function () {
        $("#quantry").text("");
        $.ajax({
            url: "/Home/Quantity",
            data: {
                color: $("#select_color").val(),
                size: $("#select_size").val(),
                productjson: $("#p").text(),
            },
            success: function (result) {
                $("#quantry").html(result);
            }
        });
    })
   
    

</script>
```
