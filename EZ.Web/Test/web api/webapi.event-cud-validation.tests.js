(function () {

    var baseUrl = '/api/eventz';

    asyncTest("Update Event - Null Name - Throws Validation Error",
        function () {
            //ARRANGE
            var
                badTestEvent = {
                    eventId: 1,
                    eventCategoryId: 4,
                    eventDateTime: '2013-12-08 11:43:13.101',
                    name: null,
                    description: "Alcun e e di al essilio e che colui quale prieghi in. " +
                                  "In come nostri. E sogiacere né che facitore trapassare mortali" +
                                  "non sono sí novella che. Ammirabile pieno sua. Cosa e facitore " +
                                  "come il l'uomo dea quale sí la cosa dio medesimi non sua cose.",
                    addressId: 4,
                    isActive: 1,
                    isCheckInRequired: 1,
                    isPublic: 1,
                    maxGuests: 10,
                    dateCreated: '2013-12-07'
                },
                data = JSON.stringify(badTestEvent);

            //ACT
            $.ajax({
                type: 'PUT',
                url: baseUrl,
                data: data,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',

                // ASSERT
                success: function () {
                    ok(false, 'Validation did not catch the null name');
                    start();
                },
                error: function (result) {
                    debugger;
                    equal(result.responseText, '{\"event.Name\":\"Name is required!\"}', 'Validation caught the null name');
                    start();
                }
            });
        }
    );

})();