var errorCategoryParams = {
    name: '',
    description: '',
    pageNumber: 1, pageSize: 20
}

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

async function renderErrorCategoryHeadFilter() {
    $('#errorCategoryNamesHeader').html(renderFilterByField([], 'tên loại lỗi', 'searchErrorCategoryName', '', '', '', true));
    $('#errorCategoryDescriptionsHeader').html(renderFilterByField([], 'mô tả', 'searchErrorCategoryDescription', '', '', '', true));
}

function renderErrorCategoryTable() {
    getErrorCategorysPagination(errorCategoryParams).then(function (res) {
        console.log(res)
        let html = '';
        res.result.forEach(item => {
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
        renderPagination(res.paginationHeader, 'errorCategoryChangePage', 'errorCategoryPagination');
    });
}

//request

//filter handle
$(document).on('input', '#searchErrorCategoryName', function () {
    console.log(this.value);
    errorCategoryParams.name = this.value;
    renderErrorCategoryTable();
});

$(document).on('input', '#searchErrorCategoryDescription', function () {
    errorCategoryParams.description = this.value;
    renderErrorCategoryTable();
});
