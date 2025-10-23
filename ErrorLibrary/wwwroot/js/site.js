async function addHandle() {
    const data = await getErrorGroups();
    let html = '<option value="" selected disabled>Chọn nhóm lỗi</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    $('#addErrorGroupSelect').html(html);
}

async function editHandle() {
    const data = await getErrorGroups();
    let html = '<option value="" selected disabled>Chọn nhóm lỗi</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    $('#exampleFormControlSelect2').html(html);
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
    addError(errorData).then(
        $('#addModel').modal('hide');
});
renderErrorTable();
    }

function renderErrorTable() {
    getErrors().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.errorGroup.name}</td>
                        <td>${item.code}</td>
                        <td>${item.name}</td>
                        <td>${item.errorCategory}</td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick="editHandle(${item.id})">
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <a class="dropdown-item" href="javascript:void(0);" onclick="deleteError(${item.id})">
                                        <i class="bx bx-trash me-1"></i> Xóa
                                    </a>
                                </div>
                            </div>
                        </td>
                    </tr>
                `;
        });
        $('#errorTableBody').html(html);
    });
}

function getErrorGroups() {
    return $.get('/ErrorLibrary/GetErrorGroups');
}

function getErrors() {
    return $.get('/ErrorLibrary/GetErrors');
}

function addError(errorDto) {
    console.log('đã vào');
    return $.ajax({
        url: '/ErrorLibrary/AddError',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(errorDto)
    });
}

function updateError(errorDto) {
    return $.ajax({
        url: '/ErrorLibrary/UpdateError',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(errorDto)
    });
}

function deleteError(id) {
    console.log(id)
    return $.ajax({
        url: '/ErrorLibrary/DeleteError',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(id)
    });
}