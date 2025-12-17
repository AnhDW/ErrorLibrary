function getRoles() {
    return ajaxRequest({
        url: '/RoleLibrary/GetRoles',
        method: 'GET',
    })
}

function getCustomRoles() {
    return ajaxRequest({
        url: '/RoleLibrary/GetCustomRoles',
        method: 'GET',
    })
}

function getRoleById(id) {
    return ajaxRequest({
        url: '/RoleLibrary/GetRoleById',
        method: 'GET',
        data: { id: id }
    })
}

function addRole(roleDto) {
    return ajaxRequest({
        url: '/RoleLibrary/AddRole',
        method: 'POST',
        data: roleDto,
        showLoading: true
    })
}

function updateRole(roleDto) {
    return ajaxRequest({
        url: '/RoleLibrary/UpdateRole',
        method: 'POST',
        data: roleDto,
        showLoading: true
    })
}

function deleteRole(id) {
    return ajaxRequest({
        url: '/RoleLibrary/DeleteRole',
        method: 'POST',
        data: id,
        showLoading: true
    })
}