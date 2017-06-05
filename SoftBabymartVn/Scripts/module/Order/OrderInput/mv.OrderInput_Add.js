var Order = Order || {};
Order.mvOrderInputAdd = function (Lst) {
    var self = this;
    self.ListProductSearch = ko.observableArray();
    self.mOrderInput = ko.observable(new Order.mOrder);
    if (Lst != undefined) {
        if (Lst != null && Lst.length > 0) {
            var data = JSON.parse(Lst);
            ko.utils.arrayForEach(data, function (o) {
                self.mOrderInput().Detail.push(ko.mapping.fromJS(o, {}, new Order.mOrderDetail));
            });
        }
    }
    self.Keyword = ko.observable();
    self.Keyword.subscribe(function (val) {
        setTimeout(function () {
            self.Research_Product();
        }, 500);
    });
    self.Total = ko.computed(function () {
        var sum = 0;
        ko.utils.arrayForEach(self.mOrderInput().Detail(), function (val) {
            sum += val.TotalMoney();
        });
        self.mOrderInput().Total(sum);
    });
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
        var hasIt = ko.utils.arrayFirst(self.mOrderInput().Detail(), function (o) {
            return val.Id() == o.ProductId();
        });
        if (hasIt != null)
            hasIt.Total(hasIt.Total() + 1);
        else {
            var newObj = new Order.mOrderDetail();
            newObj.ProductName(val.ProductName());
            newObj.ProductId(val.Id());
            newObj.Code(val.Code());
            self.mOrderInput().Detail.push(newObj);
        }
    };

    self.CreatOrderInput = function (isDone) {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/Order_Input/CreateOrder_Inside"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ model: self.mOrderInput(), isDone: isDone }),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                self.mOrderInput(new Order.mOrder)
            }
            Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.RemoveItem = function (val) {
        self.mOrderInput().Detail.remove(val)
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderInputAddViewId'));
    };
    $("body").on("click", function () {
        self.ListProductSearch([]);
    });
};