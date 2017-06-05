KHO_Order_Product.mvKHO_Order_Input = function () {
    var self = this;
    self.mKHO_Input = ko.observable(new KHO_Order_Product.mKHO_Order_Input);
    self.mKHO_Input_lst = ko.observableArray();
    self.Barcode = ko.observable();
    self.IsOrdering = ko.observable(false);
    self.IsOrdering.subscribe(function (v) {
        if (!v)
            self.LoadListOrderInput();
    });
    self.IsPrintOrdering = ko.observable(false);
    self.IsPrintOrdering.subscribe(function (val) {
        if (val)
            self.PrintOrder();
    })
    self.Barcode.subscribe(function (val) {
        if (val.length > 0) {
            Utils.showWait(true);
            $.ajax({
                type: "GET",
                url: Utils.url("/KHO_OrderInput/GetProductbyBarcode"),
                data: { barcode: val }
            }).done(function (data) {
                if (data == null)
                    return
                var inputChild = ko.observable(new KHO_Order_Product.mKHO_Order_Input_Child);
                inputChild().masp(data.masp);
                inputChild().tensp(data.tensp);
                inputChild().tendvt(data.tendvt);
                inputChild().IdDvt(data.IdDvt);

                inputChild().IdNPP(data.IdNPP);
                inputChild().IdProduct(data.IdProduct);
                inputChild().Barcode(data.barcode);
                inputChild().NPP(ko.mapping.fromJS(data.NPP, {}, new KHO_Order_Product.NPP));
                self.mKHO_Input().soft_KHO_Order_Input_Child.push(inputChild());
            }).always(function () {
                Utils.showWait(false);
            });
        }
        self.Barcode("");
    });
    self.Total = ko.computed(function () {
        var sum = 0;
        ko.utils.arrayForEach(self.mKHO_Input().soft_KHO_Order_Input_Child(), function (item) {
            sum += item.Total();
        });
        return sum;
    });
    self.Remove = function (val) {
        self.mKHO_Input().soft_KHO_Order_Input_Child.pop(val);
    };
    self.SaveOrderInput = function () {
        Utils.showWait(true);
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: "/KHO_OrderInput/SaveOrderInput",
            data: ko.toJSON({ model: self.mKHO_Input() }),
        }).done(function (data) {
            Utils.notify("Thông báo", data.messaging, data.success ? '' : 'error')
        }).always(Utils.showWait(false));
    };
    self.LoadListOrderInput = function () {
        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/KHO_OrderInput/GetLoadListOrderInput"),
        }).done(function (data) {
            if (data == null)
                return
            self.mKHO_Input_lst(Utils.MapArray(data, KHO_Order_Product.mKHO_Order_Input));
            ko.utils.arrayForEach(self.mKHO_Input_lst(), function (o) {
                o.ViewId("Store#" + o.Id() + " - " + Utils.dateshort(o.DateUpdate()));
            });
        }).always(function () {
            Utils.showWait(false);
        });
    };
    self.redirectToDetail = function (val) {
        if (val.Id()) {
            Utils.addTabDynamic(val.ViewId(), Utils.url('/KHO_OrderInput/RenderViewInput?id=' + val.Id()), '#contentX');
        }
        else
            Utils.notify("Thông báo", 'Dữ liệu không hợp lệ');
    };
    self.GetDetailOrderInput = function () {

        Utils.showWait(true);
        $.ajax({
            type: "GET",
            url: Utils.url("/KHO_OrderInput/GetLoadOrderInput"),
            data: { id: BagId }
        }).done(function (data) {
            if (data == null)
                return
            ko.mapping.fromJS(data, {}, self.mKHO_Input);
            self.mKHO_Input().ViewId("Store#" + data.Id + " - " + Utils.dateshort(data.DateUpdate));

        }).always(function () {
            Utils.showWait(false);
        });
    };

    self.listPrintOrder = ko.observableArray();
    self.PrintOrder = function () {

        var group_to_values = self.mKHO_Input().soft_KHO_Order_Input_Child().reduce(function (obj, item) {
            obj[item.IdNPP()] = obj[item.IdNPP()] || [];
            obj[item.IdNPP()].push(item);
            return obj;
        }, {});
        var groups = Object.keys(group_to_values).map(function (key) {
            return { group: key, product: group_to_values[key] };
        });
        self.listPrintOrder(groups);
    };
    self.StartDetail = function () {
        ko.applyBindings(self, document.getElementById('kho_Order_InputDetailViewId'));
        self.GetDetailOrderInput();
    };
    self.Start = function () {
        ko.applyBindings(self, document.getElementById('kho_Order_InputViewId'));
        self.LoadListOrderInput();
    };
    self.Print = function (val) {
        var el = "printorder_" + val.group;
        var contents = document.getElementById(el).innerHTML;
        var frame1 = document.createElement('iframe');
        frame1.name = "frame1";
        frame1.style.position = "absolute";
        frame1.style.top = "-1000000px";
        document.body.appendChild(frame1);
        var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
        frameDoc.document.open();
        frameDoc.document.write('<html><head><title>DIV Contents</title>');
        frameDoc.document.write('</head><body>');
        frameDoc.document.write(contents);
        frameDoc.document.write('</body></html>');
        frameDoc.document.close();
        setTimeout(function () {
            window.frames["frame1"].focus();
            window.frames["frame1"].print();
            document.body.removeChild(frame1);
        }, 500);
        return false;

    }
};