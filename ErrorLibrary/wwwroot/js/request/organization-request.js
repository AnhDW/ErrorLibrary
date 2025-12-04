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