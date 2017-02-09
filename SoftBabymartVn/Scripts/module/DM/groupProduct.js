var GroupProduct = GroupProduct || {};
GroupProduct = function () {
    var self = this;
    var IsNewRecord = false;
    self.listTable = ko.observableArray([]);
    function loadList() {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/GroupProduct/GetList"
        }).done(function (data) {
            self.listTable(ko.utils.arrayMap(data, function (item) {
                return new typeModel(item.Id, item.tenloai, false);
            }));
            Utils.showWait(false);

        }).fail(function () {
            Utils.showWait(false);
        });
    };
    var viewModel = {
        readonlyTemplate: ko.observable("readonlyTemplate_productgroup"),
        editTemplate: ko.observable()
    };
    viewModel.currentTemplate = function (tmpl) {
        return tmpl === this.editTemplate() ? 'editTemplate_productgroup' :
         this.readonlyTemplate();
    }.bind(viewModel);
    viewModel.reset = function (t) {
        this.editTemplate("readonlyTemplate_productgroup");
    };
    function typeModel(Id, tenloai, isNew) {
        return {
            Id: Id,
            tenloai: tenloai,
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
        Utils.openConfirm('Xóa nhóm hàng hóa này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/GroupProduct/Remove/" + d.Id
            }).done(function (data) {
                viewModel.reset();
                loadList();
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
                Utils.showWait(false);
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
                url: "/GroupProduct/Edit",
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
                url: "/GroupProduct/Add",
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
        ko.applyBindings(self, document.getElementById('groupProductViewId'));
    };
};

