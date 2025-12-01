function errorExcelPreview(previewErrorExcelDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/ErrorExcelPreview',
        method: 'POST',
        data: previewErrorExcelDto
    })
}

function getErrorsPagination(errorParams) {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrorsPagination',
        method: 'GET',
        data: $.param(errorParams, true)
    });
}

function getErrors() {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrors',
        method: 'GET',
    })
}

function getErrorById(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrorById',
        method: 'GET',
        data: { id: id }
    })
}

function generateErrorCode(errorGroupId) {
    return ajaxRequest({
        url: '/ErrorLibrary/GenerateErrorCode',
        method: 'GET',
        data: { errorGroupId: errorGroupId }
    })
}

function generateErrorCodeWhenUpdate(errorGroupId, currentCode) {
    return ajaxRequest({
        url: '/ErrorLibrary/GenerateErrorCodeWhenUpdate',
        method: 'GET',
        data: {
            errorGroupId: errorGroupId,
            currentCode: currentCode
        }
    })
}

function addError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/AddError',
        method: 'POST',
        data: errorDto,
        showLoading: true,
        useToken: true
    })
}

function updateError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/UpdateError',
        method: 'POST',
        data: errorDto,
        showLoading: true,
        useToken:true
    })
}

function deleteError(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/DeleteError',
        method: 'POST',
        data: id,
        showLoading: true,
        useToken: true

    })
}

function importErrorsToExcel(importErrorDto) {
    const formData = new FormData();
    formData.append("file", $("#importErrors")[0].files[0]);
    formData.append("worksheetIndex", importErrorDto.worksheetIndex);

    return ajaxRequest({
        url: '/ErrorLibrary/ImportErrorsToExcel',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true,
    })
}

function addErrorsToErrorExcelDto(errorExcelDtos) {
    return ajaxRequest({
        url: '/ErrorLibrary/AddErrorsToErrorExcelDto',
        method: 'POST',
        data: errorExcelDtos,
        showLoading: true,
        useToken: true
    })
}

function deleteAll(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/DeleteAll',
        method: 'POST',
        data: id,
        showLoading: true,
        useToken: true

    })
}
