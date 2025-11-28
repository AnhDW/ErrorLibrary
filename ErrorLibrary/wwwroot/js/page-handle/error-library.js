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
function setErrorCode(elementId) {
    const currentCode = $('#' + elementId).val();
    if (currentCode) {
        const errorGroupId = $('#editErrorGroupSelect').val();
        if (!errorGroupId || errorGroupId === '') {
            toastr.warning('Vui lòng chọn nhóm lỗi trước khi tạo mã lỗi');
        }
        generateErrorCodeWhenUpdate(errorGroupId, currentCode).then(function (res) {
            $('#' + elementId).val(res.result);
        });
    } else {
        const errorGroupId = $('#addErrorGroupSelect').val();
        if (!errorGroupId || errorGroupId === '') {
            toastr.warning('Vui lòng chọn nhóm lỗi trước khi tạo mã lỗi');
        }
        generateErrorCode(errorGroupId).then(function (res) {
            $('#' + elementId).val(res.result);
        });
    }
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

function renderErrorTable() {
    getErrors().then(function (data) {
        let html = '';
        data.forEach(item => {
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
    });
}
