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
    setRowData();
    gridColumnApi.autoSizeAllColumns();
}

const formatVNDate = (value) => {
    if (!value) return "";

    const date = new Date(value);

    return new Intl.DateTimeFormat("vi-VN", {
        timeZone: "Asia/Ho_Chi_Minh",
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        //hour: "2-digit",
        //minute: "2-digit"
    }).format(date);
};
const dateColumn = {
    editable: true,
    cellEditor: "agDateCellEditor",
    valueFormatter: params => formatVNDate(params.value),
    valueParser: params => {
        if (!params.newValue) return null;
        return new Date(params.newValue).toISOString();
    }
};
function normalizeDate(date) {
    if (!date) return null;

    const d = new Date(date);

    if (d.getFullYear() <= 1) return null;

    return d.toISOString();
}

async function setRowData(){
    data = await getByReportFinalFactory(reportFinalFactoryId);
    console.log(data);
    result = data.result.map(x => ({
        ...x,
        preFinalDate1: new Date(x.preFinalDate1),
        preFinalDate2: new Date(x.preFinalDate2),
        preFinalDate3: new Date(x.preFinalDate3),
        finalDate1: new Date(x.finalDate1),
        finalDate2: new Date(x.finalDate2),
        finalDate3: new Date(x.finalDate3),
    }));
    gridApi.setGridOption("rowData", result);
}

let defectColumn = [];
let data = [];
let changedRows = {};
let gridOptions;
let gridApi;
let previewGridApi;
let resultOptions = [
    { id: 0, name: 'Pass' },
    { id: 1, name: 'Fail' }
];
function createGridOptions(defectColumn) {
    return {
        getRowId: params => params.data.id,
        rowData: data,
        //rowSelection: { mode: "multiRow" },
        columnDefs: [
            {
                headerName: "Khách hàng/Mã hàng", children: [
                    { field: "customerCode", editable: true, headerName: "Khách hàng", pinned: "left", width: 112 },
                    { field: "styleCode", editable: true, headerName: "Mã hàng", pinned: "left", width: 112 },
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
                    { field: "preFinalDate1", editable: true, headerName: "Ngày kiểm lần 1", ...dateColumn },
                    {
                        field: "preFinalResult1", editable: true, headerName: "Kết quả lần 1",
                        cellEditor: "agSelectCellEditor",
                        cellEditorParams: {
                            values: resultOptions.map(ro => ro.id),
                        },
                        valueFormatter: id => resultOptions.find(o => o.id === id.value)?.name
                    },
                    { field: "preFinalDate2", editable: true, headerName: "Ngày kiểm lần 2", ...dateColumn },
                    {
                        field: "preFinalResult2", editable: true, headerName: "Kết quả lần 2",
                        cellEditor: "agSelectCellEditor",
                        cellEditorParams: {
                            values: resultOptions.map(ro => ro.id),
                        },
                        valueFormatter: id => resultOptions.find(o => o.id === id.value)?.name
                    },
                    { field: "preFinalDate3", editable: true, headerName: "Ngày kiểm lần 3", ...dateColumn },
                    {
                        field: "preFinalResult3", editable: true, headerName: "Kết quả lần 3",
                        cellEditor: "agSelectCellEditor",
                        cellEditorParams: {
                            values: resultOptions.map(ro => ro.id),
                        },
                        valueFormatter: id => resultOptions.find(o => o.id === id.value)?.name
                    },
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
                    { field: "finalDate1", editable: true, headerName: "Ngày kiểm lần 1", ...dateColumn },
                    {
                        field: "finalResult1", editable: true, headerName: "Kết quả lần 1",
                        cellEditor: "agSelectCellEditor",
                        cellEditorParams: {
                            values: resultOptions.map(ro => ro.id),
                        },
                        valueFormatter: id => resultOptions.find(o => o.id === id.value)?.name
                    },
                    { field: "finalDate2", editable: true, headerName: "Ngày kiểm lần 2", ...dateColumn },
                    {
                        field: "finalResult2", editable: true, headerName: "Kết quả lần 2",
                        cellEditor: "agSelectCellEditor",
                        cellEditorParams: {
                            values: resultOptions.map(ro => ro.id),
                        },
                        valueFormatter: id => resultOptions.find(o => o.id === id.value)?.name
                    },
                    { field: "finalDate3", editable: true, headerName: "Ngày kiểm lần 3", ...dateColumn },
                    {
                        field: "finalResult3", editable: true, headerName: "Kết quả lần 3",
                        cellEditor: "agSelectCellEditor",
                        cellEditorParams: {
                            values: resultOptions.map(ro => ro.id),
                        },
                        valueFormatter: id => resultOptions.find(o => o.id === id.value)?.name
                    },
                ]
            },
            {
                field: "remark", editable: true, headerName: "Ghi chú",
                cellEditor: "agLargeTextCellEditor",
                cellEditorPopup: true,
                cellEditorParams: {
                    maxLength: 100
                }
            },
            { headerName: "Khuyết điểm", children: defectColumn },
            {
                headerName: "",
                width: 50,
                pinned: "right",
                cellRenderer: (params) => {
                    const e = document.createElement("i");
                    e.className = "fas fa-times";
                    e.style.color = "red";
                    e.style.cursor = "pointer";

                    e.onclick = () => {
                        deleteRow(params);
                    };

                    return e;
                }
            }
        ],
        onCellValueChanged: function (event) {
            const reportFinalFactoryDetailId = event.data.id;
            const columnId = event.column.getColId();
            const colId = event.column.getColId();

            const newValue = event.newValue;
            const oldValue = event.oldValue;

            if (colId.startsWith("defect_")) {

                const defectId = parseInt(colId.replace("defect_", ""));

                //update ReportFinalFactoryDetailDefect
                var reportFinalFactoryDetailDefectDto = { reportFinalFactoryDetailId, defectId, quantity: newValue }
                updateReportFinalFactoryDetailDefect(reportFinalFactoryDetailDefectDto).then(res => {
                    console.log(res);
                });
            }

            //update ReportFinalFactoryDetail
            console.log("Row:", reportFinalFactoryDetailId);
            console.log("Column:", columnId);
            console.log("Old:", oldValue);
            console.log("New:", newValue);
            const x = event.data;

            const reportFinalFactoryDetailDto = {
                ...x,
                reportFinalFactoryId,
                preFinalDate1: normalizeDate(x.preFinalDate1),
                preFinalDate2: normalizeDate(x.preFinalDate2),
                preFinalDate3: normalizeDate(x.preFinalDate3),
                finalDate1: normalizeDate(x.finalDate1),
                finalDate2: normalizeDate(x.finalDate2),
                finalDate3: normalizeDate(x.finalDate3),
            };
            console.log(reportFinalFactoryDetailDto);
            updateReportFinalFactoryDetail(reportFinalFactoryDetailDto).then(res => {
                console.log(res);
            });

            //if (columnId.startsWith("preFinalDate")) {
            //console.log("New:", newValue.toISOString());
            //}
                
            
        }
    };
}

