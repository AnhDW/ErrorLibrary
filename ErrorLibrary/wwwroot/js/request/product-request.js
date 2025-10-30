function getProducts() {
    return ajaxRequest({
        url: '/ProductLibrary/GetProducts',
        method: 'GET',
    })
}

function getProductById(id) {
    return ajaxRequest({
        url: '/ProductLibrary/GetProductById',
        method: 'GET',
        data: { id: id }
    })
}

function addProduct(productDto) {
    const formData = new FormData();

    formData.append("productCategoryId", productDto.productCategoryId);
    formData.append("code", productDto.code);
    formData.append("po", productDto.po);
    formData.append("file", $("#addProductImage")[0].files[0]);

    return ajaxRequest({
        url: '/ProductLibrary/AddProduct',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true
    })
}

function updateProduct(productDto) {
    const formData = new FormData();

    formData.append("id", productDto.id);
    formData.append("productCategoryId", productDto.productCategoryId);
    formData.append("code", productDto.code);
    formData.append("po", productDto.po);
    formData.append("imageUrl", productDto.imageUrl);
    formData.append("file", $("#editProductImage")[0].files[0]);

    return ajaxRequest({
        url: '/ProductLibrary/UpdateProduct',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true
    })
}

function deleteProduct(id) {
    return ajaxRequest({
        url: '/ProductLibrary/DeleteProduct',
        method: 'POST',
        data: id,
        showLoading: true
    })
}