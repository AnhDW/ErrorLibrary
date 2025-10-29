function getEnterprises() {
    return ajaxRequest({
        url: '/EnterpriseLibrary/GetEnterprises',
        method: 'GET',
    });
}

function getEnterprisesByFactoryId(factoryId) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/GetEnterprisesByFactoryId',
        method: 'GET',
        data: { factoryId: factoryId },
    });
}

function getEnterpriseById(id) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/GetEnterpriseById',
        method: 'GET',
        data: { id: id },
    });
}

function addEnterprise(enterpriseDto) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/AddEnterprise',
        method: 'POST',
        data: enterpriseDto,
        showLoading: true
    });
}

function updateEnterprise(enterpriseDto) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/UpdateEnterprise',
        method: 'POST',
        data: enterpriseDto,
        showLoading: true
    });
}

function deleteEnterprise(id) {
    return ajaxRequest({
        url: '/EnterpriseLibrary/DeleteEnterprise',
        method: 'POST',
        data: id,
        showLoading: true
    });
}