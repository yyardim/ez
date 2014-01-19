define('dataservice.ez',
    ['amplify'],
    function(amplify) {
        var
            init = function() {
                amplify.request.define('ezs', 'ajax', {
                    url: '/api/ezs',
                    dataType: 'json',
                    type: 'GET'
                });

                amplify.request.define('ez', 'ajax', {
                    url: '/api/ezs/{id}',
                    dataType: 'json',
                    type: 'GET'
                });
            },

            getEzs = function(callbacks) {
                return amplify.request({
                    resourceId: 'ezs',
                    success: callbacks.success,
                    error: callbacks.error
                });
            },

            getEz = function(callbacks, id) {
                return amplify.request({
                    resourceId: 'ez',
                    data: { id: id },
                    success: callbacks.success,
                    error: callbacks.error
                });
            };

        init();

        return {
            getEzs: getEzs,
            getEz: getEz
        };
    });