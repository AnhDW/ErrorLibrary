function getPermissions() {
    return ajaxRequest({
        url: '/PermissionLibrary/GetPermissions',
        method: 'GET',
    })
}

function getTreePermissions() {
    return ajaxRequest({
        url: '/PermissionLibrary/GetTreePermissions',
        method: 'GET',
    })
}

function getPermissionById(id) {
    return ajaxRequest({
        url: '/PermissionLibrary/GetPermissionById',
        method: 'GET',
        data: { id: id }
    })
}

function addPermission(permissionDto) {
    return ajaxRequest({
        url: '/PermissionLibrary/AddPermission',
        method: 'POST',
        data: permissionDto,
        showLoading: true
    })
}

function updatePermission(permissionDto) {
    return ajaxRequest({
        url: '/PermissionLibrary/UpdatePermission',
        method: 'POST',
        data: permissionDto,
        showLoading: true
    })
}

function deletePermission(id) {
    return ajaxRequest({
        url: '/PermissionLibrary/DeletePermission',
        method: 'POST',
        data: id,
        showLoading: true
    })
}