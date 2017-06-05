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
    self.PriceBase = ko.observable();
    self.PriceBase_Old = ko.observable();
    self.PriceInput = ko.observable();
    self.Stock_Total = ko.observable();
    self.HasInChannel = ko.observable(false);
    self.IsEditPro = ko.observable();
    self.soft_Channel = ko.observable();
};


Order.mCustomer = function () {
    var self = this;
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.Address = ko.observable();
    self.Phone = ko.observable();
    self.Email = ko.observable();
    self.Code = ko.observable();
    self.DistrictId = ko.observable();
    self.ProvinceId = ko.observable();

    self.NameShip = ko.observable();
    self.NameShip = ko.observable();
    self.AddressShip = ko.observable();
    self.PhoneShip = ko.observable();
    self.DistrictIdShip = ko.observable();
    self.ProvinceIdShip = ko.observable();

};
Order.mOrder = function () {
    var self = this;
    var index = 0;
    self.Id = ko.observable();
    self.IsShip = ko.observable();
    self.Status = ko.observable();
    self.StatusName = ko.observable();
    self.Status.subscribe(function (val) {
        if (index > 0)
            switch (val) {
                case 1:
                    self.StatusName('Pending');
                    break;
                case 2:
                    self.StatusName('Shipped');
                    break;
                case 3:
                    self.StatusName('Done');
                    break;
                default:
                    self.StatusName('---');
            }
        if (index == 0) {
            index += 1;
        }
    });
    self.Discount = ko.observable(0);
    self.Total = ko.observable(0);
    self.TotalSum = ko.computed(function () {
        return parseInt(self.Total() - self.Discount());
    });
    self.TotalMoney = ko.observable();
    self.Note = ko.observable();
    self.TypeOrder = ko.observable();
    self.Detail = ko.observableArray();
    self.IsViewDetail = ko.observable(false);
    self.Id_From = ko.observable();
    self.Id_To = ko.observable();
    self.Customer = ko.observable(new Order.mCustomer);

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
    });
};
Order.mChannel = function () {
    var self = this;
    self.Id = ko.observable();
    self.Channel = ko.observable();
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
