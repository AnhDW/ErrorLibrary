function addShowTimeFrameModalHandle() {
    $('#addStartTime').val(new Date().toISOString().substring(11, 16));
    $('#addEndTime').val(new Date().toISOString().substring(11, 16));
}

function editShowTimeFrameModalHandle(item) {
    $('#editTimeFrameId').val(item.id);
    $('#editStartTime').val(item.startTime);
    $('#editEndTime').val(item.endTime);
    $('#editTimeFrameName').val(item.name);
}

function handleAddTimeFrame() {
    var startTime = $('#addStartTime').val();
    var endTime = $('#addEndTime').val();
    var name = $('#addTimeFrameName').val();
    var timeFrameDto = {
        startTime, endTime, name
    }
    addTimeFrame(timeFrameDto).then(function (res) {
        //$('#addModel').modal('hide');
        renderTimeFrameTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleEditTimeFrame() {
    var id = $('#editTimeFrameId').val();
    var startTime = $('#editStartTime').val();
    var endTime = $('#editEndTime').val();
    var name = $('#editTimeFrameName').val();
    var timeFrameDto = {
        id,
        startTime, endTime, name
    }
    updateTimeFrame(timeFrameDto).then(function (res) {
        //$('#editModel').modal('hide');
        renderTimeFrameTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

async function setTimeFrameName(actionName) {
    if (actionName === 'add') {
        var startTime = $('#addStartTime').val();
        var endTime = $('#addEndTime').val();
        var name = (await generateTimeFrameTitle(startTime, endTime)).result;
        $('#addTimeFrameName').val(name);
    } else if (actionName === 'edit') {
        var startTime = $('#editStartTime').val();
        var endTime = $('#editEndTime').val();
        var name = (await generateTimeFrameTitle(startTime, endTime)).result;
        $('#editTimeFrameName').val(name);
    }
}

async function showColorsModal(id) {

}

function renderTimeFrameTable() {
    getTimeFrames().then(function (res) {
        console.log(res);
        let html = '';
        res.result.forEach(function (item, index) {
            console.log(item);

            html += `<tr>
                        <td>${item.startTime}</td>
                        <td>${item.endTime}</td>
                        <td>${item.name}</td>
                        <td>
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="modal" data-bs-target="#colorsModal" onclick='showColorsModal(${item.id})'>
                                <i class="fas fa-palette"></i>
                            </button>
                        </td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                            data-bs-target="#editModel" onclick='editShowTimeFrameModalHandle(${JSON.stringify(item).replace(/'/g, " \\'")})'>
                                        <i class="bx bx-edit-alt me-1"></i> Sửa
                                    </button>
                                    <a class="dropdown-item" href="javascript:void(0);" onclick='handleDeleteTimeFrame(${item.id})'><i class="bx bx-trash me-1"></i> Xóa</a>
                                </div>
                            </div>
                        </td>
                    </tr>`
        })
        $('#timeFrameTableBody').html(html);

    });
}