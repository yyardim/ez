define('dataprimer',
    ['ko', 'datacontext', 'config'],
    function (ko, datacontext, config) {

        var logger = config.logger,

            fetch = function () {

                return $.Deferred(function (def) {

                    var data = {
                        persons: ko.observableArray(),
                        ezs: ko.observableArray()
                        //addresses: ko.observableArray(),
                        //eventerz: ko.observableArray(),
                        //personaddresses: ko.observableArray()
                    };

                    $.when(
                        datacontext.persons.getData({ results: data.persons }),
                        datacontext.ezs.getData({ results: data.ez }),
                        //datacontext.addresses.getData({ results: data.addresses }),
                        //datacontext.eventerz.getData({ param: config.currentUserId, results: data.eventerz }),
                        //datacontext.personaddresses.getData({ param: config.currentUserId, results: data.addresses }),
                        datacontext.persons.getFullPersonById(config.currentUserId,
                            {
                                success: function (person) {
                                    config.currentUser(person);
                                }
                            }, true)
                    )
                    //after up is down, cache someting whatever
                    //.pipe(function () {
                    //    // Need sessions and speakers in cache before
                    //    // speakerSessions models can be made (client model only)
                    //    datacontext.speakerSessions.refreshLocal();
                    //})

                    .pipe(function () {
                        logger.success('Fetched data for: '
                            + '<div>' + data.persons().length + ' persons </div>'
                            + '<div>' + data.ez().length + ' ez </div>'
                            //+ '<div>' + data.addresses().length + ' addresses </div>'
                            //+ '<div>' + data.eventerz().length + ' eventerz </div>'
                            //+ '<div>' + data.personaddresses().length + ' personaddresses </div>'
                            + '<div>' + (config.currentUser().isNullo ? 0 : 1) + ' user profile </div>'
                        );
                    })

                    .fail(function () { def.reject(); })

                    .done(function () { def.resolve(); });

                }).promise();   //return
            };

        return {
            fetch: fetch
        };
    });