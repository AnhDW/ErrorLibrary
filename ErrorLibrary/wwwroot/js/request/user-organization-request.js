function getUserOrganizations() {
    return ajaxRequest({
        url: '/UserOrganizationLibrary/GetUserOrganizations',
        method: 'GET',
    })
}

function getUserOrganizationById(userId, organizationType, organizationId) {
    return ajaxRequest({
        url: '/UserOrganizationLibrary/GetUserOrganizationById',
        method: 'GET',
        data: { userId, organizationType, organizationId }
    })
}

function getUserIdsByOrganization(organizationType, organizationId) {
    return ajaxRequest({
        url: '/UserOrganizationLibrary/GetUserIdsByOrganization',
        method: 'GET',
        data: { organizationType, organizationId }
    })
}

function getOrganizationsByUserId(userId) {
    return ajaxRequest({
        url: '/UserOrganizationLibrary/GetOrganizationsByUserId',
        method: 'GET',
        data: { userId }
    })
}

function updateOrganizationsByUser(updateOrganizationsByUserDto) {
    return ajaxRequest({
        url: '/UserOrganizationLibrary/UpdateOrganizationsByUser',
        method: 'POST',
        data: updateOrganizationsByUserDto,
        showLoading: true
    })
}