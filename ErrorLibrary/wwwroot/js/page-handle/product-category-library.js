function addShowProductCategoryModalHandle() {
    console.log('show add')
}

async function editShowProductCategoryModalHandle(id) {
    console.log('show edit')
    var productCategory = await getProductCategoryById(id);
    $('#editProductCategoryId').val(productCategory.id);
    $('#editProductCategoryName').val(productCategory.name);
    $('#editProductCategoryDescription').val(productCategory.description);

}

function handleAddProductCategory() {

    const name = $('#addProductCategoryName').val();
    const description = $('#addProductCategoryDescription').val();
    const productCategoryData = {
        name,
        description
    };
    addProductCategory(productCategoryData).then(function (res) {
        $('#addModel').modal('hide');
        renderProductCategoryTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditProductCategory() {

    const id = $('#editProductCategoryId').val();
    const name = $('#editProductCategoryName').val();
    const description = $('#editProductCategoryDescription').val();
    const productCategoryData = {
        id,
        name,
        description
    };
    updateProductCategory(productCategoryData).then(function (res) {
        $('#editModel').modal('hide');
        renderProductCategoryTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteProductCategory(id) {
    deleteProductCategory(id).then(function (res) {
        renderProductCategoryTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi xóa');
    });
}

function renderProductCategoryTable() {
    getProductCategories().then(function (data) {
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
                                        data-bs-target="#editModel" onclick="editShowProductCategoryModalHandle(${item.id})">
                                    <i class="bx bx-edit-alt me-1"></i> Sửa
                                </button>
                                <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteProductCategory(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                            </div>
                        </div>
                    </td>
                </tr>
                `;
        });
        $('#productCategoryTableBody').html(html);
    });
}
