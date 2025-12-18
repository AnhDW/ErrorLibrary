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

function renderErrorCharetoChart() {
    var options = {
        series: [{
            name: 'Số lỗi',
            type: 'column',
            data: [170, 160, 100, 70, 60]
        }, {
            name: '% cộng dồn',
            type: 'line',
            data: [30.91, 60.00, 78.18, 90.91, 100]
        }],
        chart: {
            width: 500,
            type: 'line',
            toolbar: {
                show: false
            }
        },
        stroke: {
            width: [0, 4]
        },
        dataLabels: {
            enabled: true,
            enabledOnSeries: [1]
        },
        labels: ['Hỏng máy', 'Lỗi sản phẩm', 'Thiếu nguyên liệu', 'Chậm giao hàng', 'Lỗi quy trình'],
        yaxis: [{
            title: {
                text: 'Số lỗi',
                style: {
                    fontFamily: "var(--bs-body-font-family)",
                },
            },
        }, {
            opposite: true,
            title: {
                text: '% cộng dồn',
                style: {
                    fontFamily: "var(--bs-body-font-family)",
                },
            }
        }],
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 300
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#errorParetoChart"), options);
    chart.render();
}

renderTopErrorChart();
renderErrorCharetoChart();