(function (ko) {
    ko.bindingHandlers.dateshort = {
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            if (value) {
                var date;
                if (value.constructor === Date)
                    date = value;
                else
                    date = new Date(parseInt(value.substr(6)));

                $(element).text(CommonUtils.SetDayFormat(date, 'd'));

            }
            else
                $(element).text(null);
        }
    };

    ko.bindingHandlers.timeshort = {
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            if (value) {
                var date;
                if (value.constructor === Date)
                    date = value;
                else
                    date = new Date(parseInt(value.substr(6)));
                $(element).text(Globalize.format(date, 't'));
            }
            else
                $(element).text(null);
        }
    };

    ko.bindingHandlers.datelong = {
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            if (value) {
                var date;
                if (value.constructor === Date)
                    date = value;
                else
                    date = new Date(parseInt(value.substr(6)));

                $(element).text(CommonUtils.SetDayFormat(date, 'f'));

            }
            else
                $(element).text(null);
        }
    };
    ko.bindingHandlers.textMoney = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            $(element).text(Globalize.format(valueUnwrapped, 'n'));
        }
    };
    ko.bindingHandlers.textMoneyDefaultSymbol = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var format = Globalize.cultures['vi-VN'].default.numberFormat.currency.pattern[1];
            var symbol = Globalize.cultures['vi-VN'].default.numberFormat.currency.symbol;
            var rs = Globalize.format(valueUnwrapped, format);
            $(element).text(rs + ' ' + symbol);
        }
    };
    ko.bindingHandlers.moneyMaskWithSymbol = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var options = allBindingsAccessor().currencyMaskOptions || {};

            ko.utils.registerEventHandler(element, 'focusout', function () {
                var observable = valueAccessor();
                var val = $(element).val().replace(/[^0-9\,\.]/g, '');

                if (val == "") val = "0";

                var numericVal = Globalize.parseFloat(val);
                numericVal = isNaN(numericVal) ? null : numericVal;

                var dis = Globalize.format(numericVal, 'c')
                numericVal = Globalize.parseFloat(dis);

                observable('[-]');
                observable(numericVal);

                var value = ko.utils.unwrapObservable(valueAccessor());
                $(element).val(Globalize.format(value, 'c'));
            });

            var isValueUpdate = allBindingsAccessor().valueUpdate === "afterkeydown";

            ko.utils.registerEventHandler(element, 'focus', function () {

                var observable = valueAccessor();
                $(this).val(observable());
            });


            if (isValueUpdate) {
                ko.utils.registerEventHandler(element, 'keyup', function () {

                    var observable = valueAccessor();
                    var val = $(element).val().replace(/[^0-9\,\.]/g, '');

                    if (val == "") val = "0";

                    var numericVal = Globalize.parseFloat(val);
                    numericVal = isNaN(numericVal) ? null : numericVal;

                    var dis = Globalize.format(numericVal, 'c')
                    numericVal = Globalize.parseFloat(dis);

                    observable('[-]');
                    observable(numericVal);

                });
            }
        },

        update: function (element, valueAccessor) {
            if (!$(element).is(":focus")) {
                var value = ko.utils.unwrapObservable(valueAccessor());
                $(element).val(Globalize.format(value, 'c'));
            }
        }
    };
})(ko);