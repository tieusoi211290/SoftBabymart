var OrderChild = OrderChild || {};
OrderChild = function (IdOrder) {
    var self = this;
    var IsNewRecord = false;
    self.listTableOrderChild = ko.observableArray([]);
    function loadList() {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/KHO_OrderChild/GetListChild"),
            data: { id: IdOrder }
        }).done(function (data) {
            self.listTableOrderChild(ko.utils.arrayMap(data, function (item) {
                return new typeModelOrderChild(item.Id, item.IdOrder, item.IdProduct, item.IdNPP, item.TenNPP,
                item.Total_Product, item.Total_Day, item.Total_Order, item.Total_Money, item.Note,
                    item.product.tensp,
                false);

            }));
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    var viewModel = {
        readonlyTemplate: ko.observable("readonlyTemplate_kho_Order_Child-" + IdOrder),
        editTemplate: ko.observable()
    };
    viewModel.currentTemplate = function (tmpl) {
        return tmpl === this.editTemplate() ? 'editTemplate_kho_Order_Child-' + IdOrder :
         this.readonlyTemplate();
    }.bind(viewModel);
    viewModel.reset = function (t) {
        this.editTemplate("readonlyTemplate_kho_Order_Child-" + IdOrder);
    };

    viewModel.addnewRecord = function () {
        var abc = new typeModel(0, "", true);
        self.listTable.push(abc);
        IsNewRecord = true; //Set the Check for the New Record
        this.editTemplate(abc);
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
    self.EditView = function (data) {
        self.viewmodel().editTemplate(data);
    };
    viewModel.delete = function (d) {
        debugger
        Utils.openConfirm('Xóa sản phẩm này ra khỏi đơn hàng?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/KHO_OrderChild/Remove/" + d.Id
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
    self.Start = function () {
        loadList();
        ko.applyBindings(self, document.getElementById('kho_Order_ChildViewId-' + IdOrder));
    };
};

