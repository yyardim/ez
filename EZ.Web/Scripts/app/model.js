define('model',
    [
        'model.ez'
    ],
    function(ez) {
        var
            model = {
                Ez: ez
            };

        model.setDataContext = function(dc) {
            // Model's that have navigation properties need a reference to the datacontext
            model.Ez.datacontext(dc);
        };

        return model;
    });