
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
        data: JSON.stringify(query),
        //now pass a callback to success to do something with the data
        success: function (data) {
            result = data;
            /*
            sessionStorage.setItem('data', JSON.stringify(data.results[0].data));
            console.log(data);
            var nodes = [], links = [];
            // parsing the output of neo4j rest api
            data.results[0].data.forEach(function (row) {
                row.graph.nodes.forEach(function (n) {
                    if (idIndex(nodes, n.id) == null) {
                        if (n.labels == "Konsult") {
                            $("#KonsultNamn").append($("<option>").text(n.properties.Namn).val(n.properties.Namn).attr('Namn', n.id));
                        }

                        nodes.push({ id: n.id, label: n.labels[0], Namn: n.properties.Namn });
                    }
                });
                links = links.concat(row.graph.relationships.map(function (r) {
                    // the neo4j documents has an error : replace start with source and end with target
                    return { source: idIndex(nodes, r.startNode), target: idIndex(nodes, r.endNode), type: r.type };
                }));
            });
            graphen = { nodes: nodes, links: links };
            */
           
        }

    });
    return request;

}