async function addShowFactoryModalHandle() {
    const data = await getUnits();
    let html = '<option value="" selected disabled>Chọn đơn vị</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    $('#addUnitSelect').html(html);
}

async function editShowFactoryModalHandle(id) {
    const data = await getUnits();
    let html = '<option value="" selected disabled>Chọn đơn vị</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    var factory = await getFactoryById(id);
    $('#editUnitSelect').html(html);
    $('#editUnitSelect').val(factory.unitId);
    $('#editFactoryId').val(factory.id);
    $('#editFactoryName').val(factory.name);
    $('#editFactoryDescription').val(factory.description);
}

function handleAddFactory() {
    const unitId = $('#addUnitSelect').val();
    const name = $('#addFactoryName').val();
    const description = $('#addFactoryDescription').val();
    const factoryData = {
        unitId,
        name,
        description
    };
    addFactory(factoryData).then(function (res) {
        if (res.isSuccess) {
            $('#addModel').modal('hide');
            renderFactoryTable();
        } else {
            alert(res.message);
        }
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi thêm');
    });
}

function handleEditFactory() {
    const unitId = $('#editUnitSelect').val();
    const id = $('#editFactoryId').val();
    const name = $('#editFactoryName').val();
    const description = $('#editFactoryDescription').val();
    const factoryData = {
        unitId,
        id,
        name,
        description
    };
    updateFactory(factoryData).then(function (res) {
        if (res.isSuccess) {
            $('#editModel').modal('hide');
            renderFactoryTable();
        } else {
            alert(res.message);
        }
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteFactory(id) {
    deleteFactory(id).then(function (res) {
        renderFactoryTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi xóa');
    });
}

function renderFactoryTable() {
    getFactories().then(function (data) {
        console.log(data)
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.unit.name}</td>
                        <td>${item.name}</td>
                        <td>${item.description}</td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick="editShowFactoryModalHandle(${item.id})">
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteFactory(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#factoryTableBody').html(html);
    });
}

function getFactories() {
    return ajaxRequest({
        url: '/FactoryLibrary/GetFactories',
        method: 'GET',
    })
}

function getFactoriesByUnitId(unitId) {
    return ajaxRequest({
        url: '/FactoryLibrary/GetFactoriesByUnitId',
        method: 'GET',
        data: { unitId: unitId }
    })
}

function getFactoryById(id) {
    return ajaxRequest({
        url: '/FactoryLibrary/GetFactoryById',
        method: 'GET',
        data: { id: id }
    })
}

function addFactory(factoryDto) {
    return ajaxRequest({
        url: '/FactoryLibrary/AddFactory',
        method: 'POST',
        data: factoryDto,
        showLoading: true
    })
}

function updateFactory(factoryDto) {
    return ajaxRequest({
        url: '/FactoryLibrary/UpdateFactory',
        method: 'POST',
        data: factoryDto,
        showLoading: true
    })
}

function deleteFactory(id) {
    return ajaxRequest({
        url: '/FactoryLibrary/DeleteFactory',
        method: 'POST',
        data: id,
        showLoading: true
    })
}