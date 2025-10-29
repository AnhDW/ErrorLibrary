async function addShowSolutionModalHandle() {
    const data = await getErrorsForSolution();
    let html = '<option selected="">Chọn chủng loại</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.code}</option>`;
    })
    $('#addErrorCodeSelect').html(html);
}

async function editShowSolutionModalHandle(solutionId) {
    const data = await getErrorsForSolution();
    let html = '<option selected="">Chọn chủng loại</option>';
    data.forEach(item => {
        html += `<option value="${item.id}">${item.code}</option>`;
    })
    var solution = await getSolutionById(solutionId);
    $('#editErrorCodeSelect').html(html);
    $('#editSolutionId').val(solution.id);
    $('#editSolutionCause').val(solution.cause);
    $('#editErrorCodeSelect').val(solution.errorId);
    $('#editSolutionHandle').val(solution.handle);
    $('#editBeforeImageUrl').val(solution.beforeUrl);
    $('#editAfterImageUrl').val(solution.afterUrl);
}

function handleAddSolution() {
    const cause = $('#addSolutionCause').val();
    const errorId = $('#addErrorCodeSelect').val();
    const handle = $('#addSolutionHandle').val();

    const solutionData = {
        cause,
        errorId,
        handle
    };
    addSolution(solutionData).then(function (res) {
        $('#addModel').modal('hide');
        renderSolutionTable();
    }).catch(function (err) {
        alert('Có lỗi xảy ra khi thêm');
    });
}

function handleEditSolution() {
    const id = $('#editSolutionId').val();
    const cause = $('#editSolutionCause').val();
    const errorId = $('#editErrorCodeSelect').val();
    const handle = $('#editSolutionHandle').val();
    const beforeUrl = $('#editBeforeImageUrl').val();
    const afterUrl = $('#editAfterImageUrl').val();
    const solutionData = {
        id,
        cause,
        errorId,
        handle,
        beforeUrl,
        afterUrl
    };
    updateSolution(solutionData).then(function (res) {
        $('#editModel').modal('hide');
        renderSolutionTable();
    }).catch(function (err) {
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function handleDeleteSolution(id) {
    deleteSolution(id).then(function (res) {
        renderSolutionTable();
    }).catch(function (err) {
        alert('Có lỗi xảy ra khi cập nhật');
    });
}

function renderSolutionTable() {
    getSolutions().then(function (data) {
        let html = '';
        data.forEach(item => {
            html += `
                <tr>
                    <td>${item.error.code}</td>
                    <td>${item.cause}</td>
                    <td>${item.handle}</td>
                    <td><img src="${item.beforeUrl}" alt="Product" style="width: 60px;" /></td>
                    <td><img src="${item.afterUrl}" alt="Product" style="width: 60px;" /></td>
                    <td>
                        <div class="dropdown">
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                <i class="bx bx-dots-vertical-rounded"></i>
                            </button>
                            <div class="dropdown-menu">
                                <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                        data-bs-target="#editModel" onclick="editShowSolutionModalHandle(${item.id})">
                                    <i class="bx bx-edit-alt me-1"></i> Sửa
                                </button>
                                <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteSolution(${item.id})"><i class="bx bx-trash me-1"></i> Xóa</a>
                            </div>
                        </div>
                    </td>
                </tr>
                `;
        });
        $('#solutionTableBody').html(html);
    });
}

//request

