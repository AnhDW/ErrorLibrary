function addShowErrorCategoryModalHandle() {
    console.log('show add')
}

async function editShowErrorCategoryModalHandle(id) {
    console.log('show edit')
    var errorCategory = await getErrorCategoryById(id);
    $('#editErrorCategoryId').val(errorCategory.id);
    $('#editErrorCategoryName').val(errorCategory.name);
    $('#editErrorCategoryDescription').val(errorCategory.description);

}

function handleAddErrorCategory() {

    const name = $('#addErrorCategoryName').val();
    const description = $('#addErrorCategoryDescription').val();
    const errorCategoryData = {
        name,
        description
    };
    addErrorCategory(errorCategoryData).then(function (res) {
        //$('#addModel').modal('hide');
        renderErrorCategoryTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleEditErrorCategory() {

    const id = $('#editErrorCategoryId').val();
    const name = $('#editErrorCategoryName').val();
    const description = $('#editErrorCategoryDescription').val();
    const errorCategoryData = {
        id,
        name,
        description
    };
    updateErrorCategory(errorCategoryData).then(function (res) {
        $('#editModel').modal('hide');
        renderErrorCategoryTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleDeleteErrorCategory(id) {
    deleteErrorCategory(id).then(function (res) {
        renderErrorCategoryTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function renderErrorCategoryTable() {
    getErrorCategories().then(function (data) {
        console.log(data)
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.name}</td>
                        <td>${item.description}</td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick="editShowErrorCategoryModalHandle(${item.id})">
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteErrorCategory(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#errorCategoryTableBody').html(html);
    });
}

//request
