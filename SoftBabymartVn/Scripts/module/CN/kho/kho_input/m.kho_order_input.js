var KHO_Order_Product = KHO_Order_Product || {};
KHO_Order_Product.mKHO_Order_Input = function () {
    var self = this;
    self.Id = ko.observable();
    self.ViewId = ko.observable();
    self.Note = ko.observable();
    self.Total = ko.observable();
    self.DateUpdate = ko.observable();
    self.soft_KHO_Order_Input_Child = ko.observableArray();
};
KHO_Order_Product.mKHO_Order_Input_Child = function () {
    var self = this;
    self.IdProduct = ko.observable();
    self.masp = ko.observable();
    self.tensp = ko.observable();
    self.tendvt = ko.observable();
    self.Total_Money = ko.observable(0);
    self.Total_Input = ko.observable(0);
    self.IdDvt = ko.observable();

    self.IdNPP = ko.observable();
    self.Total = ko.computed(function () {
       if (self.Total_Money() > 0 || self.Total_Input()>0)
            return self.Total_Money() * self.Total_Input();
        return 0;
    });
    self.Note = ko.observable();
    self.Barcode = ko.observable();
    self.NPP = ko.observable();
};
KHO_Order_Product.NPP = function () {
    var self = this;
    self.Id = ko.observable();
    self.TenNPP = ko.observable();
    self.Diachi = ko.observable();
    self.Dienthoai = ko.observable();
    self.Email = ko.observable();
};

