function addShowErrorGroupModalHandle() {
    console.log('show add')
}

async function editShowErrorGroupModalHandle(id) {
    console.log('show edit')
    var errorGroup = await getErrorGroupById(id);
    $('#editErrorGroupId').val(errorGroup.id);
    $('#editErrorGroupName').val(errorGroup.name);
    $('#editErrorGroupCode').val(errorGroup.code);
    $('#editErrorGroupDescription').val(errorGroup.description);

}

function handleAddErrorGroup() {

    const name = $('#addErrorGroupName').val();
    const code = $('#addErrorGroupCode').val();
    const description = $('#addErrorGroupDescription').val();
    const errorGroupData = {
        name,
        code,
        description
    };
    addErrorGroup(errorGroupData).then(function (res) {
        //$('#addModel').modal('hide');
        renderErrorGroupTable(); 
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleEditErrorGroup() {

    const id = $('#editErrorGroupId').val();
    const name = $('#editErrorGroupName').val();
    const code = $('#editErrorGroupCode').val();
    const description = $('#editErrorGroupDescription').val();
    const errorGroupData = {
        id,
        name,
        code,
        description
    };
    updateErrorGroup(errorGroupData).then(function (res) {
        $('#editModel').modal('hide');
        renderErrorGroupTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleDeleteErrorGroup(id) {
    deleteErrorGroup(id).then(function (res) {
        renderErrorGroupTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
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
                        <td>${item.code}</td>
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
