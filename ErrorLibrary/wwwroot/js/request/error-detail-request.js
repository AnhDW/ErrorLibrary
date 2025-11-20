function getErrorDetails() {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/GetErrorDetails',
        method: 'GET',
    })
}

function getErrorDetailById(lineId, productId, errorId, userId) {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/GetErrorDetailById',
        method: 'GET',
        data: { lineId, productId, errorId, userId }
    })
}

function addErrorDetail(errorDetailDto) {
    const formData = new FormData();

    formData.append("lineId", errorDetailDto.lineId);
    formData.append("productId", errorDetailDto.productId);
    formData.append("errorId", errorDetailDto.errorId);
    formData.append("userId", errorDetailDto.userId);
    formData.append("quantity", errorDetailDto.quantity);
    const files = $("#addErrorDetailAttachment")[0].files;
    for (let i = 0; i < files.length; i++) {
        formData.append("files", files[i]);  // <-- append từng file
    }
    return ajaxRequest({
        url: '/ErrorDetailLibrary/AddErrorDetail',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true,
        useToken: true,
    })
}

function updateErrorDetail(errorDetailDto) {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/UpdateErrorDetail',
        method: 'POST',
        data: errorDetailDto,
        showLoading: true,
        useToken: true,
    })
}

function deleteErrorDetail(deleteErrorDetailDto) {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/DeleteErrorDetail',
        method: 'POST',
        data: deleteErrorDetailDto,
        showLoading: true,
        useToken: true,
    })
}