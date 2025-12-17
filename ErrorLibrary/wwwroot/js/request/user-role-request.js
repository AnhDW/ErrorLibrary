function getUserIdsByRoleId(roleId) {
    return ajaxRequest({
        url: '/UserRoleLibrary/GetUserIdsByRoleId',
        method: 'GET',
        data: { roleId: roleId }
    })
}

function getRoleIdsByUserId(userId) {
    return ajaxRequest({
        url: '/UserRoleLibrary/GetRoleIdsByUserId',
        method: 'GET',
        data: { userId: userId }
    })
}

function updateRolesByUser(updateRolesByUserDto) {
    return ajaxRequest({
        url: '/UserRoleLibrary/UpdateRolesByUser',
        method: 'POST',
        data: updateRolesByUserDto,
        showLoading: true
    })
}