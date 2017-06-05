var Order = Order || {};
Order.mvOrderSale = function () {
    var self = this;
    self.ListProductSearch = ko.observableArray();
    self.lstOrder_Output = ko.observableArray();
    self.IsHasCustomer = ko.observable(false);
    self.mOrderSale = ko.observable(new Order.mOrder);
    self.Keyword = ko.observable();
    self.KeywordCustomer = ko.observable();
    self.KeywordCustomer.subscribe(function (val) {
        setTimeout(function () {
            self.Research_Customer(val);
        }, 500);
    });
    self.Keyword.subscribe(function (val) {
        setTimeout(function () {
            self.Research_Product();
        }, 500);
    });
    self.Total = ko.computed(function () {
        var sum = 0;
        ko.utils.arrayForEach(self.mOrderSale().Detail(), function (val) {
            sum += parseInt(val.Total());
        });
        self.mOrderSale().Total(sum);
    });
    self.TotalMoenySumary = ko.computed(function () {
        var sum = 0;
        ko.utils.arrayForEach(self.mOrderSale().Detail(), function (val) {
            sum += parseInt(val.TotalMoney());
        });
        self.mOrderSale().TotalMoney(sum);
    });
    self.CustommerMoneyTake = ko.computed(function () {
        return self.mOrderSale().TotalMoney();
    });
    self.CustommerofMoney = ko.observable(0);
    self.CustommerMoneyGive = ko.computed(function () {
        return self.CustommerofMoney() - self.CustommerMoneyTake();
    });
    self.Customer = ko.observable();
    self.Research_Customer = function (val) {
        Utils.showWait(true);
        self.ListProductSearch([]);
        $.ajax({
            type: "POST",
            url: Utils.url("/Customer/Research"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ keyword: val }),
        }).done(function (data) {
            if (data == null) {
                self.IsHasCustomer(false);
                return;
            }
            if (!data.isError && data.Data != null) {
                self.mOrderSale().Customer(ko.mapping.fromJS(data.Data, {}, new Order.mCustomer));
                self.IsHasCustomer(true);
            } else {
                self.IsHasCustomer(false);
                Utils.notify("Thông báo", data.messaging, 'error');
            }
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.Research_Product = function () {
        Utils.showWait(true);
        self.ListProductSearch([]);
        $.ajax({
            type: "POST",
            url: Utils.url("/Product/Research"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ keyword: self.Keyword() }),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                self.ListProductSearch(Utils.MapArray(data.Data, Order.mProduct));
            } else
                Utils.notify("Thông báo", data.messaging, 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    }
    self.GetProduct = function (val) {
        self.ListProductSearch([]);
        var hasIt = ko.utils.arrayFirst(self.mOrderSale().Detail(), function (o) {
            return val.Id() == o.ProductId();
        });
        if (hasIt != null)
            hasIt.Total(hasIt.Total() + 1);
        else {
            var newObj = new Order.mOrderDetail();
            newObj.ProductName(val.ProductName());
            newObj.ProductId(val.Id());
            newObj.Code(val.Code());
            newObj.Price(val.PriceBase());
            self.mOrderSale().Detail.push(newObj);
        }
    };
    self.CreatOrderSale = function (isDone) {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/OrderChannel/CreatOrderSale"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ model: self.mOrderSale(), isDone: isDone }),
        }).done(function (data) {
           if (data == null)
                return
            if (!data.isError) {
                self.mOrderSale(new Order.mOrder);
                self.KeywordCustomer(undefined)
                self.mOrderSale().Customer(new Order.mCustomer);
            }
            Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };

    self.RemoveItem = function (val) {
        self.mOrderSale().Detail.remove(val)
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderSaleViewId'));
    };


    $("body").on("click", function () {
        self.ListProductSearch([]);
    });
};