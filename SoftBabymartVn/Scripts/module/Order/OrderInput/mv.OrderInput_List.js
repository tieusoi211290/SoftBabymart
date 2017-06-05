var Order = Order || {};
Order.mvOrderInputList = function () {
    var self = this;
    self.lstOrder_Input = ko.observableArray();
    self.GetListOrderInput = function () {
        self.lstOrder_Input([]);
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
            url: Utils.url("/Order_Input/GetOrder_Inside"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                self.lstOrder_Input(Utils.MapArray(data.Data.listTable, Order.mOrder));
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
        self.GetListOrderInput();
    });
    self.TmpTable().ItemPerPage.subscribe(function () {
        self.TmpTable().CurrentPage(1);
        self.GetListOrderInput();
    });
    self.TmpTable().Sortby.subscribe(function (val) {
        if (val)
            self.GetListOrderInput();
    });
    self.lstOrder_Input.subscribe(function (val) {
        self.TmpTable().listData(val);
    });
    self.TmpTable().nameTemplate('table_OrderInput');
    //----------------------Filter-------------------
    self.GetDetail = function (val) {
        ko.utils.arrayForEach(self.TmpTable().listData(), function (v) {
            v.IsViewDetail(false);
        })
        val.IsViewDetail(true);
    };
    self.UpdateOrder = function (val) {
        val.StatusName('Done');
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/Order_Input/UpdateDone_Order_Inside"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ id: val.Id() }),
        }).done(function (data) {
            if (data.isError)
                Utils.notify("Thông báo", data.messaging, 'error');

        }).always(function () {
            Utils.showWait(false);
        });
    };
 

    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderInputListViewId'));
        self.GetListOrderInput();
    };
};
