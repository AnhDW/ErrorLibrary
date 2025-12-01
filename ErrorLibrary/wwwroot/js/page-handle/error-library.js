var errorParams = {
    errorGroupIds: [],
    errorCategoryIds: [],
    productCategoryIds: [],
    code: '',
    name: '',
    pageNumber: 1, pageSize: 20
}

//handle
async function addShowErrorModalHandle() {
    const errorGroups = await getErrorGroups();
    const errorGroupsHtml = renderSelectOptions(errorGroups, 'Chọn nhóm lỗi');
    const errorCategories = await getErrorCategories();
    const errorCategoriesHtml = renderSelectOptions(errorCategories, 'Chọn loại lỗi');
    const productCategories = await getProductCategories();
    const productCategoriesHtml = renderSelectOptions(productCategories, 'Chọn chủng loại sản phẩm');
    
    $('#addErrorGroupSelect').html(errorGroupsHtml);
    $('#addErrorCategorySelect').html(errorCategoriesHtml);
    $('#addProductCategorySelect').html(productCategoriesHtml);
}

async function editShowErrorModalHandle(errId) {
    const errorGroups = await getErrorGroups();
    const errorGroupsHtml = renderSelectOptions(errorGroups, 'Chọn nhóm lỗi');
    const errorCategories = await getErrorCategories();
    const errorCategoriesHtml = renderSelectOptions(errorCategories, 'Chọn loại lỗi');
    const productCategories = await getProductCategories();
    const productCategoriesHtml = renderSelectOptions(productCategories, 'Chọn chủng loại sản phẩm');

    var err = await getErrorById(errId);
    console.log(err);
    $('#editErrorGroupSelect').html(errorGroupsHtml);
    $('#editErrorCategorySelect').html(errorCategoriesHtml);
    $('#editProductCategorySelect').html(productCategoriesHtml);

    $('#editErrorId').val(err.id);
    $('#editErrorCode').val(err.code);
    $('#editErrorName').val(err.name);
    $('#editErrorType').val(err.errorCategory);
    $('#editErrorGroupSelect').val(err.errorGroupId);
    $('#editErrorCategorySelect').val(err.errorCategoryId);
    $('#editProductCategorySelect').val(err.productCategoryId);

}

function setAddErrorCode() {
    const errorGroupId = $('#addErrorGroupSelect').val();
    if (!errorGroupId || errorGroupId === '') {
        toastr.warning('Vui lòng chọn nhóm lỗi trước khi tạo mã lỗi');
        return;
    }
    generateErrorCode(errorGroupId).then(function (res) {
        $('#addErrorCode').val(res.result);
    });
}

async function setEditErrorCode() {
    var errorId = $('#editErrorId').val();
    var error = await getErrorById(errorId);
    console.log(error);
    const errorGroupId = $('#editErrorGroupSelect').val();
    if (!errorGroupId || errorGroupId === '') {
        toastr.warning('Vui lòng chọn nhóm lỗi trước khi tạo mã lỗi');
        return;
    }
    generateErrorCodeWhenUpdate(errorGroupId, error.code).then(function (res) {
        $('#editErrorCode').val(res.result);
    });
}

