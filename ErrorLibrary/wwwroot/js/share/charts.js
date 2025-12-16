function renderTopErrorChart() {
    var options = {
        series: [25, 30, 45],
        chart: {
            width: 380,
            type: 'pie',
        },
        labels: ['Lỗi 1', 'Lỗi 2', 'Lỗi 3'],
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#topErrorChart"), options);
    chart.render();
}

renderTopErrorChart();