var Product = Product || {};
Product.mvProduct = function () {
    var self = this;
    self.TmpTable = ko.observable(new Paging_TmpControltool());
    self.TmpTable().CurrentPage.subscribe(function () {
        self.LoadListProduct();
    });
    self.TmpTable().ItemPerPage.subscribe(function () {
        self.TmpTable().CurrentPage(1);
        self.LoadListProduct();
    });
    self.TmpTable().KeywordSearch.subscribe(function () {
        self.LoadListProduct();
    });
    self.TmpTable().Sortby.subscribe(function (val) {
        if (val)
            self.LoadListProduct();
    });
    self.TmpTable().nameTemplate('table_Products');
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
        if (val.length > 0) {
            self.Fiterby.push({ 'Name': 'Catalog', 'Values': val });
        }
        self.LoadListProduct();
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
        self.LoadListProduct();
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
        self.LoadListProduct();
    });

    self.EventClick_Price = function (val) {
        ko.utils.arrayForEach(self.TmpTable().listData(), function (o) {
            o.product_price().IsChangePrice(false);
        })
        val.product_price().IsChangePrice(true);
    };

    self.LoadListProduct = function () {
        self.TmpTable().listData([]);
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
            url: Utils.url("/Product/GetProductby"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ pageinfo: model }),
        }).done(function (data) {
            if (data == null)
                return
            if (!data.isError) {
                var lstTmp = Utils.MapArray(data.Data.listTable, Product.mProduct);
                ko.utils.arrayForEach(lstTmp, function (item) {
                    item.product(ko.mapping.fromJS(item.product(), {}, new Product.mProductSample));
                    item.product_price(ko.mapping.fromJS(item.product_price(), {}, new Product.mProduct_Price));
                    item.product_stock(ko.mapping.fromJS(item.product_stock(), {}, new Product.mProduct_Stock));
                    item.product_price().OptionPrice([
                               { "Name": "Giá Kênh", "Code": "PriceNow", "Value": item.product_price().Price(), "Id": item.product_price().ProductId() },
                               { "Name": "Giá cơ bản", "Code": "Price", "Value": item.product().PriceBase(), "Id": item.product_price().ProductId() },
                               { "Name": "Giá nhập cũ", "Code": "PriceOld", "Value": item.product().PriceBase_Old(), "Id": item.product_price().ProductId() },
                               { "Name": "Giá nhập mới", "Code": "PriceNew", "Value": item.product().PriceInput(), "Id": item.product_price().ProductId() }
                    ]);


                });
                self.TmpTable().listData(lstTmp);
                self.TmpTable().Totalitems(data.Data.totalItems);
                self.TmpTable().StartItem(data.Data.startItem);
            } else
                Utils.notify("Thông báo", data.messaging, 'error');

        }).always(function () {
            Utils.showWait(false);
        });
    };


    //------Price-------
    self.selectedPrice = ko.observable({ "Name": "", "Code": "", "Value": 0, "Id": 0 });
    self.selectedPrice.subscribe(function (val) {
        var product = ko.utils.arrayFirst(self.TmpTable().listData(), function (o) {
            return o.product().Id() == val.Id;
        });
        if (product != undefined)
            product.product_price().PriceChange(val.Value);
    });
    self.calculator = ko.observable('+');
    self.money_percent = ko.observable('vnd');
    self.intCalculator = ko.observable(0);
    self.calculatorPrice = function (val) {

        var resultSelect = ko.utils.arrayFirst(val.product_price().OptionPrice(), function (o) {
            return o.Code == self.selectedPrice().Code;
        });
        if (resultSelect == undefined)
            return;

        if (self.calculator() == "+") {
            if (self.money_percent() == "percent") {
                var tmp = (parseInt(self.intCalculator()) / 100) * parseInt(resultSelect.Value);
                val.product_price().PriceChange(resultSelect.Value + tmp);
            }
            else
                val.product_price().PriceChange(parseInt(resultSelect.Value) + parseInt(self.intCalculator()))
        }
        if (self.calculator() == "-") {
            if (self.money_percent() == "percent") {
                var tmp = (parseInt(self.intCalculator()) / 100) * parseInt(resultSelect.Value);
                val.product_price().PriceChange(resultSelect.Value - tmp);
            }
            else
                val.product_price().PriceChange(parseInt(resultSelect.Value) - parseInt(self.intCalculator()))
        }

    };
    self.EventChange_InputPrice = function (val) {
        self.calculatorPrice(val);
    };
    self.submitPrice = function (val, isAppRuleAll) {
        Utils.showWait(true);
        val.product_price().Price(val.product_price().PriceChange());
        if (isAppRuleAll) {
            ko.utils.arrayForEach(self.TmpTable().listData(), function (o) {
                self.calculatorPrice(o);
            });
        }
        var model = isAppRuleAll ? self.TmpTable().listData() : [val];
        $.ajax({
            type: "POST",
            url: Utils.url("/Product/ChangePrice"),
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(model),
        }).done(function (data) {
            self.LoadListProduct();
            Utils.notify("Thông báo", data.messaging, !data.isError ? 'success' : 'error');
        }).always(function () {
            Utils.showWait(false);
        });
    };
    //------Price-------
    self.appRuleRriceAll = ko.observable(false);
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('ProductViewId'));
        self.LoadListProduct();
    };
};