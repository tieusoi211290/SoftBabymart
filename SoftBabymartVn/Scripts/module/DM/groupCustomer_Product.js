var GroupCustomer_Product = GroupCustomer_Product || {};
GroupCustomer_Product.mGroupCustomer_Product = function () {
    var self = this;
    self.Id = ko.observable();
    self.ProductId = ko.observable();
    self.BanleCH = ko.observable();
    self.Online = ko.observable();
    self.Si = ko.observable();
    self.Lazada = ko.observable();
    self.Shopee = ko.observable();
    self.Mymall = ko.observable();
    self.Date = ko.observable();
    self.Chietkhau = ko.observable();
    self.masp = ko.observable();
    self.tensp = ko.observable();
    self.soft_IdGroupProduct = ko.observable();
    self.soft_IdNPP = ko.observable();
    self.soft_IdDvt = ko.observable();
    self.soft_Barcode = ko.observable();
    self.soft_GiaM = ko.observable();
    self.soft_GiaTC = ko.observable();
    self.Chietkhau_CH = ko.observable();
    self.Date_CH = ko.observable();
    self.Chietkhau_Onl = ko.observable(new Date());
    self.Date_Onl = ko.observable(new Date());
    self.isNew = ko.observable();
    self.currentTemp = ko.observable('readonlyTemplate_product_groupCustommer');
    self.currentTempData = ko.observable();
};
GroupCustomer_Product.mGroupCustomer_Product.prototype.toJSON = function () {
    return {
        Id: this.Id,
        ProductId: this.ProductId,
        BanleCH: this.BanleCH,
        Online: this.Online,
        Si: this.Si,
        Lazada: this.Lazada,
        Shopee: this.Shopee,
        Mymall: this.Mymall,
        Date: this.Date,
        Chietkhau: this.Chietkhau,
        masp: this.masp,
        tensp: this.tensp,
        soft_IdGroupProduct: this.soft_IdGroupProduct,
        soft_IdNPP: this.soft_IdNPP,
        soft_IdDvt: this.soft_IdDvt,
        soft_Barcode: this.soft_Barcode,
        soft_GiaM: this.soft_GiaM,
        soft_GiaTC: this.soft_GiaTC,
        Chietkhau_CH: this.Chietkhau_CH,
        Date_CH: this.Date_CH,
        Chietkhau_Onl: this.Chietkhau_Onl,
        Date_Onl: this.Date_Onl
    }
};
GroupCustomer_Product.mvGroupCustomer_Product = function () {
    var self = this;
    self.listTable = ko.observableArray([]);
    self.listTableNPP = ko.observableArray();
    self.listGroupProduct = ko.observableArray([]);
    function loadList(GroupIds, GroupNPP) {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: "/GroupCustomer_Product/GetList",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ groupProductIds: GroupIds, groupNPP: GroupNPP }),
        }).done(function (data) {
            if (data.length > 0) {
                $(".dataTable_product_groupCustommer").css({ 'display': 'block' });
                var tmpList = Utils.MapArray(data, GroupCustomer_Product.mGroupCustomer_Product);
                ko.utils.arrayForEach(tmpList, function (obj) {
                    if (obj.Date_CH()) {
                        var time = obj.Date_CH();
                        if (time.constructor != Date)
                            obj.Date_CH(new Date(parseInt(time.substr(6))));
                    }
                    obj.currentTempData(obj);
                });
                self.listTable(tmpList);
            }
            else {
                $(".dataTable_product_groupCustommer").css({ 'display': 'none' });
            }
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    self.save = function (mdata) {
        $.ajax({
            type: "PUT",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: "/GroupCustomer_Product/Edit",
            data: ko.toJSON({ model: mdata }),
        }).done(function (data) {
            self.reset(mdata);
            Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
        }).fail(function (err) {
            self.reset(mdata);
        });
    };
    self.editTemplateBind = function (obj) {
        ko.utils.arrayForEach(self.listTable(), function (o) {
            self.reset(o);
        });
        obj.currentTemp('editTemplate_product_groupCustommer');
    };
    self.reset = function (obj) {
        if (obj.currentTemp() == 'editTemplate_product_groupCustommer')
            obj.currentTemp("readonlyTemplate_product_groupCustommer");
    };

    //*********************Select*****************************************************
    self.chosenGroupProduct = ko.observableArray([]);
    self.chosenGroupNPP = ko.observableArray([]);
    self.chosenGroupNPP.subscribe(function (val) {
        if (val.length <= 0 && self.chosenGroupProduct().length <= 0) {
            return
        }
        if (val.length > 0)
            loadList(self.chosenGroupProduct(), val);
    });
    self.chosenGroupProduct.subscribe(function (val) {
        if (val.length <= 0 && self.chosenGroupNPP().length <= 0) {
            return
        }
        if (val.length > 0)
            loadList(val, self.chosenGroupNPP());
    });
    Utils.loadListNPP(function (data) {
        self.listTableNPP(data);
    });
    Utils.loadGroupProduct(function (data) {
        self.listGroupProduct(data);
    });
    //**************************************************************
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('Group_customer_productViewId'));
    };
};

