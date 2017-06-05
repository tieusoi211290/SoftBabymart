var Filter = Filter || {};
Filter.mFilter_CatalogProduct = function () {
    var self = this;
    self.Id = ko.observable();
    self.RefId = ko.observable();
    self.ChannelId = ko.observable();
    self.Name = ko.observable();
    self.Checked = ko.observable();
};
Filter.mFilter_SuppliersProduct = function () {
    var self = this;
    self.Id = ko.observable();
    self.Address = ko.observable();
    self.Phone = ko.observable();
    self.Name = ko.observable();
    self.Email = ko.observable();
    self.AccBank = ko.observable();
    self.Thue = ko.observable();
    self.Checked = ko.observable();
};
Filter.mFilter_UnitProduct = function () {
    var self = this;
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.Checked = ko.observable();
};
Filter.mvFilter_Product = function (Id) {
    var self = this;
    //********************************************
    self.listCatalog = ko.observableArray([]);
    self.listSuppliers = ko.observableArray([]);
    self.listUnit = ko.observableArray([]);
    //********************************************
    self.lstCatalog_Product = ko.observableArray([]);
    self.lstSuppliers_Product = ko.observableArray([]);
    self.lstUnit_Product = ko.observableArray([]);
    //********************************************
    Utils.loadListCatalog(function (data) {
        self.listCatalog(Utils.MapArray(data, Filter.mFilter_CatalogProduct));
    });
    Utils.loadListSuppliers(function (data) {
        self.listSuppliers(Utils.MapArray(data, Filter.mFilter_SuppliersProduct));
    });
    Utils.loadListUnit(function (data) {
        self.listUnit(Utils.MapArray(data, Filter.mFilter_UnitProduct));
    });
    //********************************************
    self.changeCatalog = function (val) {
        if (val.Checked())
            self.lstCatalog_Product.push(val.Id());
        else
            self.lstCatalog_Product.remove(val.Id());
    };
    self.changeSuppliers = function (val) {
        if (val.Checked())
            self.lstSuppliers_Product.push(val.Id());
        else
            self.lstSuppliers_Product.remove(val.Id());
    };
    self.changeUnit = function (val) {
        if (val.Checked())
            self.lstUnit_Product.push(val.Id());
        else
            self.lstUnit_Product.remove(val.Id());
    };
    //********************************************
};