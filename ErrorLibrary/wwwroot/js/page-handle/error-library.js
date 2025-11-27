//handle
async function addShowErrorModalHandle() {
    const errorGroups = await getErrorGroups();
    const errorGroupsHtml = renderSelectOptions(errorGroups, 'Chọn nhóm lỗi');
    const productCategories = await getProductCategories();
    const productCategoriesHtml = renderSelectOptions(productCategories, 'Chọn chủng loại sản phẩm');

    $('#addErrorGroupSelect').html(errorGroupsHtml);
    $('#addProductCategorySelect').html(productCategoriesHtml);
}

async function editShowErrorModalHandle(errId) {
    const errorGroups = await getErrorGroups();
    const errorGroupsHtml = renderSelectOptions(errorGroups, 'Chọn nhóm lỗi');
    const productCategories = await getProductCategories();
    const productCategoriesHtml = renderSelectOptions(productCategories, 'Chọn chủng loại sản phẩm');

    var err = await getErrorById(errId);
    console.log(err);
    $('#editErrorGroupSelect').html(errorGroupsHtml);
    $('#editProductCategorySelect').html(productCategoriesHtml);

    $('#editErrorId').val(err.id);
    $('#editErrorCode').val(err.code);
    $('#editErrorName').val(err.name);
    $('#editErrorType').val(err.errorCategory);
    $('#editErrorGroupSelect').val(err.errorGroupId);
    $('#editProductCategorySelect').val(err.productCategoryId);

}

function handleAddError() {
    const errorGroupId = $('#addErrorGroupSelect').val();
    const productCategoryId = $('#addProductCategorySelect').val();
    const code = $('#addErrorCode').val();
    const name = $('#addErrorName').val();
    const errorCategory = $('#addErrorType').val();

    const errorData = {
        errorGroupId,
        productCategoryId,
        code,
        name,
        errorCategory
    };
    addError(errorData).then(function (res) {
        //$('#addModel').modal('hide');
        //renderErrorTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditError() {
    const id = $('#editErrorId').val();
    const errorGroupId = $('#editErrorGroupSelect').val();
    const productCategoryId = $('#editProductCategorySelect').val();
    const code = $('#editErrorCode').val();
    const name = $('#editErrorName').val();
    const errorCategory = $('#editErrorType').val();

    const errorData = {
        id,
        errorGroupId,
        productCategoryId,
        code,
        name,
        errorCategory
    };
    console.log(errorData);
    updateError(errorData).then(function (res) {
        $('#editModel').modal('hide');
        //renderErrorTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteError(id) {
    deleteError(id).then(function (res) {
        //renderErrorTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderErrorTable() {
    getErrors().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                    <tr id="row_${item.id}">
                        <td>${item.errorGroup == null ? '' : item.errorGroup.name}</td>
                        <td>${item.productCategory == null ? '' : item.productCategory.name}</td>
                        <td>${item.code}</td>
                        <td>${item.name}</td>
                        <td>${item.errorCategory ?? ''}</td>
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
