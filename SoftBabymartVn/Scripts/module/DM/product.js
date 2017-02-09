var Product = Product || {};
Product = function () {
    var self = this;
    var IsNewRecord = false;
    self.listTable = ko.observableArray([]);
    self.listGroupProduct = ko.observableArray([]);

    function loadList(GroupIds, GroupNPP) {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            url: "/Product/GetList",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ groupProductIds: GroupIds, groupNPP: GroupNPP }),
        }).done(function (data) {
            // self.listTable([]);
            var tmplistTable = []
            ko.utils.arrayForEach(data, function (item) {
                var newmodel = new typeModel(item.id, item.masp, item.tensp, item.soft_IdNPP,
                    item.soft_IdDvt, item.soft_IdGroupProduct,
                    item.soft_Barcode, item.soft_GiaTC, item.soft_GiaM,
                    false)
                var npp = ko.utils.arrayFirst(self.listTableNPP(), function (obj) {
                    return newmodel.soft_IdNPP === obj.Id;
                });
                var dvt = ko.utils.arrayFirst(self.listTableDvt(), function (obj) {
                    return newmodel.soft_IdDvt === obj.Id;
                });
                var gproduct = ko.utils.arrayFirst(self.listGroupProduct(), function (obj) {
                    return newmodel.soft_IdGroupProduct === obj.Id;
                });
                newmodel.TenNPP = npp ? npp.TenNPP : "Chưa có.";
                newmodel.TenDvt = dvt ? dvt.Dvt : "Chưa có.";
                newmodel.Tenloai = gproduct ? gproduct.tenloai : "Chưa có.";

                tmplistTable.push(newmodel);
            });
            if (tmplistTable.length > 0) {
                $(".dataTable_product").css({ 'display': 'block' });
                self.listTable(tmplistTable);
            }
            else
                $(".dataTable_product").css({ 'display': 'none' });

            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    var viewModel = {
        readonlyTemplate: ko.observable("readonlyTemplate_product"),
        editTemplate: ko.observable()
    };
    viewModel.currentTemplate = function (tmpl) {
        return tmpl === this.editTemplate() ? 'editTemplate_product' :
         this.readonlyTemplate();
    }.bind(viewModel);
    viewModel.reset = function (t) {
        this.editTemplate("readonlyTemplate_product");
    };
    function typeModel(id, masp, tensp, soft_IdNPP, soft_IdDvt, soft_IdGroupProduct,
        soft_Barcode, soft_GiaTC, soft_GiaM,
        isNew) {
        return {
            id: id ? id : 0,
            masp: masp ? masp : "",
            tensp: tensp ? tensp : "",
            soft_IdNPP: soft_IdNPP ? soft_IdNPP : 0,
            soft_IdDvt: soft_IdDvt ? soft_IdDvt : 0,
            soft_IdGroupProduct: soft_IdGroupProduct ? soft_IdGroupProduct : 0,
            soft_Barcode: soft_Barcode ? soft_Barcode : "",
            soft_GiaTC: soft_GiaTC ? soft_GiaTC : 0,
            soft_GiaM: soft_GiaM ? soft_GiaM : 0,
            isNew: isNew,
            TenNPP: "",
            TenDvt: "",
            Tenloai: "",

        }
    };
    viewModel.addnewRecord = function () {
        var abc = new typeModel(0, "", "", 0, 0, 0, "", 0, 0, true);
        self.listTable.push(abc);
        IsNewRecord = true; //set the check for the new record
        this.editTemplate(abc);
        //debugger
        //var abc = new typeModel(0, "", "", 0, 0, 0, true);
        //var tmplst = self.listTable();
        //tmplst.push(abc);
        //self.listTable(tmplst);
        //IsNewRecord = true;
        //this.editTemplate(abc);


    };
    viewModel.delete = function (d) {
        Utils.openConfirm('Xóa sản phẩm này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/Product/Remove/" + d.id
            }).done(function (data) {
                viewModel.reset();
                loadList(self.chosenGroupProduct(), self.chosenGroupNPP());
                Utils.showWait(false);
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function () {
                viewModel.reset();
                Utils.showWait(false);
            });
        });
    };
    //datatables waring : Requested unknown parameter '1' from the data socrce for row 9
    viewModel.save = function (mdata) {
        debugger
        mdata.soft_IdDvt = self.selectedDvt();
        mdata.soft_IdGroupProduct = self.selectedGProduct();
        mdata.soft_IdNPP = self.selectedNPP();

        if (IsNewRecord === false) {
            $.ajax({
                type: "PUT",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: "/Product/Edit",
                data: ko.toJSON({ model: mdata }),
            }).done(function (data) {
                viewModel.reset();
                //  loadList(self.chosenGroupProduct());
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function (err) {
                viewModel.reset();
            });
        }
        if (IsNewRecord === true) {
            IsNewRecord = false;
            $.ajax({
                type: "POST",
                url: "/Product/Add",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON({ model: mdata }),
            }).done(function (data) {
                viewModel.reset();
                //  loadList(self.chosenGroupProduct());
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function (err) {
                viewModel.reset();
            });
        };
    };
    self.viewmodel = ko.observable(viewModel);
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
    self.selectedNPP = ko.observable();
    self.listTableNPP = ko.observableArray();
    self.selectedDvt = ko.observable();
    self.listTableDvt = ko.observableArray();
    self.selectedGProduct = ko.observable();
    self.selectChanged_GroupProduct = function (data, event) {
        var k = $(event.target).attr("index");
        var Id = parseInt(k);
        if (k) {
            var tmpIndex = ko.utils.arrayFirst(self.listGroupProduct(), function (obj) {
                return obj.Id === parseInt(k);
            });
            if (tmpIndex)
                data.Tenloai = tmpIndex.tenloai;
        }
    };
    self.selectChanged_NPP = function (data, event) {
        var k = $(event.target).attr("index");
        if (k) {
            var tmpIndex = ko.utils.arrayFirst(self.listTableNPP(), function (obj) {
                return obj.Id === parseInt(k);
            });
            if (tmpIndex)
                data.TenNPP = tmpIndex.TenNPP;
        }
    };
    self.selectChanged_Dvt = function (data, event) {
        var k = $(event.target).attr("index");
        if (k) {
            var tmpIndex = ko.utils.arrayFirst(self.listTableDvt(), function (obj) {
                return obj.Id === parseInt(k);
            });
            if (tmpIndex)
                data.TenDvt = tmpIndex.Dvt;
        }
    };
    //*********************Bind******************************************
    viewModel.editTemplate.subscribe(function (val) {
        self.selectedNPP(val.soft_IdNPP > 0 ? val.soft_IdNPP : null);
        self.selectedDvt(val.soft_IdDvt > 0 ? val.soft_IdDvt : null);
        self.selectedGProduct(val.soft_IdGroupProduct > 0 ? val.soft_IdGroupProduct : null);
    });
    //*********************Load extend***********************************
    Utils.loadListNPP(function (data) {
        self.listTableNPP(data);
    });
    Utils.loadGroupProduct(function (data) {
        self.listGroupProduct(data);
    });
    Utils.loadListDvt(function (data) {
        self.listTableDvt(data);
    });
    //**************************************************************
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('productViewId'));
    };
};

