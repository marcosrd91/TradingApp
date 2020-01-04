var TableData = [];
var ChartOperations = [];
var ChartDate = [];

var ChartOp = []

window.chartColors = {
    red: 'rgb(255, 99, 132)',
    orange: 'rgb(255, 159, 64)',
    yellow: 'rgb(255, 205, 86)',
    green: 'rgb(75, 192, 192)',
    blue: 'rgb(54, 162, 235)',
    purple: 'rgb(153, 102, 255)',
    grey: 'rgb(201, 203, 207)'
};
$(document).ready(function () {

        //convierto el input en datapicker
        $('#sandbox-container input').datepicker('update', new Date());


        //traigo el monto $ inicial del usuario
        
        $.ajax({
            url: "/api/InfoesApi/5",
            type: "GET", //send it through get method
            data: {
                Id: 4,

            },
            success: function (response)
            {
                if (response.MontoInicial != null) {

                    $(".PartialViewAmount").css("display", "none");
                    $(".initialAmount").text("Monto Inicial : " + response.MontoInicial);
                    $(".ActualAmount").text("Monto Actual : " + response.MontoActual);
                    ChartOp.push(response.MontoInicial)
                    ChartOperations.push(response.MontoInicial)
                }
            }  
            },
          );


        //obtengo todas las operaciones del usuario

        $.ajax({
            url: "/api/OperationsApi/5",
            type: "GET", //send it through get method
            data: {
                Id: 4,
            
            },
            success: function (response) {

                for (i = 0; i <= response.length - 1; i++) {
                    //load data for Grid
                    if (response[i].Tipo == "0") {
                        response[i].Tipo = "Long";
                    }
                    else {
                        response[i].Tipo = "Short";
                    }

                    TableData.push(response[i]);
                    //load data for chart




                    var Operationdate = response[i].Fecha.substr(0, 10);
                    ChartDate.push(Operationdate);

                    if (response[i].IsLost == false) {
                        var lastAmount = ChartOperations[ChartOperations.length - 1] + parseInt((response[i].Monto));
                 
                    }
                    else {
                        var lastAmount = ChartOperations[ChartOperations.length - 1] - parseInt((response[i].Monto));
                       
                    }

                    ChartOperations.push(parseInt((lastAmount)));
                }
                loadgrid();
                //CalculateChartMoney();
                loadChart();
            },
            error: function (xhr) {
                //Do Something to handle error
            }
        });





    function loadgrid() {
        var countries = [
            { Name: "", Id: 0 },
            { Name: "United States", Id: 1 },
            { Name: "Canada", Id: 2 },
            { Name: "United Kingdom", Id: 3 }
        ];

            $("#jsGrid").jsGrid({
   
            width: "100%",
            height: "400px",

            //inserting: true,
            //editing: true,
            //sorting: true,
            //paging: true,

            data: TableData,

            fields: [
                //{ name: "id", type: "number",width: 60, validate: "required" },
                { name: "Fecha", type: "text", width: 25 },
                { name: "Tipo", type: "text", width: 20 },
                { name: "Observaciones", type: "text", width: 60 },
                { name: "%", type: "number", width: 20 },
                { name: "Monto", type: "number", width: 20 },
              
                //{ name: "userId", type: "text", width: 60 },
                //{ name: "user", type: "text", width: 60 },
                //{ type: "control" }
            ]
        });

    }

    function loadChart() {
        var ctx = document.getElementById('myChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ChartDate,
                datasets: [{
                    label: 'Performance',
                  
                    backgroundColor: 
                        window.chartColors.green,
                    
                    borderColor: 
                        window.chartColors.green,
                    

                    borderWidth: 1,
                    data: ChartOperations,
                    fill: false,
                }],



            },
            options: {
                
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }});  



    

