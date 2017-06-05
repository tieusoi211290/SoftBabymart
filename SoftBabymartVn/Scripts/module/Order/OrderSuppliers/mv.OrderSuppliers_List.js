var Order = Order || {};
Order.mvOrderSuppliersList = function () {
    var self = this;
    self.lstOrder_Suppliers = ko.observableArray();
    self.GetListOrderSuppliers = function () {
        self.lstOrder_Suppliers([]);
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
            url: Utils.url("/Order_Suppliers/GetOrder_Suppliers"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                self.lstOrder_Suppliers(Utils.MapArray(data.Data.listTable, Order.mOrder));
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
        self.GetListOrderSuppliers();
    });
    self.TmpTable().ItemPerPage.subscribe(function () {
        self.TmpTable().CurrentPage(1);
        self.GetListOrderSuppliers();
    });
    self.TmpTable().Sortby.subscribe(function (val) {
        if (val)
            self.GetListOrderSuppliers();
    });
    self.lstOrder_Suppliers.subscribe(function (val) {
        self.TmpTable().listData(val);
    });
    self.TmpTable().nameTemplate('table_OrderSuppliers');
    //----------------------Filter-------------------
    self.GetDetail = function (val) {
        ko.utils.arrayForEach(self.TmpTable().listData(), function (v) {
            v.IsViewDetail(false);
        })
        val.IsViewDetail(true);
    }
    self.CreateOrderInput = function (val) {
        debugger
        Utils.addTabDynamic('Nhập hàng', Utils.url('/Order_Input/RenderViewCreate'), '#contentX', true, val.Detail());
    }
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderSuppliersListViewId'));
        self.GetListOrderSuppliers();
    };
};
