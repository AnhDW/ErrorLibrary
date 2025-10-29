function getLines() {
    return ajaxRequest({
        url: '/LineLibrary/GetLines',
        method: 'GET',
    });
}

function getLineById(id) {
    return ajaxRequest({
        url: '/LineLibrary/GetLineById',
        method: 'GET',
        data: { id: id },
    });
}

function addLine(lineDto) {
    return ajaxRequest({
        url: '/LineLibrary/AddLine',
        method: 'POST',
        data: lineDto,
        showLoading: true
    });
}

function updateLine(lineDto) {
    return ajaxRequest({
        url: '/LineLibrary/UpdateLine',
        method: 'POST',
        data: lineDto,
        showLoading: true
    });
}

function deleteLine(id) {
    return ajaxRequest({
        url: '/LineLibrary/DeleteLine',
        method: 'POST',
        data: id,
        showLoading: true
    });
}