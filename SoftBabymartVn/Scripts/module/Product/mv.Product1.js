var Channel = Channel || {};
Channel.mvChannel_Product = function () {
    var self = this;
    self.lstKenhProduct = ko.observableArray();
    self.mProduct = ko.observable();
    self.TmpTable = ko.observable(new Paging_TmpControltool());
    self.TmpTable().CurrentPage.subscribe(function () {
        self.LoadListKenh_Product();
    });
    self.TmpTable().ItemPerPage.subscribe(function () {
        self.TmpTable().CurrentPage(1);
        self.LoadListKenh_Product();
    });
    self.TmpTable().KeywordSearch.subscribe(function () {
        self.LoadListKenh_Product();
    });
    self.TmpTable().Sortby.subscribe(function (val) {
        if (val)
            self.LoadListKenh_Product();
    });
    self.lstKenhProduct.subscribe(function (val) {
        self.TmpTable().listData(val);
    });
    self.TmpTable().nameTemplate('table_ProductChannel');
    self.Fiterby = ko.observableArray();
    self.FilterProduct = ko.observable(new Filter.mvFilter_Product());
    self.FilterProduct().lstCatalog_Product.subscribe(function (val) {
        if (self.Fiterby().length > 0) {
            var hasFil = ko.utils.arrayFirst(self.Fiterby(), function (val) {
                return val.Name == 'Catalog';
            });
            if (hasFil)
                self.Fiterby.remove(hasFil);
        }
        self.Fiterby.push({ 'Name': 'Catalog', 'Values': val });
        self.LoadListKenh_Product();
    });
    self.FilterProduct().lstSuppliers_Product.subscribe(function (val) {
        if (self.Fiterby().length > 0) {
            var hasFil = ko.utils.arrayFirst(self.Fiterby(), function (val) {
                return val.Name == 'Suppliers';
            });
            if (hasFil)
                self.Fiterby.remove(hasFil);
        }
        self.Fiterby.push({ 'Name': 'Suppliers', 'Values': val });
        self.LoadListKenh_Product();
    });
    self.FilterProduct().lstUnit_Product.subscribe(function (val) {
        if (self.Fiterby().length > 0) {
            var hasFil = ko.utils.arrayFirst(self.Fiterby(), function (val) {
                return val.Name == 'Unit';
            });
            if (hasFil)
                self.Fiterby.remove(hasFil);
        }
        self.Fiterby.push({ 'Name': 'Unit', 'Values': val });
        self.LoadListKenh_Product();
    });

    self.EventClick_Price = function (val) {
        ko.utils.arrayForEach(self.lstKenhProduct(), function (o) {
            o.IsChangePrice(false);
        })
        val.IsChangePrice(true);
    };
    self.selectedPrice = ko.observable({ "Name": "", "Code": "", "Value": 0, "Id": 0 });
    self.selectedPrice.subscribe(function (val) {
        var result = ko.utils.arrayFirst(self.lstKenhProduct(), function (o) {
            return o.Id() == val.Id;
        });
        if (result != undefined)
            result.PriceChange(val.Value);
    });
    self.calculator = ko.observable('+');
    self.money_percent = ko.observable('vnd');
    self.intCalculator = ko.observable(0);
    self.calculatorPrice = function (val) {

        var resultSelect = ko.utils.arrayFirst(val.OptionPrice(), function (o) {
            return o.Code == self.selectedPrice().Code;
        });
        if (resultSelect == undefined)
            return;

        if (self.calculator() == "+") {
            if (self.money_percent() == "percent") {
                var tmp = (parseInt(self.intCalculator()) / 100) * parseInt(resultSelect.Value);
                val.PriceChange(resultSelect.Value + tmp);
            }
            else
                val.PriceChange(parseInt(resultSelect.Value) + parseInt(self.intCalculator()))
        }
        if (self.calculator() == "-") {
            if (self.money_percent() == "percent") {
                var tmp = (parseInt(self.intCalculator()) / 100) * parseInt(resultSelect.Value);
                val.PriceChange(resultSelect.Value - tmp);
            }
            else
                val.PriceChange(parseInt(resultSelect.Value) - parseInt(self.intCalculator()))
        }

    };
    self.EventChange_InputPrice = function (val) {
        self.calculatorPrice(val);
    };
    self.appRuleRriceAll = ko.observable(false);

    self.LoadListKenh_Product = function () {
        self.lstKenhProduct([]);
        var model = {
            pageindex: self.TmpTable().CurrentPage(),
            pagesize: self.TmpTable().ItemPerPage(),
            keyword: self.TmpTable().KeywordSearch(),
            sortby: self.TmpTable().Sortby(),
            sortbydesc: self.TmpTable().SortbyDesc(),
            filterby: self.Fiterby()
        };
        self.TmpTable().Sortby(undefined);
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/Channel_Product/LoadListChannel_product"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {

                var lstTmp = (Utils.MapArray(data.Data.listTable, Channel.mChannel_Product));
                ko.utils.arrayForEach(lstTmp, function (item) {
                    item.soft_Product(ko.mapping.fromJS(item.soft_Product(), {}, new Channel.mProduct));
                    item.OptionPrice([
                                    { "Name": "Giá hiện tại", "Code": "PriceNow", "Value": item.Price(), "Id": item.Id() },
                                    { "Name": "Giá vốn", "Code": "Price", "Value": item.soft_Product().PriceInput(), "Id": item.Id() },
                                    { "Name": "Giá nhập cũ", "Code": "PriceOld", "Value": item.soft_Product().PriceInput_Old(), "Id": item.Id() },
                                    { "Name": "Giá nhập mới", "Code": "PriceNew", "Value": item.soft_Product().PriceInput_New(), "Id": item.Id() }
                    ]);
                });
                ko.utils.arrayForEach(lstTmp, function (val) {
                    val.Code(val.soft_Product().Code());
                    val.ProductName(val.soft_Product().ProductName());
                    val.Barcode(val.soft_Product().Barcode());
                });
                self.lstKenhProduct(lstTmp);
                self.TmpTable().Totalitems(data.Data.totalItems);
                self.TmpTable().StartItem(data.Data.startItem);
            }
            else
                Utils.notify("Thông báo", data.messaging, 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.submitPrice = function (val, isAppRuleAll) {
        Utils.showWait(true);
        val.Price(val.PriceChange());
        if (isAppRuleAll) {
            ko.utils.arrayForEach(self.lstKenhProduct(), function (o) {
                self.calculatorPrice(o);
            });
        }
        var model = isAppRuleAll ? self.lstKenhProduct() : [val];
        $.ajax({
            type: "POST",
            url: Utils.url("/Channel_Product/ChangePrice"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model),
        }).done(function (data) {
            Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.removeProduct = function (val) {
        Utils.confirm("Bạn có muốn xóa sản phẩm này ra khỏi Kênh?", function () {
            Utils.showWait(true);
            self.TmpTable().listData.remove(val);
            $.ajax({
                type: "POST",
                url: Utils.url("/Channel_Product/RemoveProduct"),
                cache: false,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON({ Id: val.Id() })
            }).done(function (data) {
                Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
            }).always(function () {
                Utils.showWait(false);
            });
        })
    };
    self.IsEdit = ko.observable(false);
    self.mEditProduct_Channel = ko.observable();
    self.editProduct = function (val) {
        val.IsEdit(true);
        ko.utils.arrayForEach(self.lstKenhProduct(), function (o) {
            if (o.Id() != val.Id())
                o.IsEdit(false);
        });
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/Product/GetProductbyId/" + val.ProductId()),
            cache: false,
            dataType: 'json',
        }).done(function (data) {
            if (!data.isError) {
                self.mProduct(ko.mapping.fromJS(data.Data, {}, new Channel.mProduct));
            } else
                Utils.notify("Thông báo", data.messaging, 'error');
        }).always(function () {
            Utils.showWait(false);
        });

    };
    self.SaveEitProduct = function (val) {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: Utils.url("/Product/UpdateProduct"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ model: val })
        }).done(function (data) {
            Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
            self.LoadListKenh_Product();
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('channel_productViewId'));
        self.LoadListKenh_Product();
    };

};