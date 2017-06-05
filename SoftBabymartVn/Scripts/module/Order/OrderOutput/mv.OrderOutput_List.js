var Order = Order || {};
Order.mvOrderOutputList = function () {
    var self = this;
    self.lstOrder_Output = ko.observableArray();
    self.GetListOrderOutput = function () {
        self.lstOrder_Output([]);
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
            url: Utils.url("/Order_Output/GetOrder_Output"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                self.lstOrder_Output(Utils.MapArray(data.Data.listTable, Order.mOrder));
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
        self.GetListOrderOutput();
    });
    self.TmpTable().ItemPerPage.subscribe(function () {
        self.TmpTable().CurrentPage(1);
        self.GetListOrderOutput();
    });
    self.TmpTable().Sortby.subscribe(function (val) {
        if (val)
            self.GetListOrderOutput();
    });
    self.lstOrder_Output.subscribe(function (val) {
        self.TmpTable().listData(val);
    });
    self.TmpTable().nameTemplate('table_OrderOutput');
    //----------------------Filter-------------------
    self.GetDetail = function (val) {
        ko.utils.arrayForEach(self.TmpTable().listData(), function (v) {
            v.IsViewDetail(false);
        })
        val.IsViewDetail(true);
    }
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('OrderOutputListViewId'));
        self.GetListOrderOutput();
    };

};
