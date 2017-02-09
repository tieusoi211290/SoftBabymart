var KHO = KHO || {};
KHO = function () {
    var self = this;
    var IsNewRecord = false;
    self.listTable = ko.observableArray([]);
    function loadList() {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/KHO/GetList"
        }).done(function (data) {
            self.listTable(ko.utils.arrayMap(data, function (item) {
                return new typeModel(item.Id, item.Kho, item.Diachi,
                    item.Lienhe, false);
            }));
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
    var viewmodel = {
        readonlyTemplate: ko.observable("readonlyTemplate_kho"),
        editTemplate: ko.observable()
    };
    viewmodel.currentTemplate = function (tmpl) {
        return tmpl === this.editTemplate() ? 'editTemplate_kho' :
         this.readonlyTemplate();
    }.bind(viewmodel);
    viewmodel.reset = function (t) {
        this.editTemplate("readonlyTemplate_kho");
    };
    function typeModel(Id, Kho, Diachi, Lienhe, isNew) {
        return {
            Id: Id,
            Kho: Kho,
            Diachi: Diachi,
            Lienhe: Lienhe,
            isNew: isNew
        }
    };
    viewmodel.addnewRecord = function () {
        var abc = new typeModel(0, "", "", "", true);
        self.listTable.push(abc);
        IsNewRecord = true; //Set the Check for the New Record
        this.editTemplate(abc);
    };
    viewmodel.delete = function (d) {
        Utils.openConfirm('Xóa kho này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/KHO/Remove/" + d.Id
            }).done(function (data) {
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
                viewmodel.reset();
                loadList();
                Utils.showWait(false);
            }).fail(function () {
                viewmodel.reset();
                Utils.showWait(false);
            });
        });
    };
    viewmodel.save = function (mdata) {
        if (IsNewRecord === false) {
            $.ajax({
                type: "PUT",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: "/KHO/Edit",
                data: ko.toJSON({ model: mdata }),
            }).done(function (data) {
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
                viewmodel.reset();
                loadList();
            }).fail(function (err) {
                viewmodel.reset();
            });
        }
        if (IsNewRecord === true) {
            IsNewRecord = false;
            $.ajax({
                type: "POST",
                url: "/KHO/Add",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON({ model: mdata }),
            }).done(function (data) {
                Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
                viewmodel.reset();
                loadList();
            }).fail(function (err) {
                viewmodel.reset();
            });
        };
    };
    self.viewmodel = ko.observable(viewmodel);
    self.Start = function () {
        loadList();
        ko.applyBindings(self, document.getElementById('khoViewId'));
    };
};

