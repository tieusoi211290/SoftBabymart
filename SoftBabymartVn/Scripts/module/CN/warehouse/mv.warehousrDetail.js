var Warehouse = Warehouse || {};
Warehouse.mvWarehouseDetail = function (mWarehouse) {
    var self = this;
    self.mWarehouse = ko.observable();
    self.ListProduct = ko.observableArray();
    self.listKho = ko.observableArray();
    self.listNPP = ko.observableArray();
    self.Total = ko.observable(0);
    Utils.loadListKho(function (val) {
        self.listKho(val);
    });
    Utils.loadListNPP(function (val) {
        self.listNPP(val);
    });
    self.chosenGroupNPP = ko.observableArray([]);
    self.chosenGroupNPP.subscribe(function (val) {
        self.loadListProductBy(val);
    });
    self.LoadData = function () {
        var data_mWarehouse = mWarehouse ? ko.mapping.fromJS(mWarehouse, {}, new Warehouse.mWarehouse) : new Warehouse.mWarehouse;
        self.mWarehouse(data_mWarehouse);
        self.ListProduct(Utils.MapArray(self.mWarehouse().ItemsKHO_Product_DetailModel(), Warehouse.mKHO_Product_DetailModel));

    };
    self.SaveEdit = function () {
        debugger
        if (self.mWarehouse()) {
            if (self.ListProduct().length > 0) {
                self.mWarehouse().ItemsKHO_Product_DetailModel([]);
                ko.utils.arrayForEach(self.ListProduct(), function (obj) {
                    debugger
                    if (obj.SL() > 0 && obj.Price() > 0) {
                        self.mWarehouse().ItemsKHO_Product_DetailModel.push(obj);
                    }
                });
            }
            if (self.mWarehouse().IsNewRecord() == false) {
                Utils.showWait(true);
                $.ajax({
                    type: "POST",
                    url: "/Warehouse/SaveEdit",
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: ko.toJSON({ model: self.mWarehouse() }),
                }).done(function (data) {
                    debugger
                    Utils.showWait(false);
                }).fail(function () {
                    Utils.showWait(false);
                });
            } else {
                Utils.showWait(true);
                $.ajax({
                    type: "POST",
                    url: "/Warehouse/SaveAdd",
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: ko.toJSON({ model: self.mWarehouse() }),
                }).done(function (data) {
                    debugger
                    Utils.showWait(false);
                }).fail(function () {
                    Utils.showWait(false);
                });
            }
        }
    };

    self.loadListProductBy = function (GroupNPP) {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: "/Product/GetList",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ groupProductIds: null, groupNPP: GroupNPP }),
        }).done(function (data) {
            var tmp = [];
            ko.utils.arrayForEach(self.ListProduct(), function (obj) {
                if (obj.IsMember() == true)
                    tmp.push(obj);
            });
            self.ListProduct([]);
            ko.utils.arrayForEach(data, function (obj) {
                var item = ko.mapping.fromJS(obj, {}, new Warehouse.mKHO_Product_DetailModel);
                item.IsMember(false);
                tmp.push(item);
            });
            self.ListProduct(tmp);
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    self.Start = function () {
        self.LoadData();
        ko.applyBindings(self, document.getElementById('Kho_productDetailViewId'));
    };
};