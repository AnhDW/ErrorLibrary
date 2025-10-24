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
    console.log( solution);
    $('#editErrorCodeSelect').html(html);
    $('#editSolutionId').val(solution.id);
    $('#editSolutionCause').val(solution.cause);
    $('#editErrorCodeSelect').val(solution.errorId);
    $('#editSolutionHandle').val(solution.handle);
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
    console.log(solutionData);
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

    const solutionData = {
        id,
        cause,
        errorId,
        handle
    };
    console.log(solutionData);
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
        console.log(html, data);
    });
}
function getErrorsForSolution() {
    return $.get('/SolutionLibrary/GetErrorsForSolution');
}

function getSolutions() {
    return $.get('/SolutionLibrary/GetSolutions');
}

function getSolutionById(id) {
    return $.get('/SolutionLibrary/GetSolutionById', { id: id });
}

function addSolution(solutionDto) {
    const formData = new FormData();

    formData.append("errorId", solutionDto.errorId);
    formData.append("cause", solutionDto.cause);
    formData.append("handle", solutionDto.handle);
    formData.append("beforeFile", $("#addBeforeImage")[0].files[0]);
    formData.append("afterFile", $("#addAfterImage")[0].files[0]);

    return $.ajax({
        url: '/SolutionLibrary/AddSolution',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false
    });
}

function updateSolution(solutionDto) {
    const formData = new FormData();
    formData.append("id", solutionDto.id);
    formData.append("errorId", solutionDto.errorId);
    formData.append("cause", solutionDto.cause);
    formData.append("handle", solutionDto.handle);
    formData.append("beforeFile", $("#editBeforeImage")[0].files[0]);
    formData.append("afterFile", $("#editAfterImage")[0].files[0]);

    console.log(formData);
    return $.ajax({
        url: '/SolutionLibrary/UpdateSolution',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false
    });
}

function deleteSolution(id) {
    return $.ajax({
        url: '/SolutionLibrary/DeleteSolution',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(id)
    });
}