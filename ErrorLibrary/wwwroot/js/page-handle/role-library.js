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

//request
