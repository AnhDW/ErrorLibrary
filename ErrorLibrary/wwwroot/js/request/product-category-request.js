function getProductCategories() {
    return ajaxRequest({
        url: '/ProductCategoryLibrary/GetProductCategories',
        method: 'GET',
    })
}

function getProductCategoryById(id) {
    return ajaxRequest({
        url: '/ProductCategoryLibrary/GetProductCategoryById',
        method: 'GET',
        data: { id: id }
    })
}

function addProductCategory(productCategoryDto) {
    return ajaxRequest({
        url: '/ProductCategoryLibrary/AddProductCategory',
        method: 'POST',
        data: productCategoryDto,
        showLoading: true
    })
}

function updateProductCategory(productCategoryDto) {
    return ajaxRequest({
        url: '/ProductCategoryLibrary/UpdateProductCategory',
        method: 'POST',
        data: productCategoryDto,
        showLoading: true
    })
}

function deleteProductCategory(id) {
    return ajaxRequest({
        url: '/ProductCategoryLibrary/DeleteProductCategory',
        method: 'POST',
        data: id,
        showLoading: true
    })
}