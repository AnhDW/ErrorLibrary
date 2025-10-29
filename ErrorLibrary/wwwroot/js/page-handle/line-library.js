async function handleSelectUnit(value, action) {
    const factoriesByUnit = await getFactoriesByUnitId(value);
    const html = renderSelectOptions(factoriesByUnit, 'Chọn nhà máy');

    if (action === 'add') {
        $('#addFactorySelect').prop('disabled', false);
        $('#addFactorySelect').html(html);
    } else if (action === 'edit') {
        $('#editFactorySelect').prop('disabled', false);
        $('#editFactorySelect').html(html);
    }
}

async function handleSelectFactory(value, action) {
    const enterprisesByUnit = await getEnterprisesByFactoryId(value);
    const html = renderSelectOptions(enterprisesByUnit, 'Chọn xưởng');

    if (action === 'add') {
        $('#addEnterpriseSelect').prop('disabled', false);
        $('#addEnterpriseSelect').html(html);
    } else if (action === 'edit') {
        $('#editEnterpriseSelect').prop('disabled', false);
        $('#editEnterpriseSelect').html(html);
    }
}

async function addShowLineModalHandle() {
    const units = await getUnits();
    const unitHtml = renderSelectOptions(units, 'Chọn đơn vị');
    $('#addUnitSelect').html(unitHtml);
}

async function editShowLineModalHandle(line) {
    console.log(line)
    const units = await getUnits();
    const factories = await getFactories();
    const enterprises = await getEnterprises();

    const unitHtml = renderSelectOptions(units, 'Chọn đơn vị');
    const factoryHtml = renderSelectOptions(factories, 'Chọn nhà máy');
    const enterpriseHtml = renderSelectOptions(enterprises, 'Chọn xưởng');

    $('#editUniteSelect').html(unitHtml);
    $('#editFactorySelect').html(factoryHtml);
    $('#editEnterpriseSelect').html(enterpriseHtml);
    $('#editUnitSelect').val(line.enterprise.factory.unitId);
    $('#editFactorySelect').val(line.enterprise.factoryId);
    $('#editEnterpriseSelect').val(line.enterpriseId);
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
                    <td>${item.enterprise.factory.unit.name}</td>
                    <td>${item.enterprise.factory.name}</td>
                    <td>${item.enterprise.name}</td>
                    <td>${item.name}</td>
                    <td>${item.description}</td>
                    <td>
                        <div class="dropdown">
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                <i class="bx bx-dots-vertical-rounded"></i>
                            </button>
                            <div class="dropdown-menu">
                                <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                        data-bs-target="#editModel" onclick='editShowLineModalHandle(${JSON.stringify(item).replace(/'/g, "\\'")})'>
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