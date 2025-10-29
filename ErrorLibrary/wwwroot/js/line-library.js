async function addShowLineModalHandle() {
    const data = await getEnterprises();
    console.log(data);
    let html = '<option value="" selected disabled>Chọn xưởng</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    $('#addEnterpriseSelect').html(html);
}

async function editShowLineModalHandle(lineId) {
    const data = await getEnterprises();
    let html = '<option value="" selected disabled>Chọn xưởng</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    var line = await getLineById(lineId);
    $('#editEnterpriseSelect').html(html);
    $('#editId').val(line.id);
    $('#editName').val(line.name);
    $('#editDescription').val(line.description);
}

function handleAddLine() {
    const enterpriseId = $('#addEnterpriseSelect').val();
    const name = $('#addName').val();
    const description = $('#addDescription').val();

    const lineData = {
        enterpriseId,
        name,
        description
    };
    addLine(lineData).then(function (res) {
        $('#addModel').modal('hide');
        renderLinesTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditLine() {
    const id = $('#editId').val();
    const enterpriseId = $('#editEnterpriseSelect').val();
    const name = $('#editName').val();
    const description = $('#editDescription').val();

    // const unit = $('#editUnit').val();

    const lineData= {
        id,
        enterpriseId,
        name,
        description
    };
    updateLine(lineData).then(function (res) {
        $('#editModel').modal('hide');
        renderLinesTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderLinesTable() {
    getLines().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.name}</td>
                        <td>${item.description}</td>
                        <td>${item.enterprise.name}</td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick=(editShowLineModalHandle(${item.id}))>
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <button type="button" class="dropdown-item" onclick="handleDeleteLine(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#lineTableBody').html(html);
    });
}

function handleDeleteLine(id) {
    deleteLine(id).then(function (res) {
        renderLinesTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function getEnterprises() {
    return ajaxRequest({
        url: '/LineLibrary/GetEnterprises',
        method: 'GET',
    })
}

function getLines() {
    return ajaxRequest({
        url: '/LineLibrary/GetLines',
        method: 'GET',
    });
}

function getLineById(id) {
    return ajaxRequest({
        url: '/LineLibrary/GetLineById',
        method: 'GET',
        data: { id: id },
    });
}

function addLine(lineDto) {
    return ajaxRequest({
        url: '/LineLibrary/AddLine',
        method: 'POST',
        data: lineDto,
        showLoading: true
    });
}

function updateLine(lineDto) {
    return ajaxRequest({
        url: '/LineLibrary/UpdateLine',
        method: 'POST',
        data: lineDto,
        showLoading: true
    });
}

function deleteLine(id) {
    // return $.ajax({
    //     url: '/UserLibrary/DeleteUser',
    //     type: 'POST',
    //     contentType: 'application/json',
    //     data: JSON.stringify(id)
    // });
    return ajaxRequest({
        url: '/LineLibrary/DeleteLine',
        method: 'POST',
        data: id,
        showLoading: true
    });
}