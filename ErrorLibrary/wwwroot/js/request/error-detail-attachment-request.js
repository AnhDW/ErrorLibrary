function getByErrorDetail(lineId, productId, errorId, userId) {
    return ajaxRequest({
        url: '/ErrorDetailAttachmentLibrary/GetByErrorDetail',
        method: 'GET',
        data: { lineId, productId, errorId, userId }
    })
}

function getErrorDetailAttachmentById(id) {
    return ajaxRequest({
        url: '/ErrorDetailAttachmentLibrary/GetErrorDetailAttachmentById',
        method: 'GET',
        data: { id }
    })
}

function addErrorDetailAttachment(errorDetailAttachmentDto) {
    const formData = new FormData();

    formData.append("lineId", errorDetailAttachmentDto.lineId);
    formData.append("productId", errorDetailAttachmentDto.productId);
    formData.append("errorId", errorDetailAttachmentDto.errorId);
    formData.append("userId", errorDetailAttachmentDto.userId);
    formData.append("fileName", errorDetailAttachmentDto.fileName);
    formData.append("url", errorDetailAttachmentDto.url);
    formData.append("contentType", errorDetailAttachmentDto.contentType);

    formData.append("file", $("#addErrorDetailAttachment")[0].files[0]);

    return ajaxRequest({
        url: '/ErrorDetailAttachmentLibrary/AddErrorDetailAttachment',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true,
        useToken: true,
    })
}

function deleteErrorDetailAttachment(id) {
    return ajaxRequest({
        url: '/ErrorDetailAttachmentLibrary/DeleteErrorDetailAttachment',
        method: 'POST',
        data: {id},
        showLoading: true,
        useToken: true,
    })
}