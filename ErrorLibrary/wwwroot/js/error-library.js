//handle
async function addShowErrorModalHandle() {
    console.log('errData');

    const data = await getErrorGroups();
    console.log(data);
    let html = '<option value="" selected disabled>Chọn nhóm lỗi</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    $('#addErrorGroupSelect').html(html);
}

async function editShowErrorModalHandle(errId) {
    const data = await getErrorGroups();
    let html = '<option value="" selected disabled>Chọn nhóm lỗi</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    var err = await getErrorById(errId);
    $('#editErrorGroupSelect').html(html);
    $('#editErrorId').val(err.id);
    $('#editErrorCode').val(err.code);
    $('#editErrorName').val(err.name);
    $('#editErrorType').val(err.errorCategory);
    $('#editErrorGroupSelect').val(err.errorGroupId);

}

function handleAddError() {
    const errorGroupId = $('#addErrorGroupSelect').val();
    const code = $('#addErrorCode').val();
    const name = $('#addErrorName').val();
    const type = $('#addErrorType').val();

    const errorData = {
        errorGroupId,
        code,
        name,
        type
    };
    addError(errorData).then(function (res) {
        $('#addModel').modal('hide');
        renderErrorTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditError() {
    const id = $('#editErrorId').val();
    const errorGroupId = $('#editErrorGroupSelect').val();
    const code = $('#editErrorCode').val();
    const name = $('#editErrorName').val();
    const type = $('#editErrorType').val();

    const errorData = {
        id,
        errorGroupId,
        code,
        name,
        type
    };
    console.log(errorData);
    updateError(errorData).then(function (res) {
        $('#editModel').modal('hide');
        renderErrorTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteError(id) {
    deleteError(id).then(function (res) {
        renderErrorTable();
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
                    <tr>
                        <td>${item.errorGroup == null ? '' : item.errorGroup.name}</td>
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

// request
function getErrorGroups() {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrorGroups',
        method: 'GET',
    })
}

function getErrors() {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrors',
        method: 'GET',
    })
}

function getErrorById(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrorById',
        method: 'GET',
        data: { id: id }
    })
}

function addError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/AddError',
        method: 'POST',
        data: errorDto,
        showLoading: true
    })
}

function updateError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/UpdateError',
        method: 'POST',
        data: errorDto,
        showLoading: true
    })
}

function deleteError(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/DeleteError',
        method: 'POST',
        data: id,
        showLoading: true
    })
}