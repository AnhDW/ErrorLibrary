function getRoleIdsByPermissionId(permissionId) {
    return ajaxRequest({
        url: '/RolePermissionLibrary/GetRoleIdsByPermissionId',
        method: 'GET',
        data: { permissionId: permissionId }
    })
}

function getPermissionIdsByRoleId(roleId) {
    return ajaxRequest({
        url: '/RolePermissionLibrary/GetPermissionIdsByRoleId',
        method: 'GET',
        data: { roleId: roleId }
    })
}

function updatePermissionsByRole(updatePermissionsByRoleDto) {
    return ajaxRequest({
        url: '/RolePermissionLibrary/UpdatePermissionsByRole',
        method: 'POST',
        data: updatePermissionsByRoleDto,
        showLoading: true
    })
}