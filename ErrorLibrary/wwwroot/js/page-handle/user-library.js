var addTree = $('#addTree').jstree(true);
var editTree = $('#editTree').jstree(true);
async function addShowUserModalHandle() {
    var tree = await GetOrganizationTree();
    $('#addTree')
        .jstree({
            'core': {
                'data': tree,
                'themes': { 'name': 'proton', 'responsive': true }
            },
            'plugins': ["wholerow", "checkbox"],
        })

}

async function editShowUserModalHandle(userId) {
    var user = await getUserById(userId);
    var tree = await GetOrganizationTree();
    //gán ids hiện tại
    var selectedIds = await getOrganizationsByUserId(userId);
    var organizationIds = selectedIds.map(x => { return x.organizationType + "_" + x.organizationId })
    console.log(selectedIds, organizationIds);
    if ($.jstree.reference('#editTree')) {
        $('#editTree').jstree('destroy').off(); // off để gỡ event cũ
    }

    $('#editTree')
        .on('loaded.jstree', function () {
            organizationIds.forEach(id => {
                $('#editTree').jstree('check_node', id);
            });
        })
        .jstree({
            'core': {
                'data': tree,
                'themes': { 'name': 'proton', 'responsive': true }
            },
            "plugins": ["wholerow", "checkbox"],
        })

    $('#editUserId').val(userId);
    $('#editUsername').val(user.code);
    $('#editFullName').val(user.fullName);
    $('#editEmail').val(user.email);
    $('#editAvatarUrl').val(user.avatarUrl);
}

function handleAddUser() {
    const username = $('#addUsername').val();
    const password = $('#addPassword').val();
    const fullName = $('#addFullName').val();
    const email = $('#addEmail').val();

    var selectedIds = $('#addTree').jstree('get_selected');
    console.log(selectedIds);
    var organizations = selectedIds.map(x => {
        return {
            organizationType: x.split("_")[0],
            organizationId: x.split("_")[1]
        };
    });
    console.log(selectedIds);
    const userData = {
        code: username,
        password,
        confirmPassword: password,
        fullName,
        email
    };

    addUser(userData).then(function (res) {
        $('#addModel').modal('hide');
        renderUsersTable();
        const updateOrganizationsByUserData = {
            userId: res.result.id,
            organizations: organizations
        }
        updateOrganizationsByUser(updateOrganizationsByUserData);
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditUser() {
    const id = $('#editUserId').val();
    const username = $('#editUsername').val();
    const fullName = $('#editFullName').val();
    const email = $('#editEmail').val();
    const avatarUrl = $('#editAvatarUrl').val();

    var selectedIds = $('#editTree').jstree('get_selected');
    var organizations = selectedIds.map(x => {
        return {
            organizationType: x.split("_")[0],
            organizationId: x.split("_")[1]
        }; });

    console.log(selectedIds, organizations);
    const userData = {
        id,
        code: username,
        fullName,
        email,
        avatarUrl
    };

    const updateOrganizationsByUserData = {
        userId: id,
        organizations: organizations
    }
    
    console.log(updateOrganizationsByUserData);
    updateUser(userData).then(function (res) {
        $('#editModel').modal('hide');
        renderUsersTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
    updateOrganizationsByUser(updateOrganizationsByUserData);
}

function handleDeleteUser(id) {
    deleteUser(id).then(function (res) {
        renderUsersTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderUsersTable() {
    getUsers().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                        <td>${item.code}</td>
                        <td>${item.fullName}</td>
                        <td>${item.email}</td>
                        <td>NBC</td>
                        <td><img src="${item.avatarUrl}" alt="Avatar" style="width: 60px;" /></td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick=(editShowUserModalHandle('${item.id}'))>
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <button type="button" class="dropdown-item" onclick="handleDeleteUser('${item.id}')"><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
        });
        $('#userTableBody').html(html);
    });
}
