async function editShowUserModalHandle(userId) {
    var user = await getUserById(userId);

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
    const unit = $('#addUnit').val();


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

    // const unit = $('#editUnit').val();

    const userData = {
        id,
        code: username,
        fullName,
        email,
        avatarUrl
    };
    console.log(userData);
    updateUser(userData).then(function (res) {
        $('#editModel').modal('hide');
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

function handleDeleteUser(id) {
    deleteUser(id).then(function (res) {
        renderUsersTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function getUsers() {
    return ajaxRequest({
        url: '/UserLibrary/GetUsers',
        method: 'GET',
    });
}

function getUserById(id) {
    return ajaxRequest({
        url: '/UserLibrary/GetUserById',
        method: 'GET',
        data: { id: id },
    }); 
}

function addUser(userDto) {
    const formData = new FormData();

    formData.append("email", userDto.email);
    formData.append("code", userDto.code);
    formData.append("fullName", userDto.fullName);
    formData.append("password", userDto.password);
    formData.append("confirmPassword", userDto.confirmPassword);
    formData.append("file", $("#addAvatarFile")[0].files[0])
    console.log('đã vào');

    return ajaxRequest({
        url: '/UserLibrary/AddUser',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true
    });
}

function updateUser(userDto) {
    const formData = new FormData();

    formData.append("id", userDto.id);
    formData.append("email", userDto.email);
    formData.append("code", userDto.code);
    formData.append("fullName", userDto.fullName);
    formData.append("password", userDto.password);
    formData.append("confirmPassword", userDto.confirmPassword);
    formData.append("avatarUrl", userDto.avatarUrl);
    formData.append("file", $("#editAvatarFile")[0].files[0]);

    return ajaxRequest({
        url: '/UserLibrary/UpdateUser',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true
    });
}

function deleteUser(id) {
    // return $.ajax({
    //     url: '/UserLibrary/DeleteUser',
    //     type: 'POST',
    //     contentType: 'application/json',
    //     data: JSON.stringify(id)
    // });
    return ajaxRequest({
        url: '/UserLibrary/DeleteUser',
        method: 'POST',
        data: id,
        showLoading: true
    });
}