function getProductCategoriesPagination(productCategoryParams) {
    return ajaxRequest({
        url: '/ProductCategoryLibrary/GetProductCategoriesPagination',
        method: 'GET',
        data: $.param(productCategoryParams, true)
    })
}

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

function addProductCategoryByNames(names) {
    return ajaxRequest({
        url: '/ProductCategoryLibrary/AddProductCategoryByNames',
        method: 'POST',
        data: names,
        showLoading: true
    })
}