define('model.person',
    ['ko', 'config'],
    function(ko, config) {
        var
            _dc = null,

            settings = {
                imageBasePath: '../content/images/photos/',
                unknownPersonImageSource: 'unknown_person.jpg'
            },

            Person = function() {
                var self = this;
                self.id = ko.observable();
                self.firstName = ko.observable().extend({ required: true });
                self.lastName = ko.observable().extend({ required: true });
                self.fullName = ko.computed(function() {
                    return self.firstName() + " " + self.lastName();
                }, self);

                self.email = ko.observable().extend({ email: true });
                self.gender = ko.observable();
                self.imageSource = ko.observable();
                self.bio = ko.observable();

                self.isNullo = false;

                self.DirtyFlag = new ko.DirtyFlag([
                    self.firstName,
                    self.lastName,
                    self.email,
                    self.bio
                ]);

                return self;
            };

        Person.Nullo = new Person()
            .id(0)
            .firstName('Not a')
            .lastName('Person')
            .email('')
            .gender('F')
            .imageSource('')
            .bio('');

        Person.Nullo.isNullo = true;
        Person.Nullo.DirtyFlag().reset();

        Person.datacontext = function(dc) {
            if (dc) {
                _dc = dc;
            }

            return _dc;
        };

        return Person;

    });