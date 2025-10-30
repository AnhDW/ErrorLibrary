function GetOrganizationTree() {
    return ajaxRequest({
        url: '/OrganizationLibrary/GetOrganizationTree',
        method: 'GET',
    })
}