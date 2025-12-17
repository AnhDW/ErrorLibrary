function addShowRoleModalHandle() {
    console.log('show add')
}

async function editShowRoleModalHandle(id) {
    var role = (await getRoleById(id)).result;
    console.log(role);
    $('#editRoleId').val(role.id);
    $('#editRoleName').val(role.name);
    $('#editRoleDisplayName').val(role.displayName);

}

function handleAddRole() {

    const name = $('#addRoleName').val();
    const displayName = $('#addRoleDisplayName').val();
    const roleData = {
        name,
        displayName
    };
    addRole(roleData).then(function (res) {
        $('#addModel').modal('hide');
        renderRoleTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditRole() {

    const id = $('#editRoleId').val();
    const name = $('#editRoleName').val();
    const displayName = $('#editRoleDisplayName').val();
    const roleData = {
        id,
        name,
        displayName
    };
    updateRole(roleData).then(function (res) {
        $('#editModel').modal('hide');
        renderRoleTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteRole(id) {
    deleteRole(id).then(function (res) {
        renderRoleTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi xóa');
    });
}

function renderRoleTable() {
    getRoles().then(function (res) {
        console.log(res)
        let html = '';
        res.result.forEach(item => {
            html += `
                    <tr>
                        <td>${item.name}</td>
                        <td>${item.normalizedName}</td>
                        <td>${item.displayName}</td>
                        <td>
                            <button type="button" class="btn rounded-pill btn-primary btn-sm" onclick="initPermissionModalHandle('${item.id}')">
                                <i class="fas fa-users-cog"></i>
                            </button>
                        </td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick="editShowRoleModalHandle('${item.id}')">
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteRole('${item.id}')"><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#roleTableBody').html(html);
    });
}

async function initPermissionModalHandle(roleId) {
    var permissionIds = (await getPermissionIdsByRoleId(roleId)).result;
    var permissions = (await getTreePermissions()).result;
    var modal = new bootstrap.Modal(document.getElementById('permissionModel'));
    modal.show();
    console.log(roleId, permissionIds);
    if ($.jstree.reference('#permissionTree')) {
        $('#permissionTree').jstree('destroy').off(); // off để gỡ event cũ
    }
    $('#permissionTree')
        .on('loaded.jstree', function () {
            permissionIds.forEach(id => {
                $('#permissionTree').jstree('check_node', id);
            });
        })
        .jstree({
            'core': {
                'data': permissions,
                'themes': { 'name': 'proton', 'responsive': true }
            },
            'plugins': ["wholerow", "checkbox"],
        })
    $('#roleId').val(roleId);

}

function handleSaveRolePermission() {
    var selectedIds = $('#permissionTree').jstree('get_selected');
    const roleId = $('#roleId').val();
    const permissionIds = selectedIds
        .map(x => Number(x))
        .filter(Number.isInteger);
    updatePermissionsByRoleDto = {
        roleId: roleId,
        permissionIds: permissionIds
    }
    updatePermissionsByRole(updatePermissionsByRoleDto).then(function (res) {
        resToastr(res);
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}
//request
