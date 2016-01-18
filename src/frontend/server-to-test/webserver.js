var http = require("http");
var dispatch = require("../lib/dispatch/dispatch");

var server = http.createServer(dispatch ({
    "/usuario": function (req, res) {
       res.text({
            nome:"tbrito",
            senha:"pwd",
            autenticado:true
        });
    }    
}));

server.listen(8082);
console.log("server no ar na porta 8082");