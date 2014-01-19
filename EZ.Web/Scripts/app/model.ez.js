define('model.ez',
    ['ko', 'config'],
    function(ko, config) {
        var Ez = function() {
            var self = this;
            self.id = ko.observable();
            self.name = ko.observable().extend({ required: true });
            self.description = ko.observable();
            self.ezDateTime = ko.observable();

            self.sessionHash = ko.computed(function() {
                return config.hashes.ezs + '/' + self.id();
            });

            self.isBrief = ko.observable(true);
            self.DirtyFlag = new ko.DirtyFlag([
                self.name,
                self.description
            ]);

            return self;
        };

        Ez.Nullo = new Ez()
            .id(0)
            .name('Not an EZ')
            .description('');
        Ez.Nullo.isNullo = true;
        Ez.Nullo.isBrief = function () { return false; }; // nullo is never brief
        Ez.Nullo.dirtyFlag().reset();

        var _dc = null;
        // Static member, no access to instances of Ez
        Ez.datacontext = function (dc) {
            if (dc) { _dc = dc; }
            return _dc;
        };

        // Prototype is available to all instances
        // It has access to the properties of the instance of Ez
        Ez.prototype = function() {
            var
                dc = Ez.datacontext;

            return {
                isNullo: false
            };
        }();

        return Ez;
    });