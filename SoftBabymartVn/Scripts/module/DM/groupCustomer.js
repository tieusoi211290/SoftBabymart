var GroupCustomer = GroupCustomer || {};
GroupCustomer = function () {
    var self = this;
    var IsNewRecord = false;
    self.listTable = ko.observableArray([]);
    function loadList() {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/GroupCustomer/GetList"
        }).done(function (data) {
            self.listTable(ko.utils.arrayMap(data, function (item) {
                return new typeModel(item.Id, item.tennhom, false);
            }));
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    var viewModel = {
        readonlyTemplate: ko.observable("readonlyTemplate_groupCus"),
        editTemplate: ko.observable()
    };
    viewModel.currentTemplate = function (tmpl) {
        return tmpl === this.editTemplate() ? 'editTemplate_Kho_Order' :
         this.readonlyTemplate();
    }.bind(viewModel);
    viewModel.reset = function (t) {
        this.editTemplate("readonlyTemplate_groupCus");
    };
    function typeModel(Id, tennhom, isNew) {
        return {
            Id: Id,
            tennhom: tennhom,
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
        Utils.openConfirm('Xóa nhóm khách hàng này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/GroupCustomer/Remove/" + d.Id
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
                url: "/GroupCustomer/Edit",
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
    self.viewmodel = ko.observable(viewModel);
    self.Start = function () {
        loadList();
        ko.applyBindings(self, document.getElementById('groupCustomerViewId'));
    };
};

