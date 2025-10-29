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
    return ajaxRequest({
        url: '/UserLibrary/DeleteUser',
        method: 'POST',
        data: id,
        showLoading: true
    });
}