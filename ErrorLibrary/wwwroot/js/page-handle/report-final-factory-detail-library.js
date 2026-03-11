let reportFinalFactoryId = 0;
function initialReportFinalFactoryDetailPage() {
    renderLineDropdown();
    $('#date').val(new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(0, 10));
    initGrid();
}

async function renderLineDropdown() {
    var organizations = (await getFactoriesOrganizationsDisplay()).result;
    console.log(organizations);
    $('#factoryDropdown').html(renderFactorySelectDropdown(organizations, 'id', 'factoryName'));
}

async function checkAndInitReportFinalFactory() {
    //tạo 1 in line nếu chưa có
    var factoryId = $('#factoryDropdown .select-input').attr('data-value');
    var createdDate = $('#date').val();
    console.log(date);
    var createReportFinalFactoryDto = {
        factoryId, createdDate
    }
    const res = (await checkInitReportFinalFactory(createReportFinalFactoryDto)).result;
    reportFinalFactoryId = res.id;
    data = await getByReportFinalFactory(reportFinalFactoryId);
    console.log(data);
    gridApi.setGridOption("rowData", data.result);

}

let defectColumn = [];
let data = [];
let changedRows = {};
let gridOptions;
let gridApi;
function createGridOptions(defectColumn) {
    return {
        getRowId: params => params.data.id,
        rowData: data,
        rowSelection: { mode: "multiRow" },
        columnDefs: [
            {
                headerName: "Khách hàng/Mã hàng", children: [
                    { field: "customerCode", editable: true, headerName: "Khách hàng" },
                    { field: "styleCode", editable: true, headerName: "Mã hàng" },
                ]
            },
            { field: "po", editable: true, headerName: "PO" },
            { field: "quantity", editable: true, headerName: "Số lượng" },
            {
                headerName: "Prefinal", children: [
                    {
                        headerName: "Tình trạng", children: [
                            { field: "preFinalMinor", editable: true, headerName: "Nặng" },
                            { field: "preFinalMajor", editable: true, headerName: "Nhẹ" }
                        ]
                    },
                    { field: "preFinalDate1", editable: true, headerName: "Ngày kiểm lần 1" },
                    { field: "preFinalResult1", editable: true, headerName: "Kết quả lần 1" },
                    { field: "preFinalDate2", editable: true, headerName: "Ngày kiểm lần 2" },
                    { field: "preFinalResult2", editable: true, headerName: "Kết quả lần 2" },
                    { field: "preFinalDate3", editable: true, headerName: "Ngày kiểm lần 3" },
                    { field: "preFinalResult3", editable: true, headerName: "Kết quả lần 3" },
                ]
            },
            {
                headerName: "Final", children: [
                    {
                        headerName: "Tình trạng", children: [
                            { field: "finalMinor", editable: true, headerName: "Nặng" },
                            { field: "finalMajor", editable: true, headerName: "Nhẹ" }
                        ]
                    },
                    { field: "finalDate1", editable: true, headerName: "Ngày kiểm lần 1" },
                    { field: "finalResult1", editable: true, headerName: "Kết quả lần 1" },
                    { field: "finalDate2", editable: true, headerName: "Ngày kiểm lần 2" },
                    { field: "finalResult2", editable: true, headerName: "Kết quả lần 2" },
                    { field: "finalDate3", editable: true, headerName: "Ngày kiểm lần 3" },
                    { field: "finalResult3", editable: true, headerName: "Kết quả lần 3" },
                ]
            },
            { field: "remark", editable: true, headerName: "Ghi chú" },
            { headerName: "Khuyết điểm", children: defectColumn }
        ],
        onCellValueChanged: function (event) {
            const row = event.data;
            changedRows[row.id] = row;
        }
    };
}

async function initGrid() {
    var defects = (await getDefects()).result;
    defectColumn = defects.map(x => ({ colId: x.id, field: x.code, headerName: x.name, editable: true }));
    
    gridOptions = createGridOptions(defectColumn);

    const myGridElement = document.querySelector('#myGrid');
    gridApi = agGrid.createGrid(myGridElement, gridOptions);
}

function addReportFinalFactoryDetail() {
    var newRow = { id: 0, customCode: "", styleCode: "", po: "", quantity: 0, preFinalMinor: 0, preFinalMajor: 0, preFinalDate1: new Date()}
    gridApi.setGridOption("rowData", [newRow]);
    
    console.log(gridApi);
    var reportFinalFactoryDetailDto = { reportFinalFactoryId, customerCode: 'Cus_Test', styleCode: 'Style_Test', po: 'PO_Test', quantity: 100 };
    createReportFinalFactoryDetail(reportFinalFactoryDetailDto).then(res => {
        console.log(res);
    });

}

$('#factoryDropdown').on('change', '.select-input', async function () {
    await checkAndInitReportFinalFactory();
});

$('#date').on('change keyup', async function () {
    await checkAndInitReportFinalFactory();
});
