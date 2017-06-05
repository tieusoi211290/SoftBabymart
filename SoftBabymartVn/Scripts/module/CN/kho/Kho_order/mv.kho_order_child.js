var OrderChild = OrderChild || {};
OrderChild.mOrderChild = function () {
    var self = this;
    self.Id = ko.observable("");
    self.IdOrder = ko.observable();
    self.soft_IdNPP = ko.observable();
    self.soft_IdDvt = ko.observable();
    self.slDat = ko.observable();
    self.slTBban = ko.observable();
    self.tensp = ko.observable("");
    self.ghichu = ko.observable("");
    self.IdProduct = ko.observable();
    self.IsEdit = ko.observable(false);
    self.masp = ko.observable("");
    self.tenNPP = ko.observable("");
    self.tendvt = ko.observable("");

};
OrderChild.mvOrderChild = function (IdOrder) {
    var self = this;
    self.listTableOrderChild = ko.observableArray([]);
    self.loadList = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/KHO_Order/GetDetail"),
            data: { id: IdOrder }
        }).done(function (data) {
            self.listTableOrderChild(Utils.MapArray(data.soft_KHO_Order_Child, OrderChild.mOrderChild));
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.save = function (mdata) {
        $.ajax({
            type: "PUT",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: "/KHO_Order/Edit",
            data: ko.toJSON({ model: mdata }),
        }).done(function (data) {
            self.loadList();
            Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.delete = function (d) {
        Utils.openConfirm('Xóa sản phẩm này ra khỏi đơn hàng?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/KHO_OrderChild/Remove/" + d.Id
            }).done(function (data) {
                self.loadList();
                Utils.showWait(false);
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function () {
                Utils.showWait(false);
            });
        });
    };
    self.Start = function () {
        self.loadList();
        ko.applyBindings(self, document.getElementById('kho_Order_ChildViewId-' + IdOrder));
    };

    self.CancleChangeEdit = function (val) {
        val.IsEdit(false)
    };
    self.ChangeEdit = function (val) {
        ko.utils.arrayForEach(self.listTableOrderChild(), function (obj) {
            obj.IsEdit(false)
        });
        val.IsEdit(true)
    };
};

