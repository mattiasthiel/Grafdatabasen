
function ajaxsearch(querymatch) {
    // The query
    //var querymatch = ;"match path= (n) return  n"
    var query = {
        "statements": [{
            "statement": "" + querymatch + "",
            "resultDataContents": ["row"]
        }]
    };

    //the helper function provided by neo4j documents
    function idIndex(a, id) {
        for (var i = 0; i < a.length; i++) {
            if (a[i].id == id) return i;
        }
        return null;
    }
    // jQuery ajax call
    var result;
    var request = $.ajax({
        type: "POST",
        async: false,
        url: "http://localhost:7474/db/data/transaction/commit",
        accepts: { json: "application/json" },
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(query),
        //now pass a callback to success to do something with the data
        success: function (data) {
            result = data.row[0];
        }

    });
    return request;

}