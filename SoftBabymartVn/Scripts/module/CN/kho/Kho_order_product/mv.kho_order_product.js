KHO_Order_Product.mvKHO_Order_Product = function () {
    var self = this;
    var IsNewRecord = false;
    self.listProductSearch = ko.observableArray([]);
    self.txtStringQuery = ko.observable();
    self.SearchQstr = ko.computed(function () {
        if (self.txtStringQuery() && self.txtStringQuery().length > 0) {
            Utils.showWait(true);
            $.ajax({
                type: "GET",
                url: "/KHO_OrderProduct/Search",
                data: { query: self.txtStringQuery() }
            }).done(function (data) {
                self.listProductSearch(ko.utils.arrayMap(data, function (item) {
                    return new typeModelSearch(item.Id, item.masp, item.tensp);
                }));
            }).always(Utils.showWait(false));
        }
    }).extend({ throttle: 400 });
    self.ListProduct_Order = ko.observableArray([]);
    self.txtDayRequest = ko.observable();
    self.txtDayOrder = ko.observable();
    self.mOrderProduct = ko.observable(new KHO_Order_Product.mKHO_Order);
    self.txtDayOrder.subscribe(function (val) {
        ko.utils.arrayForEach(self.mOrderProduct().soft_KHO_Order_Child(), function (item) {
            item.slDat(item.slTBban() * parseInt(val));
        });
    });
    self.CreateOrder = function () {
        self.ListProduct_Order([]);
        if (self.txtDayRequest() == undefined || self.txtDayRequest() <= 0) {
            Utils.notify("Thông báo", "Chưa nhập số ngày cần kiểm tra đặt hàng.", 'error')
            return;
        }
        $.ajax({
            type: "GET",
            url: "/KHO_OrderProduct/CreateOrder",
            data: { dayOrder: self.txtDayRequest() }
        }).done(function (data) {
            self.mOrderProduct().soft_KHO_Order_Child(Utils.MapArray(data, KHO_Order_Product.mKHO_Order_Product));
            self.txtDayOrder(5);
        }).always(Utils.showWait(false));
    }
    self.OrderProduct = function () {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: "/KHO_OrderProduct/AddOrderProduct",
            data: ko.toJSON({ model: self.mOrderProduct() }),
        }).done(function (data) {
            Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
        }).always(Utils.showWait(false));
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('kho_Order_ProductViewId'));

    };
    self.IsPrintOrdering = ko.observable(false);
    self.listPrintOrder = ko.observableArray();
    self.PrintOrder = function () {

        var group_to_values = self.mOrderProduct().soft_KHO_Order_Child().reduce(function (obj, item) {
            obj[item.soft_IdNPP()] = obj[item.soft_IdNPP()] || [];
            obj[item.soft_IdNPP()].push(item);
            return obj;
        }, {});
        var groups = Object.keys(group_to_values).map(function (key) {
            return { group: key, product: group_to_values[key] };
        });
        self.listPrintOrder(groups);
        self.IsPrintOrdering(true);
    };
    self.Print = function (val) {
        var el = "printorder_" + val.group;
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
};