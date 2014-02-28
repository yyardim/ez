define('binder',
    ['jquery', 'ko', 'config', 'vm'],
    function ($, ko, config, vm) {
        var
            ids = config.viewIds,

            bind = function () {
                ko.applyBindings(vm.shell, getView(ids.shellTop));
                ko.applyBindings(vm.persons, getView(ids.persons));
                ko.applyBindings(vm.ezs, getView(ids.ezs));
                //ko.applyBindings(vm.ezcategories, getView(ids.ezcategories));
                //ko.applyBindings(vm.addresses, getView(ids.addresses));
                //ko.applyBindings(vm.ezPersons, getView(ids.ezPersons));
                //ko.applyBindings(vm.personaddresses, getView(ids.personaddresses));
            },

            getView = function (viewName) {
                return $(viewName).get(0);
            };

        return {
            bind: bind
        };
    });