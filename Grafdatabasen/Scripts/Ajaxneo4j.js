
function ajaxneo4j(querymatch){
    // The query
    //var querymatch = ;"match path= (n) return  n"
    var query = {
        "statements": [{
            "statement": "" + querymatch + "",
            "resultDataContents": ["graph", "row"]
        }]
    };

    var graphen;

    //the helper function provided by neo4j documents
    function idIndex(a, id) {
        for (var i = 0; i < a.length; i++) {
            if (a[i].id == id) return i;
        }
        return null;
    }
    //sessionStorage.clear();
    // jQuery ajax call
    var result;
    var request = $.ajax({
        type: "POST",
        async: false,
        url: "http://localhost:7474/db/data/transaction/commit",
        accepts: { json: "application/json" },
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(query)
    });
    return request;

}