async function initGrid() {
    var defects = (await getDefects()).result;
    //defectColumn = defects.map(x => ({ colId: x.id, field: x.code, headerName: x.name, editable: true }));
    defectColumn = defects.map(d => ({
        colId: "defect_" + d.id,
        headerName: d.name,
        headerTooltip: d.code,
        editable: true,
        width: 90,

        valueGetter: params => {
            const item = params.data.reportFinalFactoryDetailDefects
                ?.find(x => x.defectId === d.id);
            return item ? item.quantity : 0;
        },

        valueSetter: params => {
            let item = params.data.reportFinalFactoryDetailDefects
                ?.find(x => x.defectId === d.id);

            if (item) {
                item.quantity = Number(params.newValue);
            } else {
                params.data.reportFinalFactoryDetailDefects.push({
                    reportFinalFactoryDetailId: params.data.id,
                    defectId: d.id,
                    quantity: Number(params.newValue)
                });
            }

            return true; // báo grid refresh
        }
    }));
    console.log(defectColumn);
    gridOptions = createGridOptions(defectColumn);

    const myGridElement = document.querySelector('#reportFinalFactoryDetailGrid');
    gridApi = agGrid.createGrid(myGridElement, gridOptions);
}

function addReportFinalFactoryDetail() {
    if (reportFinalFactoryId === 0) {
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
    
    console.log(gridApi);
    var reportFinalFactoryDetailDto = { reportFinalFactoryId, customerCode: '', styleCode: '', po: '', quantity: 0 };
    createReportFinalFactoryDetail(reportFinalFactoryDetailDto).then(res => {
        setRowData();
    });

}
function deleteRow(params) {
    const rowNode = params.node;
    deleteReportFinalFactoryDetail(rowNode.id).then(res => {
        resToastr(res);
        gridApi.applyTransaction({
            remove: [params.data]
        });
    });
}

function onDownloadForm() {
    console.log('export');
    gridApi.exportDataAsExcel();
}
//show import model
function showImportModel() {
    if (reportFinalFactoryId === 0) {
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
}

//import handle
document.getElementById('importReportFinalFactories').addEventListener('change', function (e) {
    const file = e.target.files[0];
    const reader = new FileReader();

    reader.onload = function (event) {
        const data = new Uint8Array(event.target.result);
        const workbook = XLSX.read(data, { type: 'array' });

        // lấy danh sách sheet
        const sheetNames = workbook.SheetNames;

        let html = `<option value="" selected disabled>Chọn work sheet</option>`;
        sheetNames.forEach((item, index) => {
            html += `<option value="${index}">${item}</option>`;
        });

        $('#sheetSelect').html(html);

    };

    reader.readAsArrayBuffer(file);
});

document.getElementById('sheetSelect').addEventListener('change', function (e) {
    const worksheetIndex = $('#sheetSelect').val();
    const importModel = $('#importModel');
    const importModelChild = importModel.find('.modal-dialog');
    importModelChild.addClass('modal-xl');

    console.log(importModel);
    importReportFinalFactoryToExcel({ worksheetIndex: worksheetIndex }).then(async function (res) {
        console.log(res.result);
        var previewReportFinalFactoryDetailExcel = res.result;
        var html = await reportFinalFactoryDetailExcelPreview(previewReportFinalFactoryDetailExcel);
        $('#preview').html(html);

        var defects = (await getDefects()).result;
        defectColumn = defects.map(d => ({
            colId: "defect_" + d.id,
            headerName: d.name,
            headerTooltip: d.code,
            editable: true,
            width: 90,

            valueGetter: params => {
                const item = params.data.reportFinalFactoryDetailDefects
                    ?.find(x => x.defectId === d.id);
                return item ? item.quantity : 0;
            },

            valueSetter: params => {
                let item = params.data.reportFinalFactoryDetailDefects
                    ?.find(x => x.defectId === d.id);

                if (item) {
                    item.quantity = Number(params.newValue);
                } else {
                    params.data.reportFinalFactoryDetailDefects.push({
                        reportFinalFactoryDetailId: params.data.id,
                        defectId: d.id,
                        quantity: Number(params.newValue)
                    });
                }

                return true; // báo grid refresh
            }
        }));
        gridOptions = createGridOptions(defectColumn);
        const myGridElement = document.querySelector('#previewReportFinalFactoryDetailGrid');
        previewGridApi = agGrid.createGrid(myGridElement, gridOptions);

        result = res.result.excel.map(x => ({
            ...x,
            preFinalDate1: new Date(x.preFinalDate1),
            preFinalDate2: new Date(x.preFinalDate2),
            preFinalDate3: new Date(x.preFinalDate3),
            finalDate1: new Date(x.finalDate1),
            finalDate2: new Date(x.finalDate2),
            finalDate3: new Date(x.finalDate3),
        }));
        previewGridApi.setGridOption("rowData", result);
    });
});

$('#factoryDropdown').on('change', '.select-input', async function () {
    $('#formWrapper').removeClass('shake border border-2 border-danger p-2 rounded');
    await checkAndInitReportFinalFactory();
});

$('#date').on('change keyup', async function () {
    $('#formWrapper').removeClass('shake border border-2 border-danger p-2 rounded');
    await checkAndInitReportFinalFactory();
});

$("#toggleFormBtn").on("click", function () {
    const wrapper = document.getElementById("formWrapper");
    const icon = document.getElementById("toggleIcon");

    wrapper.classList.toggle("d-none");

    if (wrapper.classList.contains("d-none")) {
        this.innerHTML = '<i class="fas fa-angle-double-down"></i>';
    } else {
        this.innerHTML = '<i class="fas fa-angle-double-up"></i>';
    }
});
