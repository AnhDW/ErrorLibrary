async function addShowEnterpriseModalHandle() {
    console.log('enterpriseData');

    const data = await getFactories();
    console.log(data);
    let html = '<option value="" selected disabled>Chọn nhà máy</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    $('#addFactorySelect').html(html);
}

async function editShowEnterpriseModalHandle(entId) {
    const data = await getFactories();
    let html = '<option value="" selected disabled>Chọn nhà máy</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    var ent = await getEnterpriseById(entId);
    $('#editFactorySelect').html(html);
    $('#editId').val(ent.id);
    $('#editName').val(ent.name);
    $('#editDescription').val(ent.description);
}

function handleAddEnterprise() {
    const factoryId = $('#addFactorySelect').val();
    const name = $('#addName').val();
    const description = $('#addDescription').val();

    const enterpriseData = {
        factoryId,
        name,
        description
    };

    addEnterprise(enterpriseData).then(function (res) {
        $('#addModel').modal('hide');
        renderEnterprisesTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditEnterprise() {
    const id = $('#editId').val();
    const factoryId = $('#editFactorySelect').val();
    const name = $('#editName').val();
    const description = $('#editDescription').val();

    // const unit = $('#editUnit').val();

    const enterpriseData = {
        id,
        factoryId,
        name,
        description
    };
    updateEnterprise(enterpriseData).then(function (res) {
        $('#editModel').modal('hide');
        renderEnterprisesTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderEnterprisesTable() {
    getEnterprises().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.name}</td>
                        <td>${item.description}</td>
                        <td>${item.factory.name}</td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick=(editShowEnterpriseModalHandle(${item.id}))>
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <button type="button" class="dropdown-item" onclick="handleDeleteEnterprise(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#enterpriseTableBody').html(html);
    });
}

function handleDeleteEnterprise(id) {
    deleteEnterprise(id).then(function (res) {
        renderEnterprisesTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function getFactories() {
    return ajaxRequest({
        url: '/EnterpriseLibrary/GetFactories',
        method: 'GET',
    })
}

function getEnterprises() {
    return ajaxRequest({
        url: '/EnterpriseLibrary/GetEnterprises',
        method: 'GET',
    });
}

function getEnterpriseById(id) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/GetEnterpriseById',
        method: 'GET',
        data: { id: id },
    });
}

function addEnterprise(enterpriseDto) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/AddEnterprise',
        method: 'POST',
        data: enterpriseDto,
        showLoading: true
    });
}

function updateEnterprise(enterpriseDto) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/UpdateEnterprise',
        method: 'POST',
        data: enterpriseDto,
        showLoading: true
    });
}

function deleteEnterprise(id) {
    // return $.ajax({
    //     url: '/UserLibrary/DeleteUser',
    //     type: 'POST',
    //     contentType: 'application/json',
    //     data: JSON.stringify(id)
    // });
    return ajaxRequest({
        url: '/EnterpriseLibrary/DeleteEnterprise',
        method: 'POST',
        data: id,
        showLoading: true
    });
}