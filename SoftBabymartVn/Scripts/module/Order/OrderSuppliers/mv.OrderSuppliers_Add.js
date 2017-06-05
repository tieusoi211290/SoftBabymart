var Order = Order || {};
Order.mvOrderSuppliersAdd = function () {
    var self = this;
    self.ListProductSearch = ko.observableArray();
    self.mOrderSuppliers = ko.observable(new Order.mOrder);
    self.Keyword = ko.observable();
    self.Keyword.subscribe(function (val) {
        setTimeout(function () {
            self.Research_Product();
        }, 500);
    });
    self.Total = ko.computed(function () {
        var sum = 0;
        ko.utils.arrayForEach(self.mOrderSuppliers().Detail(), function (val) {
            sum += parseInt(val.Total());
        });
        self.mOrderSuppliers().Total(sum);
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
        var hasIt = ko.utils.arrayFirst(self.mOrderSuppliers().Detail(), function (o) {
            return val.Id() == o.ProductId();
        });
        if (hasIt != null)
            hasIt.Total(hasIt.Total() + 1);
        else {
            var newObj = new Order.mOrderDetail();
            newObj.ProductName(val.ProductName());
            newObj.ProductId(val.Id());
            newObj.Code(val.Code());
            self.mOrderSuppliers().Detail.push(newObj);
        }
    };
    self.mOrderSuppliers_Print = ko.observableArray();
    self.CreatOrderSuppliers = function () {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/Order_Suppliers/CreateOrder_Suppliers"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(self.mOrderSuppliers()),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                self.mOrderSuppliers(new Order.mOrder)
                self.mOrderSuppliers_Print([]);
            }
            Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.CreatOrder_PrintSuppliers = function () {
        Utils.showWait(true);
        self.mOrderSuppliers_Print([]);
        $.ajax({
            type: "POST",
            url: Utils.url("/Order_Suppliers/CreateOrder_Suppliers_Print"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(self.mOrderSuppliers()),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                ko.utils.arrayForEach(data.Data, function (item) {
                    var obj = ko.observable(new Order.mOrderPrint);
                    obj().suppliers(ko.mapping.fromJS(item.suppliers, {}, new Order.mSuppliers));
                    obj().products(Utils.MapArray(item.products, Order.mProductsPrint));
                    self.mOrderSuppliers_Print.push(obj());
                });
            }
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.Print = function (val) {
        var el = "printorder_" + val.suppliers().SuppliersId();
        var contents = document.getElementById(el).innerHTML;
        var frame1 = document.createElement('iframe');
        frame1.name = "frame1";
        frame1.style.position = "absolute";
        frame1.style.top = "-1000000px";
        document.body.appendChild(frame1);
        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
        frameDoc.document.open();
        frameDoc.document.write('<html><head><title>DIV Contents</title>');
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            document.body.removeChild(frame1);
        }, 500);
        return false;

    }

    self.RemoveItem = function (val) {
        self.mOrderSuppliers().Detail.remove(val)
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderSuppliersAddViewId'));
    };
    $("body").on("click", function () {
        self.ListProductSearch([]);
    });
};