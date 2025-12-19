function truncate(text, max = 15) {
    return text.length > max ? text.substring(0, max) + "…" : text;
}
let topErrorChart = null;
let errorParetoChart = null;
function renderTopErrorChart(series = [1], labels = [""]) {
    var total = series.reduce((acc, cur) => acc + cur, 0);
    if (total == 0) {
        series = [1];
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
                    //width: 200
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

function renderErrorParetoChart(data = [], labels = []) {
    var totalErrors = data.reduce((acc, cur) => acc + cur, 0);

    const cumulativeError = data.reduce((acc, cur) => {
        acc.push((acc.at(-1) ?? 0) + cur);
        return acc;
    }, []);
    var cumulativePercentage = cumulativeError.map(x => +((x / totalErrors) * 100).toFixed(2));
    const el = document.querySelector("#errorParetoChart");
    if (errorParetoChart) {
        errorParetoChart.destroy();
    }
    const shortLabels = labels.map(l => truncate(l, 15));
    var options = {
        series: [{
            name: 'Số lượng lỗi',
            type: 'column',
            data: data
        }, {
            name: '% tích lũy',
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
        labels: shortLabels,
        yaxis: [{
            title: {
                text: 'Số lượng lỗi',
                style: {
                    fontFamily: "var(--bs-body-font-family)",
                },
            },
        }, {
            opposite: true,
            title: {
                text: '% tích lũy',
                style: {
                    fontFamily: "var(--bs-body-font-family)",
                },
            }
        }],
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    //width: 300
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    errorParetoChart = new ApexCharts(el, options);
    errorParetoChart.render();
}

renderTopErrorChart();
renderErrorParetoChart();