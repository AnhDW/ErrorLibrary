function getUnits() {
    return ajaxRequest({
        url: '/UnitLibrary/GetUnits',
        method: 'GET',
    })
}

function getUnitById(id) {
    return ajaxRequest({
        url: '/UnitLibrary/GetUnitById',
        method: 'GET',
        data: { id: id }
    })
}

function addUnit(unitDto) {
    return ajaxRequest({
        url: '/UnitLibrary/AddUnit',
        method: 'POST',
        data: unitDto,
        showLoading: true
    })
}

function updateUnit(unitDto) {
    return ajaxRequest({
        url: '/UnitLibrary/UpdateUnit',
        method: 'POST',
        data: unitDto,
        showLoading: true
    })
}

function deleteUnit(id) {
    return ajaxRequest({
        url: '/UnitLibrary/DeleteUnit',
        method: 'POST',
        data: id,
        showLoading: true
    })
}