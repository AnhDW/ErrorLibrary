function addShowTimeFrameModalHandle() {
    $('#addStartTime').val(new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(11, 16));
    $('#addEndTime').val(new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(11, 16));
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

function handleDeleteTimeFrame(id) {
    deleteTimeFrame(id).then(function (res) {
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
let currentTimeFrameId = 0;
async function showColorsModal(timeFrameId) {
    currentTimeFrameId = timeFrameId;
    renderColorCards(timeFrameId);
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

async function renderColorCards(timeFrameId) {
    const timeFrameColors = (await getByTimeFrame(timeFrameId)).result;
    let container = $("#colorCardContainer");
    container.empty();
    container.append(`
        <div class="col-12 col-md-6 col-lg-4">
            <div class="p-0 color-card d-flex justify-content-between" style="background:#3ee08c68">
                <div class="m-2">
                    <div class="input-group input-group-sm m-1">
                        <input type="color" id="newColorHex" class="form-control form-control-color" value="#3EE0CD"  style="border:none;">
                        <input type="text" id="newHexText" class="form-control" value="#3ee0cd" style="border:none;">
                    </div>
                    <div class="input-group input-group-sm m-1">
                        <span class="input-group-text">Min</span>
                        <input type="number" id="newMinQty" class="form-control" placeholder="Min" value="0">
                        <input type="number" id="newMaxQty" class="form-control" placeholder="Max" value="0">
                        <span class="input-group-text">Max</span>
                    </div>
                </div>

                <button class="btn btn-sm btn-add" onclick="handleAddTimeFrameColor(${timeFrameId})"><i class="fas fa-plus"></i></button>
            </div>
        </div>
    `);
    timeFrameColors.forEach(c => {
        container.append(`
            <div class="col-12 col-md-6 col-lg-4">
                <div class="p-0 color-card d-flex justify-content-between" style="background:${c.hexCode}68" id="color_card_${c.id}">
                    <div class="d-flex align-items-center m-2">
                        <input class="form-check-input me-2 check-color d-none" type="checkbox" value="${c.id}">
                        <div class="color-preview me-2" style="background:${c.hexCode}"></div>
                        <div>
                            <div class="fw-bold">${c.hexCode}</div>
                            <small>Min: ${c.minQuantity} – Max: ${c.maxQuantity}</small>
                        </div>
                    </div>

                    <div class="d-flex flex-column justify-content-end">
                        <button class="btn btn-sm btn-edit btn-custom flex-grow-1" onclick="showEditColorCard(${c.id})"><i class="fas fa-pen"></i></button>
                        <button class="btn btn-sm btn-delete btn-custom flex-grow-1" onclick="handleDeleteTimeFrameColor(${c.id}, ${c.timeFrameId})"><i class="fas fa-times"></i></button>
                    </div>
                </div>

                
            </div>
        `);
    });
}

function editColorCard(timeFrameColor) {
    return `<div class="p-0 color-card d-flex justify-content-between" style="background:${timeFrameColor.hexCode}68" id="edit_color_card">
                <div class="m-2">
                    <div class="input-group input-group-sm m-1">
                        <input type="color" id="editColorHex" class="form-control form-control-color" value="${timeFrameColor.hexCode}"  style="border:none;">
                        <input type="text" id="editHexText" class="form-control" value="${timeFrameColor.hexCode}" style="border:none;">
                    </div>
                    <div class="input-group input-group-sm m-1">
                        <span class="input-group-text">Min</span>
                        <input type="number" id="editMinQty" class="form-control" placeholder="Min" value="${timeFrameColor.minQuantity}">
                        <input type="number" id="editMaxQty" class="form-control" placeholder="Max" value="${timeFrameColor.maxQuantity}">
                        <span class="input-group-text">Max</span>
                    </div>
                </div>

                <button class="btn btn-sm btn-add" onclick="handleUpdateTimeFrameColor(${timeFrameColor.id}, ${timeFrameColor.timeFrameId})">
                    <i class="fas fa-check"></i>
                </button>
            </div>`;
}


$(document).on("input", "#newColorHex", function () {
    $("#newHexText").val($(this).val());
});

$(document).on("input", "#newHexText", function () {
    $("#newColorHex").val($(this).val());
});

$(document).on("input", "#editColorHex", function () {
    $("#editHexText").val($(this).val());
});

$(document).on("input", "#editHexText", function () {
    $("#editColorHex").val($(this).val());
});

async function showEditColorCard(timeFrameColorId) {
    var timeFrameColor = (await getTimeFrameColorById(timeFrameColorId)).result;
    //var card = $(`#color_card_${timeFrameColorId}`);
    const card = document.getElementById(`color_card_${timeFrameColorId}`);
    console.log(card);
    card.outerHTML = editColorCard(timeFrameColor);
}

function handleAddTimeFrameColor(timeFrameId){
    var hexCode = $("#newHexText").val();
    var minQuantity = $("#newMinQty").val();
    var maxQuantity = $("#newMaxQty").val();
    const timeFrameColorDto = {
        timeFrameId,
        hexCode, minQuantity, maxQuantity
    }
    addTimeFrameColor(timeFrameColorDto).then(function (res) {
        renderColorCards(timeFrameId);
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleUpdateTimeFrameColor(id, timeFrameId) {
    var hexCode = $(`#editHexText`).val();
    var minQuantity = $(`#editMinQty`).val();
    var maxQuantity = $(`#editMaxQty`).val(); 
    const timeFrameColorDto = {
        id, timeFrameId,
        hexCode, minQuantity, maxQuantity
    }
    console.log(timeFrameColorDto)
    updateTimeFrameColor(timeFrameColorDto).then(function (res) {
        renderColorCards(timeFrameId);
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleDeleteTimeFrameColor(id, timeFrameId) {
    deleteTimeFrameColor(id).then(function (res) {
        renderColorCards(timeFrameId);
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

$(document).on("click", "#btnSelectColor", function () {
    let checkBoxes = $('.check-color');
    let btnCopy = $('#btnCopyColor');

    checkBoxes.toggleClass('d-none');
    btnCopy.removeClass('d-none');
    if (checkBoxes.hasClass('d-none')) {
        // Khi ẩn đi thì reset trạng thái
        checkBoxes.prop('checked', false);
    }
});

$(document).on("click", "#btnCopyColor", function () {
    $('.check-color').removeClass('d-none');
    $('#btnCopyColor').addClass('d-none');
    let selectedIds = $('.check-color:checked').map(function () {
        return $(this).val();
    }).get();

    localStorage.setItem('selectedTimeFrameColorIds', JSON.stringify(selectedIds));
    console.log(selectedIds);
    if (selectedIds.length === 0) {
        toastr.warning("No colors selected to copy.");
        return;
    }

    $('#btnPasteColor').removeClass('d-none');
    navigator.clipboard.writeText(JSON.stringify(selectedIds))
        .then(() => toastr.success("Copied!"))
        .catch(err => toastr.error(err));
});

$(document).on("click", "#btnPasteColor", function () {
    $('#btnPasteColor').addClass('d-none');

    navigator.clipboard.readText()
        .then(async text => {
            try {
                const ids = JSON.parse(text);
                console.log("IDs:", ids);
                const copyAndPasteColorDto = {
                    timeFrameId: currentTimeFrameId,
                    timeFrameColorIds : ids
                }
                const res = await copyAndPasteColor(copyAndPasteColorDto);
                resToastr(res);
                renderColorCards(currentTimeFrameId);
            } catch {
                console.log("Clipboard không phải JSON array");
            }
        });

});
