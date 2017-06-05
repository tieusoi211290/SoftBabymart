var Order = Order || {};
Order.mProduct = function () {
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
    self.PriceInput = ko.observable();
    self.PriceInput_Old = ko.observable();
    self.PriceInput_New = ko.observable();
    self.Stock_Total = ko.observable();
    self.HasInChannel = ko.observable(false);
    self.IsEditPro = ko.observable();
    self.soft_Channel = ko.observable();
};
Order.mSuppliers = function () {
    var self = this;
    self.SuppliersId = ko.observable();
    self.Name = ko.observable();
    self.Address = ko.observable();
    self.Phone = ko.observable();
    self.Email = ko.observable();
};
Order.mOrder = function () {
    var self = this;
    self.Id = ko.observable();
    self.Status = ko.observable();
    self.StatusName = ko.observable();
    self.Total = ko.observable();
    self.Note = ko.observable();
    self.TypeOrder = ko.observable();
    self.Detail = ko.observableArray();
    self.IsViewDetail = ko.observable(false);
    self.Id_From = ko.observable();
    self.Id_To = ko.observable();
    self.IsShip = ko.observable(false);
    self.Name_From = ko.observable();
    self.Name_To = ko.observable();
    self.EmployeeNameUpdate = ko.observable();
    self.EmployeeNameCreate = ko.observable();


};
Order.mOrderDetail = function () {
    var self = this;
    self.Id = ko.observable();
    self.Code = ko.observable();
    self.ProductName = ko.observable();
    self.OrderId = ko.observable();
    self.ProductId = ko.observable();
    self.Total = ko.observable(1);
    self.Price = ko.observable();
    self.Product = ko.observable();
    self.TotalMoney = ko.computed(function () {
        return self.Total() * self.Price();
    })
};
Order.mChannel = function () {
    var self = this;
    self.Id = ko.observable();
    self.Channel = ko.observable();
};
Order.mBranch = function () {
    var self = this;
    self.BranchesId = ko.observable();
    self.BranchesName = ko.observableArray();
};
Order.mOrderPrint = function () {
    var self = this;
    self.suppliers = ko.observable();
    self.products = ko.observableArray();
};
Order.mProductsPrint = function () {
    var self = this;
    self.Total = ko.observable();
    self.Product = ko.observable(new Order.mProduct);
};
