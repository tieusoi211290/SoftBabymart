var Warehouse = Warehouse || {};
Warehouse.mWarehouse = function () {
	var self = this;
	self.Id = ko.observable();
	self.IdKho = ko.observable();
	self.IdNPP = ko.observable();
	self.TenNPP = ko.observable();
	self.Kho = ko.observable();
	self.Total = ko.observable();
	self.Date = ko.observable();
	self.Note = ko.observable();
	self.isNew = ko.observable();
	self.IsNewRecord = ko.observable(false);
	self.ItemsKHO_Product_DetailModel = ko.observableArray();
};
Warehouse.mWarehouse.prototype.toJSON = function () {
	return {
		Id: this.Id,
		IdKho: this.IdKho,
		IdNPP:this.IdNPP,
		Total: this.Total,
		Date: this.Date,
		Note: this.Note,
		ItemsKHO_Product_DetailModel: this.ItemsKHO_Product_DetailModel
	}
};
Warehouse.mKHO_Product_DetailModel = function () {
	var self = this;
	self.Id = ko.observable();
	self.Id_kho_product = ko.observable();
	self.IdProduct = ko.observable();
	self.SL = ko.observable(0);
	self.Price = ko.observable(0);
	self.masp = ko.observable();
	self.soft_Barcode = ko.observable();
	self.tensp = ko.observable();
	self.soft_GiaM = ko.observable();
	self.IsMember = ko.observable(true);
};
Warehouse.mKHO_Product_DetailModel.prototype.toJSON = function () {
	return {
		Id: this.Id,
		Id_kho_product: this.Id_kho_product,
		IdProduct: this.IdProduct,
		SL: this.SL,
		Price: this.Price
	}
};