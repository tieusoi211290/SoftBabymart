var NPP = NPP || {};
NPP = function () {
    var self = this;
    var IsNewRecord = false;
    self.listTable = ko.observableArray([]);
    function loadList() {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: "/NPP/GetList"
        }).done(function (data) {
            self.listTable(ko.utils.arrayMap(data, function (item) {
                return new typeModel(item.Id, item.TenNPP, item.Diachi,
                    item.Dienthoai, item.Email, item.Thue, item.TkBank, false);
            }));
            Utils.showWait(false);
        }).fail(function () {
            Utils.showWait(false);
        });
    };
   
    var viewmodel = {
        readonlyTemplate: ko.observable("readonlyTemplate_npp"),
        editTemplate: ko.observable()
    };
    viewmodel.currentTemplate = function (tmpl) {
        return tmpl === this.editTemplate() ? 'editTemplate_npp' :
         this.readonlyTemplate();
    }.bind(viewmodel);
    viewmodel.reset = function (t) {
        this.editTemplate("readonlyTemplate_npp");
    };
    function typeModel(Id, TenNPP, Diachi, Dienthoai, Email, Thue, TkBank, isNew) {
        return {
            Id: Id,
            TenNPP: TenNPP,
            Diachi: Diachi,
            Dienthoai: Dienthoai,
            Email: Email,
            Thue: Thue,
            TkBank: TkBank,
            isNew: isNew
        }
    };
    viewmodel.addnewRecord = function () {
        var abc = new typeModel(0, "","","","","","", true);
        self.listTable.push(abc);
        IsNewRecord = true; //Set the Check for the New Record
        this.editTemplate(abc);
    };
    viewmodel.delete = function (d) {
        Utils.openConfirm('Xóa nhà phân phối này ?', function () {
            Utils.showWait(true);
            $.ajax({
                type: "DELETE",
                url: "/NPP/Remove/" + d.Id
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
                url: "/NPP/Edit",
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
                url: "/NPP/Add",
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
        ko.applyBindings(self, document.getElementById('nppViewId'));
    };
};

