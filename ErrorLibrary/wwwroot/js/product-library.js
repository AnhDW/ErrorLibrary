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
    console.log(product);
    $('#editProductId').val(product.id);
    $('#editProductCode').val(product.code);
    $('#editProductPO').val(product.po);
    $('#editProductCategory').val(product.productCategoryId);


    $('#editProductCategory').html(html);
}

function handleAddProduct() {
    console.log('handle')
}

function handleEditProduct() {
    console.log('handle')
}

function handleDeleteProduct(id) {
    deleteProduct(id).then(function (res) {
        renderProductTable();
    }).catch(function (err) {
        console.Product(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderProductTable() {
    getProducts().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.Code}</td>
                        <td>${item.PO}</td>
                        <td>${item.ProductCategory.Name}</td>
                        <td><img src="~/assets/img/avatars/1.png" alt="Product" style="width: 60px;" /></td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel">
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <a class="dropdown-item" href="javascript:void(0);" onclick=""><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#productTableBody').html(html);
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

function addProduct(ProductDto) {
    return $.ajax({
        url: '/ProductLibrary/AddProduct',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(ProductDto)
    });
}

function updateProduct(ProductDto) {
    return $.ajax({
        url: '/ProductLibrary/UpdateProduct',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(ProductDto)
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