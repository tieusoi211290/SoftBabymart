var KHO_Order_Product = KHO_Order_Product || {};
KHO_Order_Product.mKHO_Order_Product = function () {
    var self = this;
    self.IdProduct = ko.observable();
    self.masp = ko.observable();
    self.tensp = ko.observable();
    self.tendvt = ko.observable();
    self.tenNPP = ko.observable();
    self.slTBban = ko.observable();
    self.soft_soluongton = ko.observable();
    self.soft_IdNPP = ko.observable();
    self.slDat = ko.observable();
    self.iddvt = ko.observable();
    self.idNPP = ko.observable();
    self.ghichu = ko.observable();
    self.NPP = ko.observable();
    self.Barcode = ko.observable();
};
KHO_Order_Product.NPP = function () {
    var self = this;
    self.Id = ko.observable();
    self.TenNPP = ko.observable();
    self.Diachi = ko.observable();
    self.Dienthoai = ko.observable();
    self.Email = ko.observable();
};

KHO_Order_Product.mKHO_Order = function () {
    var self = this;
    self.Note = ko.observable();
    self.soft_KHO_Order_Child = ko.observableArray();
};
