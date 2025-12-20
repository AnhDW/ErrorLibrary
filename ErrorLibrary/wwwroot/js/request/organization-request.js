function getOrganizationTree() {
    return ajaxRequest({
        url: '/OrganizationLibrary/GetOrganizationTree',
        method: 'GET',
    })
}

function getOrganizationTreeDropdown() {
    return ajaxRequest({
        url: '/OrganizationLibrary/GetOrganizationTreeDropdown',
        method: 'GET',
    })
}

function getOrganizationsDisplay() {
    return ajaxRequest({
        url: '/OrganizationLibrary/GetOrganizationsDisplay',
        method: 'GET',
    })
}