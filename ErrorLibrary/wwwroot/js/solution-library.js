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
    const code = $('#addSolutionCode').val();
    const po = $('#addSolutionPO').val();
    const SolutionCategoryId = $('#addSolutionCategory').val();

    const SolutionData = {
        code,
        po,
        SolutionCategoryId
    };
    addSolution(SolutionData).then(function (res) {
        $('#addModel').modal('hide');
        renderSolutionTable();
    }).catch(function (err) {
        alert('Có lỗi xảy ra khi thêm');
    });
}

function handleEditSolution() {
    const id = $('#editSolutionId').val();
    const code = $('#editSolutionCode').val();
    const po = $('#editSolutionPO').val();
    const SolutionCategoryId = $('#editSolutionCategory').val();
    const SolutionData = {
        id,
        code,
        po,
        SolutionCategoryId
    };
    deleteSolution(SolutionData).then(function (res) {
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
                        <td><img src="~/assets/img/avatars/1.png" alt="Product" style="width: 60px;" /></td>
                        <td><img src="~/assets/img/avatars/1.png" alt="Product" style="width: 60px;" /></td>
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
        $('#SolutionTableBody').html(html);
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

function addSolution(SolutionDto) {
    return $.ajax({
        url: '/SolutionLibrary/AddSolution',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(SolutionDto)
    });
}

function updateSolution(SolutionDto) {
    return $.ajax({
        url: '/SolutionLibrary/UpdateSolution',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(SolutionDto)
    });
}

function deleteSolution(id) {
    console.log(id)
    return $.ajax({
        url: '/SolutionLibrary/DeleteSolution',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(id)
    });
}