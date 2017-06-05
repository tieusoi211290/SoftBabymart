var KHO_Order = KHO_Order || {};
KHO_Order.mOrder = function () {
    var self = this;
    self.Id = ko.observable();
    self.Note = ko.observable();
    self.DateOrder = ko.observable();
    self.Total = ko.observable();
};
KHO_Order.Order = function () {
    var self = this;

    self.listTable = ko.observableArray([]);

    self.LoadList = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/KHO_Order/GetList"
        }).done(function (data) {
            self.listTable(Utils.MapArray(data, KHO_Order.mOrder));
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };

    self.ReviewInfo = function (val) {
        if (val.Id) {
            Utils.addTabDynamic('Hóa đơn số' + val.Id, Utils.url('/KHO_Order/RenderViewDetail?id=' + val.Id), '#contentX');
        }
        else
            Utils.notify("Thông báo", 'Dữ liệu không hợp lệ');
    };
    self.Start = function () {
        self.LoadList();
        ko.applyBindings(self, document.getElementById('kho_OrderViewId'));
    };
};

