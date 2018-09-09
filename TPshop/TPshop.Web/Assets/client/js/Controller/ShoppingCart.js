var cart = {
    init: function () {
        cart.loadData();
        cart.loadCheckout();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('.delete').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.plus').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.plusItem(productId);
        });
        $('.minus').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.minusItem(productId);
        });
        $('#order-submit').off('click').on('click', function (e) {
            e.preventDefault();
            cart.createOrder();
        });
        $('#fillData').off('click').on('click', function () {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtPhone').val('');
            }
        });
    },

    getTotalOrder: function () {
        var listQuantity = $('span.quantityP');
        var total = 0;
        $.each(listQuantity, function (i, item) {
            var quantity = parseInt($(item).data('value'));
            var price = parseFloat($(item).data('price'));
            total += quantity * price;
        });
        return total;
    },

    deleteItem: function (productId, callback) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    cart.loadCheckout();
                }
                else {
                    alert(response.message);
                }
            }
        });
    },

    deleteAllItem: function (productId, callback) {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    cart.loadCheckout();
                }
                else {
                    alert(response.message);
                }
            }
        });
    },

    plusItem: function (productId, callback) {
        $.ajax({
            url: '/ShoppingCart/PlusItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    cart.loadCheckout();
                }
                else {
                    alert(response.message);
                }
            }
        });
    },

    minusItem: function (productId, callback) {
        $.ajax({
            url: '/ShoppingCart/MinusItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    cart.loadCheckout();
                }
                else {
                    alert(response.message);
                }
            }
        });
    },

    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'Get',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    var qty = 0;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            Quantity: item.Quantity,
                            Qty: qty += item.Quantity
                        });
                    });
                    $(".quantity").text(qty);
                    $('#cartBody').html(html);
                    if (html == '') {
                        $('#cartBody').html('There are no product.');
                    }
                    $('.totalPrice').text(numeral(cart.getTotalOrder()).format('0,0.00'));
                    cart.registerEvent();
                }
            }
        })
    },

    loadCheckout: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'Get',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('.tplCheckout').html();
                    var html = '';
                    var data = res.data;
                    var qty = 0;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            Quantity: item.Quantity,
                            Qty: qty += item.Quantity
                        });
                    });
                    $('.checkOutBody').html(html);
                    if (html == '') {
                        $('.checkOutBody').html('There are no product.');
                        $('#order-submit').click(function () { return false; });
                    }
                    cart.registerEvent();
                }
            }
        })
    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var user = res.data
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        })
    },
    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerPhone: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            Status: null,
            PaymentMethod: $("input[name='payment']:checked").val(),
            PaymentStatus: 'Not paid'
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (response) {
                if (response.status) {
                    cart.deleteAllItem();
                    $('#txtName').val('');
                    $('#txtAddress').val('');
                    $('#txtPhone').val('');
                    $('#txtMessage').val('');
                    setTimeout(function () {
                        $('#messageCheckout').css("display", "block");
                    }, 2000);
                }
            }
        });
    }
}
cart.init();