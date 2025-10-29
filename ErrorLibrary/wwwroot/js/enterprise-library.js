async function handleSelectUnit(value, action) {
    const factoriesByUnit = await getFactoriesByUnitId(value);
    const html = renderSelectOptions(factoriesByUnit, 'Chọn nhà máy');

    if (action === 'add') {
        $('#addFactorySelect').prop('disabled', false);
        $('#addFactorySelect').html(html);
    }
    if (action === 'edit') {
        $('#editFactorySelect').prop('disabled', false);
        $('#editFactorySelect').html(html);
    }
}

async function addShowEnterpriseModalHandle() {
    const units = await getUnits();
    const unitHtml = renderSelectOptions(units, 'Chọn đơn vị');
    $('#addUnitSelect').html(unitHtml);
}

async function editShowEnterpriseModalHandle(enterprise) {
    console.log(enterprise);
    const units = await getUnits();
    const factories = await getFactories();
    const unitHtml = renderSelectOptions(units, 'Chọn đơn vị');
    const factoryHtml = renderSelectOptions(factories, 'Chọn nhà máy');

    $('#editUnitSelect').html(unitHtml);
    $('#editFactorySelect').html(factoryHtml);
    $('#editUnitSelect').val(enterprise.factory.unitId);
    $('#editFactorySelect').val(enterprise.factoryId);
    $('#editId').val(enterprise.id);
    $('#editName').val(enterprise.name);
    $('#editDescription').val(enterprise.description);
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

    console.log(enterpriseData)

    addEnterprise(enterpriseData).then(function (res) {
        if (res.isSuccess) {
            $('#addModel').modal('hide');
            renderEnterprisesTable();
        } else {
            alert(res.message);
        }
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
        if (res.isSuccess) {
            $('#editModel').modal('hide');
            renderEnterprisesTable();
        } else {
            alert(res.message);
        }
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
                    <td>${item.factory.unit.name}</td>
                    <td>${item.factory.name}</td>
                    <td>${item.name}</td>
                    <td>${item.description}</td>
                    <td>
                        <div class="dropdown">
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                <i class="bx bx-dots-vertical-rounded"></i>
                            </button>
                            <div class="dropdown-menu">
                                <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                        data-bs-target="#editModel"
                                        onclick='editShowEnterpriseModalHandle(${JSON.stringify(item).replace(/'/g, "\\'")})'>
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
        console.log(data);
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
