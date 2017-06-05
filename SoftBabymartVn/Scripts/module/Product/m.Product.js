var Product = Product || {};
Product.mProduct = function () {
    var self = this;
    self.product = ko.observable();
    self.product_price = ko.observable();
    self.product_stock = ko.observable();
};
Product.mProductSample = function () {
    var self = this;
    self.Id = ko.observable();
    self.SuppliersId = ko.observable();
    self.UnitId = ko.observable();
    self.Barcode = ko.observable();
    self.Code = ko.observable();
    self.CodeType = ko.observable()
    self.ProductName = ko.observable();
    self.Detail_Info = ko.observable();
    self.Stop_Sale = ko.observable();
    self.PriceBase = ko.observable();
    self.PriceBase_Old = ko.observable();
    self.PriceInput = ko.observable();
    self.Stock_Total = ko.observable();
    self.HasInChannel = ko.observable(false);
    self.IsEditPro = ko.observable();
};
Product.mProduct_Price = function () {
    var self = this;
    self.Id = ko.observable();
    self.ProductId = ko.observable();
    self.IdKenh = ko.observable();
    self.Count_sale = ko.observable();
    self.Price = ko.observable(0);
    self.PriceChange = ko.observable(0);
    self.IsEdit = ko.observable(false);
    self.Code = ko.observable();
    self.ProductName = ko.observable();
    self.Barcode = ko.observable();
    self.IsChangePrice = ko.observable(false);
    self.soft_Product = ko.observable();
    self.OptionPrice = ko.observableArray();
}
Product.mProduct_Stock = function () {
    var self = this;
    self.BranchesId = ko.observable();
    self.ProductId = ko.observable();
    self.Stock_Total = ko.observable(0);
    self.Note = ko.observable();
}