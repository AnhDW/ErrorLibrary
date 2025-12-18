function truncate(text, max = 15) {
    return text.length > max ? text.substring(0, max) + "…" : text;
}
let topErrorChart = null;
function renderTopErrorChart(series = [1], labels = [""]) {
    var total = series.reduce((acc, cur) => acc + cur, 0);
    if (total == 0) {
        series = [1]
    }
    const el = document.querySelector("#topErrorChart");
    if (topErrorChart) {
        topErrorChart.destroy();
    }
    const shortLabels = labels.map(l => truncate(l, 15));
    var options = {
        series: series,
        chart: {
            width: 380,
            type: 'pie',
        },
        labels: shortLabels,
        tooltip: {
            custom: function ({ series, seriesIndex, w }) {
                return `<div class="text-gray p-1">${labels[seriesIndex]}</div>`;
            }
        },
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

    topErrorChart = new ApexCharts(el, options);
    topErrorChart.render();
}

function renderErrorCharetoChart(data = [170, 160, 100, 70, 60]) {
    var totalError = data.reduce((acc, cur) => acc + cur, 0);
    const cumulativeError = data.reduce((acc, cur) => {
        acc.push((acc.at(-1) ?? 0) + cur);
        return acc;
    }, []);
    var cumulativePercentage = cumulativeError.map(x => +((x / totalError) * 100).toFixed(2));
    var options = {
        series: [{
            name: 'Số lỗi',
            type: 'column',
            data: data
        }, {
            name: '% cộng dồn',
            type: 'line',
            data: cumulativePercentage
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