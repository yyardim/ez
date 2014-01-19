(function () {
    QUnit.config.testTimeout = 10000;

    var stringformat = QUnit.stringformat;

    module('Web API GET Endpoints respond successfully');

    var apiUrls = [
        '/api/addresses/',
        '/api/addresses/1',

        '/api/eventerz/',
        '/api/eventerz/?personId=1&eventid=2',

        '/api/ezs/',
        '/api/ezs/1',

        '/api/personaddresses/',
        '/api/personaddresses/?personId=1&addressid=2',

        '/api/persons/',
        '/api/persons/1'
    ];

    var apiUrlslen = apiUrls.length;

    // Test only that the Web API responded to the request with 'success'
    var endpointTest = function (url) {
        $.ajax({
            url: url,
            dataType: 'json',
            success: function (result) {
                //debugger;
                ok(true, 'GET succeeded for ' + url);
                ok(!!result, 'GET retrieved some data');
                start();
            },
            error: function (result) {
                ok(false,
                    stringformat('GET on \'{0}\' failed with status=\'{1}\': {2}',
                        url, result.status, result.responseText));
                start();
            }
        });
    };

    // Returns an endpointTest function for a given URL
    var endpointTestGenerator = function (url) {
        return function () { endpointTest(url); };
    };

    // Test each endpoint in apiUrls
    for (var i = 0; i < apiUrlslen; i++) {
        var apiUrl = apiUrls[i];
        asyncTest(
            'API can be reached: ' + apiUrl,
            endpointTestGenerator(apiUrl));
    };
})();