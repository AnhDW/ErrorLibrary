async function addShowProductModalHandle() {
    const data = await getProductCategories();
    const html = renderSelectOptions(data, 'Chọn chủng loại');

    $('#addProductCategory').html(html);
}

async function editShowProductModalHandle(productId) {
    const data = await getProductCategories();
    const html = renderSelectOptions(data, 'Chọn chủng loại');

    var product = await getProductById(productId);
    console.log(product);
    $('#editProductCategory').html(html);

    $('#editProductId').val(product.id);
    $('#editProductCode').val(product.code);
    $('#editProductPO').val(product.po);
    $('#editProductCategory').val(product.productCategoryId);
    $('#editProductImageUrl').val(product.imageUrl);
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
        $('#addProductImage').val('');
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
    const imageUrl = $('#editProductImageUrl').val();
    const productData = {
        id,
        code,
        po,
        productCategoryId,
        imageUrl
    };
    console.log(productData);
    updateProduct(productData).then(function (res) {
        $('#editProductImage').val('');
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
    });
}
