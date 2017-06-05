var Order = Order || {};
Order.mvOrderSaleList = function () {
    var self = this;
    self.lstOrder_Sale = ko.observableArray();
    self.OptionStatusOrder = ko.observableArray([
        { Value: 1, Name: 'Chờ xử lý' },
        { Value: 2, Name: 'Đang giao hàng' },
        { Value: 3, Name: 'Hoàn thành' },
    ]);
    self.GetListOrderSale = function () {
        self.lstOrder_Sale([]);
        var model = {
            pageindex: self.TmpTable().CurrentPage(),
            pagesize: self.TmpTable().ItemPerPage(),
            sortby: self.TmpTable().Sortby(),
            sortbydesc: self.TmpTable().SortbyDesc(),
        };
        self.TmpTable().Sortby(undefined);
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/OrderChannel/GetOrder"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                debugger
                self.lstOrder_Sale(Utils.MapArray(data.Data.listTable, Order.mOrder));
                self.TmpTable().Totalitems(data.Data.totalItems);
                self.TmpTable().StartItem(data.Data.startItem);
            }
            else
                Utils.notify("Thông báo", data.messaging, 'error');

        }).always(function () {
            Utils.showWait(false);
        });
    };
    //--------------Table--------------------------------
    self.TmpTable = ko.observable(new Paging_TmpControltool());
    self.TmpTable().CurrentPage.subscribe(function () {
        self.GetListOrderSale();
    });
    self.TmpTable().ItemPerPage.subscribe(function () {
        self.TmpTable().CurrentPage(1);
        self.GetListOrderSale();
    });
    self.TmpTable().Sortby.subscribe(function (val) {
        if (val)
            self.GetListOrderSale();
    });
    self.lstOrder_Sale.subscribe(function (val) {
        self.TmpTable().listData(val);
    });
    self.TmpTable().nameTemplate('table_OrderSale');
    //----------------------Filter-------------------
    self.GetDetail = function (val) {
        ko.utils.arrayForEach(self.TmpTable().listData(), function (v) {
            v.IsViewDetail(false);
        })
        val.IsViewDetail(true);
    };
    self.UpdateOrder = function (val) {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/OrderChannel/UpdateDone_Order_Sale"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ model: val }),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                Utils.notify("Thông báo", 'Cập nhật đơn hàng thành công', 'success');
            } else
                Utils.notify("Thông báo", data.messaging, 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderSaleListViewId'));
        self.GetListOrderSale();
    };
};
