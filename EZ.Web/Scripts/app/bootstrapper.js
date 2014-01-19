define('bootstrapper',
    ['jquery', 'config', 'route-config', 'presenter', 'dataprimer', 'binder'],
    function ($, config, routeConfig, presenter, dataprimer, binder) {
        var
            run = function () {
                presenter.toggleActivity(true); //let user know that something happening

                config.dataserviceInit();

                $.when(dataprimer.fetch())                  //go get the data
                    .done(binder.bind)                      //if done, bind 'em
                    .done(routeConfig.register)             //setup routes for navigation
                    .always(function () {
                        presenter.toggleActivity(false);
                    });
            };

        return {
            run: run
        };
    });