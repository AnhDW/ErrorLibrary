function addShowUnitModalHandle() {
    console.log('show add')
}

async function editShowUnitModalHandle(id) {
    console.log('show edit')
    var unit = await getUnitById(id);
    $('#editUnitId').val(unit.id);
    $('#editUnitName').val(unit.name);
    $('#editUnitDescription').val(unit.description);

}

function handleAddUnit() {

    const name = $('#addUnitName').val();
    const description = $('#addUnitDescription').val();
    const unitData = {
        name,
        description
    };
    addUnit(unitData).then(function (res) {
        $('#addModel').modal('hide');
        renderUnitTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditUnit() {

    const id = $('#editUnitId').val();
    const name = $('#editUnitName').val();
    const description = $('#editUnitDescription').val();
    const unitData = {
        id,
        name,
        description
    };
    updateUnit(unitData).then(function (res) {
        $('#editModel').modal('hide');
        renderUnitTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteUnit(id) {
    deleteUnit(id).then(function (res) {
        renderUnitTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi xóa');
    });
}

function renderUnitTable() {
    getUnits().then(function (data) {
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
                                                data-bs-target="#editModel" onclick="editShowUnitModalHandle(${item.id})">
                                            <i class="bx bx-edit-alt me-1"></i> Sửa
                                        </button>
                                        <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteUnit(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    `;
        });
        $('#unitTableBody').html(html);
    });
}

//request
function getUnits() {
    return ajaxRequest({
        url: '/UnitLibrary/GetUnits',
        method: 'GET',
    })
}

function getUnitById(id) {
    return ajaxRequest({
        url: '/UnitLibrary/GetUnitById',
        method: 'GET',
        data: { id: id }
    })
}

function addUnit(unitDto) {
    return ajaxRequest({
        url: '/UnitLibrary/AddUnit',
        method: 'POST',
        data: unitDto,
        showLoading: true
    })
}

function updateUnit(unitDto) {
    return ajaxRequest({
        url: '/UnitLibrary/UpdateUnit',
        method: 'POST',
        data: unitDto,
        showLoading: true
    })
}

function deleteUnit(id) {
    return ajaxRequest({
        url: '/UnitLibrary/DeleteUnit',
        method: 'POST',
        data: id,
        showLoading: true
    })
}