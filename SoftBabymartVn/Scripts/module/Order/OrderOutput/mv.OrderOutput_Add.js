var Order = Order || {};
Order.mvOrderOutputAdd = function () {
    var self = this;
    self.ListProductSearch = ko.observableArray();
    self.lstOrder_Output = ko.observableArray();
    self.mOrderOutput = ko.observable(new Order.mOrder);
    self.Keyword = ko.observable();
    self.Keyword.subscribe(function (val) {
        setTimeout(function () {
            self.Research_Product();
        }, 500);
    });
    self.Total = ko.computed(function () {
        var sum = 0;
        ko.utils.arrayForEach(self.mOrderOutput().Detail(), function (val) {
            sum += parseInt(val.Total());
        });
        self.mOrderOutput().Total(sum);
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
        var hasIt = ko.utils.arrayFirst(self.mOrderOutput().Detail(), function (o) {
            return val.Id() == o.ProductId();
        });
        if (hasIt != null)
            hasIt.Total(hasIt.Total() + 1);
        else {
            var newObj = new Order.mOrderDetail();
            newObj.ProductName(val.ProductName());
            newObj.ProductId(val.Id());
            newObj.Code(val.Code());
            self.mOrderOutput().Detail.push(newObj);
        }
    };
    self.CreatOrderOutput = function () {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/Order_Output/CreateOrder_Output"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(self.mOrderOutput()),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                self.mOrderOutput(new Order.mOrder)
            }
            Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.LstBranches = ko.observableArray();
    Utils.LoadLstBranches(function (data) {
        //var rs = ko.observableArray();
        //ko.utils.arrayForEach(data, function (v) {
        //    if (v.BranchesId != parseInt(currentBranchId))
        //        rs.push(v);
        //})
        self.LstBranches(data);
    });
    self.RemoveItem = function (val) {
        self.mOrderOutput().Detail.remove(val)
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderOutputAddViewId'));
    };
    $("body").on("click", function () {
        self.ListProductSearch([]);
    });
};