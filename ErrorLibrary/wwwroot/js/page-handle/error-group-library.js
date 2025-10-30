function addShowErrorGroupModalHandle() {
    console.log('show add')
}

async function editShowErrorGroupModalHandle(id) {
    console.log('show edit')
    var errorGroup = await getErrorGroupById(id);
    $('#editErrorGroupId').val(errorGroup.id);
    $('#editErrorGroupName').val(errorGroup.name);
    $('#editErrorGroupDescription').val(errorGroup.description);

}

function handleAddErrorGroup() {

    const name = $('#addErrorGroupName').val();
    const description = $('#addErrorGroupDescription').val();
    const errorGroupData = {
        name,
        description
    };
    addErrorGroup(errorGroupData).then(function (res) {
        $('#addModel').modal('hide');
        renderErrorGroupTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleEditErrorGroup() {

    const id = $('#editErrorGroupId').val();
    const name = $('#editErrorGroupName').val();
    const description = $('#editErrorGroupDescription').val();
    const errorGroupData = {
        id,
        name,
        description
    };
    updateErrorGroup(errorGroupData).then(function (res) {
        $('#editModel').modal('hide');
        renderErrorGroupTable(); // ✅ chỉ gọi sau khi update thành công
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteErrorGroup(id) {
    deleteErrorGroup(id).then(function (res) {
        renderErrorGroupTable();
    }).catch(function (err) {
        console.error(err);
        alert('Có lỗi xảy ra khi xóa');
    });
}

function renderErrorGroupTable() {
    getErrorGroups().then(function (data) {
        console.log(data)
        let html = '';
        data.forEach(item => {
            html += `
                    <tr>
                            <td>${item.name}</td>
                            <td>${item.description}</td>
                            <td>
                                <div class="dropdown">
                                    <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                        <i class="bx bx-dots-vertical-rounded"></i>
                                    </button>
                                    <div class="dropdown-menu">
                                        <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                                data-bs-target="#editModel" onclick="editShowErrorGroupModalHandle(${item.id})">
                                            <i class="bx bx-edit-alt me-1"></i> Sửa
                                        </button>
                                        <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteErrorGroup(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    `;
        });
        $('#errorGroupTableBody').html(html);
    });
}

//request
