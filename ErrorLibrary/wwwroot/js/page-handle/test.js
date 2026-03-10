let defectColumn = [];
async function getDefectData() {
    var defects = (await getDefects()).result;
    console.log(defects);
    defectColumn = defects.map(x => ({ colId: x.id, field: x.code, headerName: x.name, editable: true }));
    console.log(defectColumn);
}
// Grid Options: Contains all of the Data Grid configurations
let data = [];
let changedRows = {};
async function initGrid() {
    await getDefectData(); // lấy dữ liệu defect trước
    const gridOptions = {
        rowData: data,
        columnDefs: [
            { field: "customer_style", editable: true, headerName: "Khách hàng/Mã hàng" },
            { field: "po", editable: true, headerName: "PO" },
            { field: "quantity", editable: true, headerName: "Số lượng" },
            {
                headerName: "Prefinal", children: [
                    {
                        headerName: "Tình trạng", children: [
                            { field: "preMinor", editable: true, headerName: "Nặng" },
                            { field: "preMajor", editable: true, headerName: "Nhẹ" }
                        ]
                    },
                    { field: "preDate1", editable: true, headerName: "Ngày kiểm lần 1" },
                    { field: "preResult1", editable: true, headerName: "Kết quả lần 1" },
                    { field: "preDate2", editable: true, headerName: "Ngày kiểm lần 2" },
                    { field: "preResult2", editable: true, headerName: "Kết quả lần 2" },
                    { field: "preDate3", editable: true, headerName: "Ngày kiểm lần 3" },
                    { field: "preResult3", editable: true, headerName: "Kết quả lần 3" },
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
        rowSelection: { mode: "multiRow" },
        onCellValueChanged: function (event) {
            const row = event.data;
            changedRows[row.id] = row;
        }
    };

    const myGridElement = document.querySelector('#myGrid');
    agGrid.createGrid(myGridElement, gridOptions);
}
function handleAddRow() {
    console.log(gridOptions.rowData)
}

function handleDeleteRow() {
    const selectedRowNodes = gridApi.getSelectedNodes();
    const selectedIds = selectedRowNodes.map(function (rowNode) {
        return rowNode.id;
    });
    data = data.filter(function (dataItem, index) {
        return selectedIds.indexOf("" + index) < 0;
    });
    console.log(data);
    gridApi.setGridOption("rowData", data);
}