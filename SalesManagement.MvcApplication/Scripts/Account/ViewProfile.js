$(function () {
    var login = $("#login").text().trim();
    var monthsAmount = 12;
    displayMonthlyChart();
    displayStatusChart();

    function displayMonthlyChart() {
        var monthlyChartUrl = "/Order/GetMonthlyOrderAmountStatistics";
        $.get(monthlyChartUrl, { employeeLogin: login, monthsAmount: monthsAmount }, function (recievedData) {
            var monthlyChartCategories = recievedData.dates;
            for (var i = 0; i < monthlyChartCategories.length; i++) {
                var date = moment(monthlyChartCategories[i], "d/M/YYYY");
                monthlyChartCategories[i] = date.format("MMM") + "'" + date.format("YY");
            }
            var categoriesValues = recievedData.amounts;

            
            $("#btnMonthlyAmountChart").click(function () {
                $('#salesChart').highcharts(monthlyOrderAmountChart);
                $("#btnStatusChart").addClass("notchosenbutton");
                $("#btnStatusChart").removeClass("chosenbutton");
                $("#btnMonthlyAmountChart").addClass("chosenbutton");
                $("#btnMonthlyAmountChart").removeClass("notchosenbutton");
            });

            var monthlyOrderAmountChart = {
                chart: {
                    type: 'column',
                    backgroundColor: '#FFEEDF'
                },
                title: {
                    text: 'Latest sales'
                },
                xAxis: {
                    categories: monthlyChartCategories,
                    labels: {
                        rotation: -45,
                        align: 'right',
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Order amount'
                    },
                    minTickInterval: 1
                },
                legend: {
                    enabled: false
                },
                tooltip: {
                    pointFormat: 'Sales: <b>{point.y:.0f} order(s)</b>',
                },
                series: [{
                    name: 'Sales',
                    data: categoriesValues,
                    color: '#FFB87D',
                    dataLabels: {
                        enabled: false,
                        rotation: -90,
                        color: '#FFFFFF',
                        align: 'right',
                        x: 4,
                        y: 10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 3px black'
                        }
                    }
                }]
            };

            $('#salesChart').highcharts(monthlyOrderAmountChart);
            $("#btnMonthlyAmountChart").addClass("chosenbutton");
            $("#btnStatusChart").addClass("notchosenbutton");
            
        }, "json");
    }

    function displayStatusChart() {
        var statusChartUrl = "/Order/GetStatusesPercentageStatistics";
        $.get(statusChartUrl, { employeeLogin: login, monthsAmount: monthsAmount }, function (data) {
            $("#btnStatusChart").click(function () {
                $('#salesChart').highcharts(statusChart);
                $("#btnMonthlyAmountChart").addClass("notchosenbutton");
                $("#btnMonthlyAmountChart").removeClass("chosenbutton");
                $("#btnStatusChart").addClass("chosenbutton");
                $("#btnStatusChart").removeClass("notchosenbutton");
            });

            for (var i = 0; i < data.length; i++) {
                var currentdataItem = data[i];
                var color = "red";
                switch (currentdataItem.Key) {
                    case "Paid":
                        color = "#FFBC6A";
                        break;
                    case "Unpaid":
                        color = "#FF6A6A";
                        break;
                    case "Delivered":
                        color = "#72FF6A";
                        break;
                    case "Refused":
                        color = "#B4B4B4";
                        break;
                }
                data[i] = { name: currentdataItem.Key, y: currentdataItem.Value, color: color };
            }
            data[0].sliced = true;

            var statusChart = {
                chart: {
                    backgroundColor: '#FFEEDF'
                },
                title: {
                    text: 'Order\'s status percentage'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            color: '#000000',
                            connectorColor: '#000000',
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Percentage',
                    data: data
                }]
            };
        }, "json");
    }
});