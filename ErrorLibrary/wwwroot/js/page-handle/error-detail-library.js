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
    const enterprisesByFactory = await getEnterprisesByFactoryId(value);
    const html = renderSelectOptions(enterprisesByFactory, 'Chọn xưởng');

    if (action === 'add') {
        $('#addEnterpriseSelect').prop('disabled', false);
        $('#addEnterpriseSelect').html(html);
    } else if (action === 'edit') {
        $('#editEnterpriseSelect').prop('disabled', false);
        $('#editEnterpriseSelect').html(html);
    }
}

async function handleSelectEnterprise(value, action) {
    const lineByEnterprise = await getLinesByEnterpriseId(value);
    const html = renderSelectOptions(lineByEnterprise, 'Chọn chuyền');

    if (action === 'add') {
        $('#addLineSelect').prop('disabled', false);
        $('#addLineSelect').html(html);
    } else if (action === 'edit') {
        $('#editLineSelect').prop('disabled', false);
        $('#editLineSelect').html(html);
    }
}

async function addShowErrorDetailModalHandle() {
    const units = await getUnits();
    const products = await getProducts();
    const errors = await getErrors();
    const unitHtml = renderSelectOptionsByField(units, 'Chọn đơn vị', 'id', 'name');
    const productHtml = renderSelectOptionsByField(products, 'Chọn mã hàng', 'id', 'code');
    const errorHtml = renderSelectOptionsByField(errors, 'Chọn mã lỗi', 'id', 'code');
    console.log(errors);
    $('#addUnitSelect').html(unitHtml);
    $('#addProductSelect').html(productHtml);
    $('#addErrorSelect').html(errorHtml);
}

async function editShowErrorDetailModalHandle(errorDetail) {
    console.log(errorDetail);

    const units = await getUnits();
    const factories = await getFactories();
    const enterprises = await getEnterprises();
    const lines = await getLines();
    const products = await getProducts();
    const errors = await getErrors();
    const unitHtml = renderSelectOptionsByField(units, 'Chọn đơn vị', 'id', 'name');
    const factoryHtml = renderSelectOptionsByField(factories, 'Chọn nhà máy', 'id', 'name');
    const enterpriseHtml = renderSelectOptionsByField(enterprises, 'Chọn xưởng', 'id', 'name');
    const lineHtml = renderSelectOptionsByField(lines, 'Chọn chuyền', 'id', 'name');
    const productHtml = renderSelectOptionsByField(products, 'Chọn mã hàng', 'id', 'code');
    const errorHtml = renderSelectOptionsByField(errors, 'Chọn mã lỗi', 'id', 'code');

    $('#editUnitSelect').html(unitHtml);
    $('#editFactorySelect').html(factoryHtml);
    $('#editEnterpriseSelect').html(enterpriseHtml);
    $('#editLineSelect').html(lineHtml);
    $('#editProductSelect').html(productHtml);
    $('#editErrorSelect').html(errorHtml);

    $('#editUnitSelect').val(errorDetail.line.enterprise.factory.unitId);
    $('#editFactorySelect').val(errorDetail.line.enterprise.factoryId);
    $('#editEnterpriseSelect').val(errorDetail.line.enterpriseId);
    $('#editLineSelect').val(errorDetail.lineId);
    $('#editProductSelect').val(errorDetail.productId);
    $('#editErrorSelect').val(errorDetail.errorId);
    $('#editQuantity').val(errorDetail.quantity);
    $('#userId').val(errorDetail.userId);
}

function handleAddErrorDetail() {
    const lineId = $('#addLineSelect').val();
    const productId = $('#addProductSelect').val();
    const errorId = $('#addErrorSelect').val();
    const quantity = $('#addQuantity').val();

    const errorDetailData = {
        lineId,
        productId,
        errorId,
        quantity,
        userId: ''
    };
    console.log(errorDetailData, 'lol');
    addErrorDetail(errorDetailData).then(function (res) {
        $('#addModel').modal('hide');
        renderErrorDetailsTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditErrorDetail() {
    const lineId = $('#editLineSelect').val();
    const productId = $('#editProductSelect').val();
    const errorId = $('#editErrorSelect').val();
    const quantity = $('#editQuantity').val();
    const userId = $('#userId').val();

    const errorDetailData = {
        lineId,
        productId,
        errorId,
        quantity,
        userId
    };
    updateErrorDetail(errorDetailData).then(function (res) {
        $('#editModel').modal('hide');
        renderErrorDetailsTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderErrorDetailsTable() {
    getErrorDetails().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                <tr>
                    <td>${item.line.enterprise.factory.unit.name}</td>
                    <td>${item.line.enterprise.factory.name}</td>
                    <td>${item.line.enterprise.name}</td>
                    <td>${item.line.name}</td>
                    <td>${item.product.code}</td>
                    <td>${item.error.code}</td>
                    <td>${item.quantity}</td>
                    <td>${item.user.fullName}</td>
                    <td>
                        <div class="dropdown">
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                <i class="bx bx-dots-vertical-rounded"></i>
                            </button>
                            <div class="dropdown-menu">
                                <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                        data-bs-target="#editModel" onclick='editShowErrorDetailModalHandle(${JSON.stringify(item).replace(/'/g, "\\'")})'>
                                    <i class="bx bx-edit-alt me-1"></i> Sửa
                                </button>
                                <button type="button" class="dropdown-item" onclick="handleDeleteErrorDetail(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                            </div>
                        </div>
                    </td>
                </tr>
                `;
        });
        $('#errorDetailTableBody').html(html);
        console.log(data);
    });
}

function handleDeleteErrorDetail(id) {
    deleteErrorDetail(id).then(function (res) {
        renderErrorDetailsTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}