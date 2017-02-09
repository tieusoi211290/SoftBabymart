var Warehouse = Warehouse || {};
Warehouse.mvWarehouse = function () {
    var self = this;
    self.listTableNPP = ko.observableArray();
    Utils.loadListNPP(function (data) {
        self.listTableNPP(data);
    });
    self.ListOrderProduct = ko.observableArray();
    self.GetListOrderProduct = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/Warehouse/ListOrderProduct"
        }).done(function (data) {
            self.ListOrderProduct(Utils.MapArray(data, Warehouse.mWarehouse));
            ko.utils.arrayForEach(self.ListOrderProduct(), function (obj) {
                if (obj.Date()) {
                    var time = obj.Date();
                    if (time.constructor != Date)
                        obj.Date(new Date(parseInt(time.substr(6))));
                };
                ko.mapping.fromJS(obj.ItemsKHO_Product_DetailModel, {}, new Warehouse.mKHO_Product_DetailModel);
            })
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    self.GetOrderProductDetail = function (val) {
        var dateName;
        if (val.Date) {
            var time = val.Date;
            if (time.constructor != Date)
                dateName = new Date(parseInt(time.substr(6)));
        };
        Utils.addTabDynamic('Đơn hàng kho ' + val.Kho + '- ' + Utils.datelong(dateName), '/Warehouse/RenderViewDetail?id=' + val.Id, '#tabcontentCN')
    };
    //**************************************************************
    self.Start = function () {
        self.GetListOrderProduct();
        ko.applyBindings(self, document.getElementById('Kho_productViewId'));
    };
};

