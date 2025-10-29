function getFactories() {
    return ajaxRequest({
        url: '/FactoryLibrary/GetFactories',
        method: 'GET',
    })
}

function getFactoriesByUnitId(unitId) {
    return ajaxRequest({
        url: '/FactoryLibrary/GetFactoriesByUnitId',
        method: 'GET',
        data: { unitId: unitId }
    })
}

function getFactoryById(id) {
    return ajaxRequest({
        url: '/FactoryLibrary/GetFactoryById',
        method: 'GET',
        data: { id: id }
    })
}

function addFactory(factoryDto) {
    return ajaxRequest({
        url: '/FactoryLibrary/AddFactory',
        method: 'POST',
        data: factoryDto,
        showLoading: true
    })
}

function updateFactory(factoryDto) {
    return ajaxRequest({
        url: '/FactoryLibrary/UpdateFactory',
        method: 'POST',
        data: factoryDto,
        showLoading: true
    })
}

function deleteFactory(id) {
    return ajaxRequest({
        url: '/FactoryLibrary/DeleteFactory',
        method: 'POST',
        data: id,
        showLoading: true
    })
}