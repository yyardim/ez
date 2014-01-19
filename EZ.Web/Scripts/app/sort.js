define('sort', [], function() {

    var
        ezSort = function(ezA, ezB) {
            return ezA.eventDateTime() > ezB.eventDateTime() ? 1 : -1;
        },

        personSort = function(personA, personB) {
            return personA.lastName() > personB.lastName() ? 1 : -1;
        };

    return {
        ezSort: ezSort,
        personSort: personSort
    };
});