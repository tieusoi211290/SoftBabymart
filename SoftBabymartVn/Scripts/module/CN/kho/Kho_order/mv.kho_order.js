var KHO_Order = KHO_Order || {};
KHO_Order.Order = function () {
    var self = this;
    var IsNewRecord = false;
    self.listTable = ko.observableArray([]);
    function loadList() {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/KHO_Order/GetList"
        }).done(function (data) {
            self.listTable(ko.utils.arrayMap(data, function (item) {
                return new typeModel(item.Id, item.Note, item.DateOrder, item.Total, false);
            }));
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    var viewModel = {
        readonlyTemplate: ko.observable("readonlyTemplate_kho_Order"),
        editTemplate: ko.observable()
    };
    viewModel.currentTemplate = function (tmpl) {
        return tmpl === this.editTemplate() ? 'editTemplate_kho_Order' :
         this.readonlyTemplate();
    }.bind(viewModel);
    viewModel.reset = function (t) {
        this.editTemplate("readonlyTemplate_kho_Order");
    };
    function typeModel(Id, Note, DateOrder, Total, isNew) {
        return {
            Id: Id,
            Note: Note,
            DateOrder: DateOrder,
            Total: Total,
            isNew: isNew
        }
    };
    viewModel.addnewRecord = function () {
        var abc = new typeModel(0, "", true);
        self.listTable.push(abc);
        IsNewRecord = true; //Set the Check for the New Record
        this.editTemplate(abc);
    };
    viewModel.delete = function (d) {
        Utils.openConfirm('Xóa đơn hàng này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/KHO_Order/Remove/" + d.Id
            }).done(function (data) {
                viewModel.reset();
                loadList();
                Utils.showWait(false);
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function () {
                viewModel.reset();
                Utils.showWait(false);
            });
        });
    };
    viewModel.save = function (mdata) {
        if (IsNewRecord === false) {
            $.ajax({
                type: "PUT",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: "/KHO_Order/Edit",
                data: ko.toJSON({ model: mdata }),
            }).done(function (data) {
                viewModel.reset();
                loadList();
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function (err) {
                viewModel.reset();
            });
        }
        if (IsNewRecord === true) {
            IsNewRecord = false;
            $.ajax({
                type: "POST",
                url: "/GroupCustomer/Add",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON({ model: mdata }),
            }).done(function (data) {
                viewModel.reset();
                loadList();
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
            }).fail(function (err) {
                viewModel.reset();
            });
        };
    };
    self.listTableOrderChild = ko.observableArray();
    viewModel.review = function (val) {
        if (val.Id) {
            Utils.addTabDynamic('Hóa đơn số' + val.Id,Utils.url('/KHO_OrderChild/RenderView?id=' + val.Id), '#tabcontentCN');
        }
        else
            Utils.notify("Thông báo", 'Dữ liệu không hợp lệ');
    };
    function typeModelOrderChild(Id, IdOrder, IdProduct, IdNPP, TenNPP,
        Total_Product, Total_Day, Total_Order, Total_Money, Note, Tensp,
        isNew) {
        return {
            Id: Id,
            IdOrder: IdOrder,
            IdProduct: IdProduct,
            IdNPP: IdNPP,
            TenNPP: TenNPP,
            Total_Product: Total_Product,
            Total_Day: Total_Day,
            Total_Order: Total_Order,
            Total_Money: Total_Money,
            Tensp: Tensp,
            Note: Note,
            isNew: isNew
        }
    };
    self.viewmodel = ko.observable(viewModel);
    self.Start = function () {
        loadList();
        ko.applyBindings(self, document.getElementById('kho_OrderViewId'));
    };
};