function handleAddError() {
    const errorGroupId = $('#addErrorGroupSelect').val();
    const errorCategoryId = $('#addErrorCategorySelect').val();
    const productCategoryId = $('#addProductCategorySelect').val();
    const code = $('#addErrorCode').val();
    const name = $('#addErrorName').val();

    const errorData = {
        errorGroupId,
        errorCategoryId,
        productCategoryId,
        code,
        name
    };
    addError(errorData).then(function (res) {
        //$('#addModel').modal('hide');
        //renderErrorTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleEditError() {
    const id = $('#editErrorId').val();
    const errorGroupId = $('#editErrorGroupSelect').val();
    const errorCategoryId = $('#editErrorCategorySelect').val();
    const productCategoryId = $('#editProductCategorySelect').val();
    const code = $('#editErrorCode').val();
    const name = $('#editErrorName').val();

    const errorData = {
        id,
        errorGroupId,
        errorCategoryId,
        productCategoryId,
        code,
        name
    };
    console.log(errorData);
    updateError(errorData).then(function (res) {
        $('#editModel').modal('hide');
        //renderErrorTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleDeleteError(id) {
    deleteError(id).then(function (res) {
        //renderErrorTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

async function renderErrorHeadFilter() {
    const [errorGroups, errorCategories, productCategories] = await Promise.all([
        getErrorGroups(),
        getErrorCategories(),
        getProductCategories()
    ]);

    $('#errorGroupsHeader').html(renderFilterByField(errorGroups, 'nhóm lỗi', 'ErrorGroup', 'id', 'name', 'error-group'));
    $('#errorCategoriesHeader').html(renderFilterByField(errorCategories, 'loại lỗi', 'ErrorCategory', 'id', 'name', 'error-category'));
    $('#productCategoriesHeader').html(renderFilterByField(productCategories, 'chủng loại sản phẩm', 'ProductCategory', 'id', 'name', 'product-category'));
    $('#errorCodesHeader').html(renderFilterByField([], 'mã lỗi', 'searchErrorCode', '', '', '', true));
    $('#errorNamesHeader').html(renderFilterByField([], 'tên lỗi', 'searchErrorName', '', '', '', true));
}

function renderErrorTable() {
    getErrorsPagination(errorParams).then(function (res) {
        let html = '';
        res.result.forEach(item => {
            html += `
                <tr id="row_${item.id}">
                    <td>${item.errorGroup == null ? '' : item.errorGroup.name}</td>
                    <td>${item.errorCategory == null ? '' : item.errorCategory.name}</td>
                    <td>${item.productCategory == null ? '' : item.productCategory.name}</td>
                    <td>${item.code}</td>
                    <td>${item.name}</td>
                    <td>
                        <div class="dropdown">
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                <i class="bx bx-dots-vertical-rounded"></i>
                            </button>
                            <div class="dropdown-menu">
                                <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick="editShowErrorModalHandle(${item.id})">
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteError(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>

                            </div>
                        </div>
                    </td>
                </tr>
                `;
        });
        $('#errorTableBody').html(html);
        renderPagination(res.paginationHeader, 'errorChangePage', 'errorPagination');
    });
}

function errorChangePage(page) {
    errorParams.pageNumber = page;
    renderErrorTable();
}

//import handle
document.getElementById('importErrors').addEventListener('change', function (e) {
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

var errorGroupNamesExcept = [];
var productCategoryNamesExcept = [];
var errorCategoryNamesExcept = [];
var errorExcel = [];

document.getElementById('sheetSelect').addEventListener('change', function (e) {
    const worksheetIndex = $('#sheetSelect').val();
    const importModel = $('#importModel');
    const importModelChild = importModel.find('.modal-dialog');
    importModelChild.addClass('modal-xl');

    console.log(importModel);
    importErrorsToExcel({ worksheetIndex: worksheetIndex }).then(async function (res) {
        var previewErrorExcel = res.result;
        var html = await errorExcelPreview(previewErrorExcel);
        $('#preview').html(html);
        errorGroupNamesExcept = previewErrorExcel.errorGroupNamesExcept;
        productCategoryNamesExcept = previewErrorExcel.productCategoryNamesExcept;
        errorCategoryNamesExcept = previewErrorExcel.errorCategoryNamesExcept;
        errorExcel = previewErrorExcel.excel;
    });
});

async function importErrorsExcel() {
    const worksheetIndex = $('#sheetSelect').val();
    if (!worksheetIndex) {
        toastr.warning('Bạn chưa chọn work sheet');
        return;
    }
    const results = await Promise.allSettled([
        addErrorGroupByNames(errorGroupNamesExcept),
        addProductCategoryByNames(productCategoryNamesExcept),
        addErrorCategoryByNames(errorCategoryNamesExcept)
    ]);
    resToastr(results[0].value);
    resToastr(results[1].value);
    resToastr(results[2].value);
    // format lại file excel theo error và insert ở đây
    /*await deleteAll();*/
    addErrorsToErrorExcelDto(errorExcel).then(function (res) {
        console.log(res);
        $('#importModel').modal('hide');
        renderErrorTable();
    });
}

function onDownloadForm() {
    window.location.href = '/import-form/Errors.xlsx';
}

//filter handle
$(document).on('change', `.error-group`, function () {
    let errorGroupIds = $(`.error-group:checked`).map(function () {
        return this.value;
    }).get();

    errorParams.errorGroupIds = errorGroupIds;
    renderErrorTable();
});

$(document).on('change', `.error-category`, function () {
    let errorCategoryIds = $(`.error-category:checked`).map(function () {
        return this.value;
    }).get();

    errorParams.errorCategoryIds = errorCategoryIds;
    renderErrorTable();
});

$(document).on('change', `.product-category`, function () {
    let productCategoryIds = $(`.product-category:checked`).map(function () {
        return this.value;
    }).get();

    errorParams.productCategoryIds = productCategoryIds;
    renderErrorTable();
});

$(document).on('change', `.product-category`, function () {
    let productCategoryIds = $(`.product-category:checked`).map(function () {
        return this.value;
    }).get();

    errorParams.productCategoryIds = productCategoryIds;
    renderErrorTable();
});

$(document).on('input', '#searchErrorCode', function () {
    console.log(this.value);
    errorParams.code = this.value;
    renderErrorTable();
});

$(document).on('input', '#searchErrorName', function () {
    errorParams.name = this.value;
    renderErrorTable();
});