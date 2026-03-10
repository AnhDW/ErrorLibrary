// Grid Options: Contains all of the Data Grid configurations
let data = [

]
let changedRows = {};
const gridOptions = {
    // Row Data: The data to be displayed.
    rowData: data,
    // Column Definitions: Defines the columns to be displayed.
    columnDefs: [
        { field: "customer_style", editable: true, headerName: "Khách hàng/Mã hàng"},
        { field: "po", editable: true, headerName: "PO"},
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
                { field: "preResult2", editable: true, headerName: "Kết quả lần 2" }
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
                { field: "finalResult2", editable: true, headerName: "Kết quả lần 2" }
            ]
        },
        { field: "remark", editable: true, headerName: "Ghi chú" },
        { field: "loiVai", editable: true, headerName: "Lỗi vãi/Lỗi sợi" },
        { field: "duongMay", editable: true, headerName: "Đường may cầm nhăn" },
        { field: "formDang", editable: true, headerName: "Form dáng: ve, lai, nẹp" },
        { field: "xepLy", editable: true, headerName: "Xếp ly" },
        { field: "ho", editable: true, headerName: "Hở" },
        { field: "leMi", editable: true, headerName: "Le mí" },
        { field: "doiXung", editable: true, headerName: "Đối xứng" },
        { field: "dunVan", editable: true, headerName: "Đùn vặn, xoắn, chúi, thấm" },
        { field: "bungSut", editable: true, headerName: "Bung sút đứt chỉ " },
        { field: "boMui", editable: true, headerName: "Bỏ mũi" },
        { field: "vscn", editable: true, headerName: "VSCN : sót chỉ , dính dơ, dính keo" },
        { field: "loiVeEpKeo", editable: true, headerName: "Lỗi về ép keo : keo bung, sai keo, sai vị trí ép…" },
        { field: "loiKhuyNut", editable: true, headerName: "Lỗi khuy nút : sai vị trí , lệch khuy-nút" },
        { field: "uiHan", editable: true, headerName: "Ủi hằn, cấn, vết ủi" },
        { field: "loiKhacVeUi", editable: true, headerName: "Lỗi khác về ủi" },
        { field: "loiDongGoi", editable: true, headerName: "Lỗi đóng gói: sai nhãn, thẻ bài…" },
        { field: "khacMau", editable: true, headerName: "Khác màu" },
        { field: "lechSoc", editable: true, headerName: "Lệch sọc " },
        { field: "thongSo", editable: true, headerName: "Thông số" },
        { field: "loKim", editable: true, headerName: "Lỗ kim" },
        { field: "thamChi", editable: true, headerName: "Thấm chỉ" },
        { field: "loiKhacVeMay", editable: true, headerName: "Lỗi khác về may" },
    ],
    rowSelection: {
        mode: "multiRow",
    },
    onCellValueChanged: function (event) {
        const row = event.data;
        console.log(row)
        // store changed row using id as key
        changedRows[row.id] = row;
    }
};

// Your Javascript code to create the Data Grid
const myGridElement = document.querySelector('#myGrid');
const gridApi = agGrid.createGrid(myGridElement, gridOptions);

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