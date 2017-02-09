ko.bindingHandlers.textMoney = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);
        $(element).text(Globalize.format(valueUnwrapped, 'n'));
    }
};
ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker(options).on("changeDate", function (ev) {
            var observable = valueAccessor();
            observable(ev.date);
        });
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).datepicker("setValue", value);
    }
};
ko.bindingHandlers.dateshort = {
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value) {
            var date;
            if (value.constructor === Date)
                date = value;
            else
                date = new Date(parseInt(value.substr(6)));
            $(element).text(Globalize.format(date, 'd'));
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
            $(element).text(Globalize.format(date, 'f'));
        }
        else
            $(element).text(null);
    }
};

ko.bindingHandlers.selectPicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        if ($(element).is('select')) {
            if (ko.isObservable(valueAccessor())) {
                if ($(element).prop('multiple') && $.isArray(ko.utils.unwrapObservable(valueAccessor()))) {
                    // in the case of a multiple select where the valueAccessor() is an observableArray, call the default Knockout selectedOptions binding
                    ko.bindingHandlers.selectedOptions.init(element, valueAccessor, allBindingsAccessor);
                } else {
                    // regular select and observable so call the default value binding
                    ko.bindingHandlers.value.init(element, valueAccessor, allBindingsAccessor);

                }
            }
            $(element).addClass('selectpicker').selectpicker();
        }
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        if ($(element).is('select')) {
            var selectPickerOptions = allBindingsAccessor().selectPickerOptions;
            if (typeof selectPickerOptions !== 'undefined' && selectPickerOptions !== null) {
                var options = selectPickerOptions.optionsArray,
                  optionsText = selectPickerOptions.optionsText,
                  optionsValue = selectPickerOptions.optionsValue,
                  optionsCaption = selectPickerOptions.optionsCaption,
                  isDisabled = selectPickerOptions.disabledCondition || false,
                  resetOnDisabled = selectPickerOptions.resetOnDisabled || false;
                if (ko.utils.unwrapObservable(options).length > 0) {
                    // call the default Knockout options binding
                    ko.bindingHandlers.options.update(element, options, allBindingsAccessor);
                }
                if (isDisabled && resetOnDisabled) {
                    // the dropdown is disabled and we need to reset it to its first option
                    $(element).selectpicker('val', $(element).children('option:first').val());
                }
                $(element).prop('disabled', isDisabled);
            }
            if (ko.isObservable(valueAccessor())) {
                if ($(element).prop('multiple') && $.isArray(ko.utils.unwrapObservable(valueAccessor()))) {
                    // in the case of a multiple select where the valueAccessor() is an observableArray, call the default Knockout selectedOptions binding
                    ko.bindingHandlers.selectedOptions.update(element, valueAccessor);
                } else {
                    // call the default Knockout value binding
                    ko.bindingHandlers.value.update(element, valueAccessor);

                }
            }

            $(element).selectpicker('refresh');
        }
    }
};
ko.bindingHandlers.dataTable1 = {
    init: function (element, valueAccessor) {
        var binding = ko.utils.unwrapObservable(valueAccessor());

        // If the binding is an object with an options field,
        // initialise the dataTable with those options. 
        if (binding.options) {
            $(element).dataTable(binding.options);
        }
    },
    update: function (element, valueAccessor) {
        var binding = ko.utils.unwrapObservable(valueAccessor());

        // If the binding isn't an object, turn it into one. 
        if (!binding.data) {
            binding = { data: valueAccessor() }
        }

        // Clear table
        $(element).dataTable().fnClearTable();

        // Rebuild table from data source specified in binding
        $(element).dataTable().fnAddData(binding.data());
    }
};
