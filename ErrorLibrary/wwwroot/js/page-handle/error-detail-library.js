async function handleSelectUnit(value, action) {
    const factoriesByUnit = await getFactoriesByUnitId(value);
    const html = renderSelectOptions(factoriesByUnit, 'Chọn nhà máy');

    if (action === 'add') {
        $('#addFactorySelect').prop('disabled', false);
        $('#addFactorySelect').html(html);
    } else if (action === 'edit') {
        $('#editFactorySelect').prop('disabled', false);
        $('#editEnterpriseSelect').prop('disabled', true);
        $('#editLineSelect').prop('disabled', true);
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
        $('#editLineSelect').prop('disabled', true);
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

async function handleSelectError(value, action) {
    const error = await getErrorById(value);
    const productsByProductCategory = (await getProductsByProductCategoryById(error.productCategoryId)).result;
    const html = renderSelectOptionsByField(productsByProductCategory, 'Chọn sản phẩm', 'id', 'code');

    if (action === 'add') {
        $('#addProductSelect').prop('disabled', false);
        $('#addProductSelect').html(html);
    } else if (action === 'edit') {
        $('#editProductSelect').prop('disabled', false);
        $('#editProductSelect').html(html);
    }
}

async function addShowErrorDetailModalHandle() {
    const units = await getUnits();
    const products = (await getProducts()).result;
    const errors = (await getErrors()).result;
    const unitHtml = renderSelectOptionsByField(units, 'Chọn đơn vị', 'id', 'name');
    const productHtml = renderSelectOptionsByField(products, 'Chọn mã hàng', 'id', 'code');
    const errorHtml = renderSelectErrorOptions(errors, 'Chọn mã lỗi');
    console.log(units);
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
    const errors = (await getErrors()).result;
    const products = (await getProducts()).result;
    const unitHtml = renderSelectOptionsByField(units, 'Chọn đơn vị', 'id', 'name');
    const factoryHtml = renderSelectOptionsByField(factories, 'Chọn nhà máy', 'id', 'name');
    const enterpriseHtml = renderSelectOptionsByField(enterprises, 'Chọn xưởng', 'id', 'name');
    const lineHtml = renderSelectOptionsByField(lines, 'Chọn chuyền', 'id', 'name');
    const productHtml = renderSelectOptionsByField(products, 'Chọn mã hàng', 'id', 'code');
    const errorHtml = renderSelectErrorOptions(errors, 'Chọn mã lỗi');

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
 function showImagesModal(errorDetail) {
     console.log(errorDetail);
     renderErrorDetailAttachment(errorDetail.lineId, errorDetail.productId, errorDetail.errorId, errorDetail.userId);
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
        //$('#addModel').modal('hide');
        renderErrorDetailsTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
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
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
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
                    <td><button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="modal" data-bs-target="#imagesModal" onclick='showImagesModal(${JSON.stringify(item).replace(/'/g, "\\'")})'>
                                <i class="bx bx-images"></i>
                        </button>
                    </td>
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
                                <button type="button" class="dropdown-item" onclick='handleDeleteErrorDetail(${JSON.stringify(item).replace(/'/g, "\\'")})'><i class="bx bx-trash me-1"></i> Xóa</a>
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

function handleDeleteErrorDetail(errorDetail) {
    const deleteErrorDetailDto = {
        lineId : errorDetail.lineId,
        productId : errorDetail.productId,
        errorId : errorDetail.errorId,
        userId : errorDetail.userId
    };
    console.log(errorDetail);
    console.log(deleteErrorDetailDto);
    deleteErrorDetail(deleteErrorDetailDto).then(function (res) {
        renderErrorDetailsTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}