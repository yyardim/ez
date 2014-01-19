define('route-config',
    ['config', 'router', 'vm'],
    function (config, router, vm) {
        var
            logger = config.logger,

            register = function () {

                var routeData = [

                    // Person routes
                    {
                        view: config.viewIds.person,
                        routes:
                            [{
                                route: config.hashes.persons,
                                title: 'Persons',
                                callback: vm.persons.activate,
                                group: '.route-top'
                            }]
                    },

                    // Person details routes
                    {
                        view: config.viewIds.person,
                        route: config.hashes.persons+ '/:id',
                        title: 'Person',
                        callback: vm.person.activate,
                        group: '.route-left'
                    },

                    // Ez and ez details routes
                    {
                        view: config.viewIds.ezs,
                        route: config.hashes.ezs,
                        title: 'ezs',
                        callback: vm.ezs.activate,
                        group: '.route-top'
                    }, {
                        view: config.viewIds.event,
                        route: config.hashes.ezs+ '/:id',
                        title: 'Ez',
                        callback: vm.event.activate
                    },

                    // Invalid routes
                    {
                        view: '',
                        route: /.*/,
                        title: '',
                        callback: function () {
                            logger.error(config.toasts.invalidRoute);
                        }
                    }
                ];

                for (var i = 0; i < routeData.length; i++) {
                    router.register(routeData[i]);
                }

                // Crank up the router
                router.run();
            };


        return {
            register: register
        };
    });