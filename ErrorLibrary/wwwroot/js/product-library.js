async function addShowProductModalHandle() {
    const data = await getProductCategories();
    let html = '<option selected="">Chọn chủng loại</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    $('#addProductCategory').html(html);
}

async function editShowProductModalHandle(productId) {
    const data = await getProductCategories();
    let html = '<option selected="">Chọn chủng loại</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    })
    var product = await getProductById(productId);
    $('#editProductCategory').html(html);

    $('#editProductId').val(product.id);
    $('#editProductCode').val(product.code);
    $('#editProductPO').val(product.po);
    $('#editProductCategory').val(product.productCategoryId);

}

function handleAddProduct() {
    const code = $('#addProductCode').val();
    const po = $('#addProductPO').val();
    const productCategoryId = $('#addProductCategory').val();

    const productData = {
        code,
        po,
        productCategoryId
    };
    addProduct(productData).then(function (res) {
        $('#addModel').modal('hide');
        renderProductTable();
    }).catch(function (err) {
        alert('Có lỗi xảy ra khi thêm');
    });
}

function handleEditProduct() {
    const id = $('#editProductId').val();
    const code = $('#editProductCode').val();
    const po = $('#editProductPO').val();
    const productCategoryId = $('#editProductCategory').val();
    const productData = {
        id,
        code,
        po,
        productCategoryId
    };
    updateProduct(productData).then(function (res) {
        $('#editModel').modal('hide');
        renderProductTable();
    }).catch(function (err) {
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteProduct(id) {
    deleteProduct(id).then(function (res) {
        renderProductTable();
    }).catch(function (err) {
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderProductTable() {
    getProducts().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.code}</td>
                        <td>${item.po}</td>
                        <td>${item.productCategory.name}</td>
                        <td><img src="${item.imageUrl}" alt="Product" style="width: 60px;" /></td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                                data-bs-target="#editModel" onclick="editShowProductModalHandle(${item.id})">
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                        <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteProduct(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#productTableBody').html(html);
        console.log(html, data);
    });
}

function getProductCategories() {
    return $.get('/ProductLibrary/GetProductCategories');
}

function getProducts() {
    return $.get('/ProductLibrary/GetProducts');
}

function getProductById(id) {
    return $.get('/ProductLibrary/GetProductById', { id: id });
}

function addProduct(productDto) {
    const formData = new FormData();

    formData.append("productCategoryId", productDto.productCategoryId);
    formData.append("code", productDto.code);
    formData.append("po", productDto.po);
    formData.append("file", $("#addProductImage")[0].files[0]);

    return $.ajax({
        url: '/ProductLibrary/AddProduct',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false
    });
}

function updateProduct(productDto) {
    const formData = new FormData();

    formData.append("id", productDto.id);
    formData.append("productCategoryId", productDto.productCategoryId);
    formData.append("code", productDto.code);
    formData.append("po", productDto.po);
    formData.append("file", $("#editProductImage")[0].files[0]);

    return $.ajax({
        url: '/ProductLibrary/UpdateProduct',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false
    });
}

function deleteProduct(id) {
    console.log(id)
    return $.ajax({
        url: '/ProductLibrary/DeleteProduct',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(id)
    });
}