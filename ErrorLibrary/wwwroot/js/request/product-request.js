function getProducts() {
    return ajaxRequest({
        url: '/ProductLibrary/GetProducts',
        method: 'GET',
    })
}

function getProductsByProductCategoryById(productCategoryId) {
    return ajaxRequest({
        url: '/ProductLibrary/GetProductsByProductCategoryById',
        method: 'GET',
        data: { productCategoryId: productCategoryId }
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
    formData.append("quantity", productDto.quantity);
    formData.append("frontFile", $("#addProductFrontImage")[0].files[0]);
    formData.append("backFile", $("#addProductBackImage")[0].files[0]);

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
    formData.append("quantity", productDto.quantity);
    formData.append("frontImageUrl", productDto.frontImageUrl);
    formData.append("backImageUrl", productDto.backImageUrl);
    formData.append("frontFile", $("#editProductFrontImage")[0].files[0]);
    formData.append("backFile", $("#editProductBackImage")[0].files[0]);
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