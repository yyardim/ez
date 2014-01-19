define('vm.ez',
    ['ko', 'datacontext', 'config', 'router', 'messenger', 'sort'],
    function(ko, datacontext, config, router, messenger, sort) {
        var
            currentEzId = ko.observable(),
            ez = ko.observable(),
            //ez
            isActive = ko.observable(),
            ezPersons = ko.observableArray(),
            
            //canEditEz = ko.computed(function() {
            //    return ez() && config.curentUser() && config.currentUser().id() === ez().creator logic.. missing
            //})

            getEz = function(completeCallback, forceRefresh) {
                var callback = function() {
                    if (completeCallback) {
                        completeCallback();
                    }
                };

                datacontext.ezs.getFullEzById(
                    currentEzId(), {
                        success: function(s) {
                            ez(s);
                            callback();
                        },
                        error: callback
                    },
                    forceRefresh
                );
            },

            tmplName = function() {
                //return canEditEz() ? 'ez.edit' : 'ez.view';
                return 'ez.view';
            };


        return {
            ez: ez,
            isActive: isActive
        };
    });