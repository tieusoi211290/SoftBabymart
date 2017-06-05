var KHO = KHO || {};
KHO.mKHO_Product = function () {
    var self = this;
    self.Id = ko.observable();
    self.IdProduct = ko.observable("");
    self.IdKHO = ko.observable();
    self.SL = ko.observable("");
    self.Note = ko.observable("");
    self.masp = ko.observable("");
    self.barcode = ko.observable("");
    self.tensp = ko.observable("");
};
KHO.mKHODetail = function () {
    var self = this;
    self.Id = ko.observable();
    self.Makho = ko.observable();
    self.Kho = ko.observable();
    self.Diachi = ko.observable();
    self.Lienhe = ko.observable();
};

KHO.mvKHO_Product = function (idKho) {
    var self = this;
    self.khoDetail = ko.observable(new KHO.mKHODetail);
    self.lstKhoProduct = ko.observableArray();
    self.LoadKhoDetail = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/KHO_Product/LoadKho"),
            cache: false,
            data: { idKho: idKho },
        }).done(function (data) {
            debugger
            if (data == null)
                return

            ko.mapping.fromJS(data, {}, self.khoDetail);
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.LoadListKho_Product = function (groupTeam, groupNPP) {
        var model = ko.toJSON({ idKho: idKho, groupTeam: groupTeam, groupNPP: groupNPP });
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/KHO_Product/LoadKho_product"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: model,
        }).done(function (data) {
            if (data == null)
                return
            debugger
            if (data.length > 0) {
                $(".div_dataTable_kho_product_" + idKho).css({ 'display': 'block' });
                self.lstKhoProduct(Utils.MapArray(data, KHO.mKHO_Product));
            }
            else
                $(".div_dataTable_kho_product_" + idKho).css({ 'display': 'none' });

        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.CancleChangeEdit = function (val) {
        val.IsEdit(false)
    };
    self.ChangeEdit = function (val) {
        ko.utils.arrayForEach(self.lstKenhProduct(), function (obj) {
            if (obj.Id())
                obj.IsEdit(false)
        });
        val.IsEdit(true)
    };
    self.SaveChange = function (val) {
        if (val.Id()) {
            $.ajax({
                type: "PUT",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: "/KENH_Product/ChangePrice",
                data: ko.toJSON({ model: val }),
            }).done(function (data) {
                self.CancleChangeEdit(val);
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error');
            }).fail(function (err) {
                viewmodel.reset();
            });
        }
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('kho_productViewId_' + idKho));
        self.LoadKhoDetail();
    };
    self.tmptool = ko.observable(new TmpControltool(idKho));
    self.tmptool().chosenGroupNPP.subscribe(function (val) {
        if (val.length <= 0 && self.tmptool().chosenGroupProduct().length <= 0) {
            return
        }
        self.LoadListKho_Product(self.tmptool().chosenGroupProduct(), val);
    });
    self.tmptool().chosenGroupProduct.subscribe(function (val) {
        if (val.length <= 0 && self.tmptool().chosenGroupNPP().length <= 0) {
            return
        }
        self.LoadListKho_Product(val, self.tmptool().chosenGroupNPP());
    });
};