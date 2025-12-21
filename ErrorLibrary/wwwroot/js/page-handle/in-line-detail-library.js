let inLine = {
    id:0,
    lineId: 0, productId: 0,
    userId: JSON.parse(localStorage.getItem('user')).id,
    date: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(0, 10),
    quantity,
    isActive: true,
    isFinalized: false
};
let firstLoadInLine = true;
let currentUserId = JSON.parse(localStorage.getItem('user')).id;

//async function initOrganizationTree() {
//    $('.dropdown-menu').on('click', function (e) {
//        e.stopPropagation();
//    });
//    var userId = inLine.userId;
//    var selectedIds = (await getOrganizationsByUserId(userId)).result;
//    var organizationIds = selectedIds.map(x => { return x.organizationType + "_" + x.organizationId })

//    const tree = (await getOrganizationTreeDropdown()).result;
//    var filteredTree = filterTree(tree, organizationIds);

//    $('#treeOrganization').treeview({
//        data: filteredTree, 
//        levels: 1,                        // Thu gọn toàn bộ
//        expandIcon: 'fa fa-chevron-right',
//        collapseIcon: 'fa fa-chevron-down',
//        showBorder: false,
//        highlightSelected: true,
//        onNodeSelected: function (event, node) {
//            if (!node.id.startsWith("line_")) {
//                $('#treeOrganization').treeview('unselectNode', [node.nodeId, { silent: true }]);
//                $('#treeOrganization').treeview('toggleNodeExpanded', [node.nodeId]);
//                return;
//            }

//            $('#selectedOrganizationNode').val(node.id);
//            $('#btnTreeOrganization').text(node.text);
//            inLine.lineId = $('#selectedOrganizationNode').val().replace("line_", "");
//            checkAndInitInLine();

//            let dd = bootstrap.Dropdown.getOrCreateInstance(
//                document.getElementById('btnTreeOrganization')
//            );
//            dd.hide();
//        },
//        onNodeExpanded: function (event, node) {
//            //console.log("Đã mở:", node.text);
//        },
//        onNodeCollapsed: function (event, node) {
//            //console.log("Đã đóng:", node.text);
//        }
//    });
//    let allNodes = $('#treeOrganization').treeview('getEnabled');
//    let leaf = {};
//    if (inLine.lineId !== 0) {
//        var lineNode = 'line_' + inLine.lineId;
//        leaf = allNodes.find(n => n.id && n.id.includes(lineNode));
//    } else {
//        leaf = allNodes.find(n => n.id && n.id.startsWith("line_"));
//    }
//    if (leaf) {
//        $('#treeOrganization').treeview('selectNode', [leaf.nodeId]);
//    }
//}

async function initialInLineDetailPage() {
    var products = (await getProducts()).result;
    var html = renderSelectOptionsByField(products, 'Chọn sản phẩm', 'id', 'code', 'productCategoryId');
    $('#selectProductCode').html(html);
    await renderLineDropdown();
    var user = (await getUserById(inLine.userId)).result;
    $('#user').val(user.fullName);
    $('#date').val(inLine.date);
    selectFirstItem('#lineDropdown');
    await checkParams();

    await Promise.all([
        renderTimeFrameCard(),
        renderInLineDetailTable()
    ]);
}

async function renderLineDropdown() {
    var organizations = (await getOrganizationsDisplay()).result;
    var userId = JSON.parse(localStorage.getItem('user')).id;
    var organizationIds = (await getOrganizationsByUserId(userId)).result;
    var lineIds = organizationIds.filter(x => x.organizationType == 'line').map(x => x.organizationId);
    organizations = organizations.filter(x => lineIds.includes(x.id));
    $('#lineDropdown').html(renderSelectDropdown(organizations, 'id', 'lineName'));
}

async function renderTimeFrameCard() {
    var timeFrames = (await getTimeFrames()).result;
    let html = '';
    for (const item of timeFrames) {
        let quantity = 0;
        let timeFrameColor = { hexCode : '#3EE0CD'};
        if (inLine && inLine.id !== 0) {
            quantity = (await getQuantityByInLineAndTimeFrame(inLine.id, item.id)).result;
            timeFrameColor = (await getTimeFrameColorByQuantity(item.id, quantity)).result;
        }

        html += `
            <div class="col-md-6 col-lg-4 col-xxl-3">
                <div class="card card-custom text-white mb-3" style="background:${timeFrameColor.hexCode};" onclick="initAddModal(${item.id})">
                    <div class="card-header"><h3 class="text-white m-0"><i class="fas fa-clock"></i> ${item.name}</h3></div>
                    <div class="card-body">
                        <h2 class="card-title text-white m-0">${quantity} lỗi</h2>
                    </div>
                </div>
            </div>`;
    }

    $('#timeFrameCard').html(html);
}

async function renderInLineDetailTable() {
    if (inLine.id === 0) return;
    var inLineDetails = (await getInLineDetailsByInLine(inLine.id)).result;
    let html = '';
    inLineDetails.forEach((item, index) => {
        html += `<tr>
            <td>${index + 1}</td>
            <td>${item.timeFrame.name}</td>
            <td>${item.createAt.substring(11, 16) }</td>
            <td>${item.error.errorGroup.name}</td>
            <td>${item.error.code}</td>
            <td>${item.error.name}</td>
            <td>${item.quantity}</td>
            <td>
                <div class="d-flex gap-2">
                    <button type="button" class="btn btn-outline-info btn-sm" onclick='initEditModal(${JSON.stringify(item).replace(/'/g, "\\'")})'>
                        <i class="bx bx-edit-alt"></i>
                    </button>
                    <button type="button" class="btn btn-danger btn-sm" onclick="handleDeleteInLineDetail(${item.id})">
                        <i class="bx bx-trash"></i>
                    </button>
                </div>
            </td>
        </tr>` });
    $('#inLineDetailTableBody').html(html);
}

function checkAndInitInLine() {
    //tạo 1 in line nếu chưa có
    var lineId = $('#lineDropdown .select-input').attr('data-value');
    var productId = $('#selectProductCode').val();
    var userId = inLine.userId;
    var date = $('#date').val();
    var quantity = $('#quantity').val();
    var isActive = inLine.isActive;
    var isFinalized = inLine.isFinalized;
    if (!lineId || !productId || !userId || !date) {
        return;
    }

    if (lineId != inLine.lineId || productId != inLine.productId || userId != inLine.userId || date != inLine.date) {
        firstLoadInLine = true;
    }

    var inLineDto = {
        lineId, productId, userId, date, quantity, firstLoad: firstLoadInLine, isActive, isFinalized
    }

    $('#formWrapper').removeClass('shake border border-2 border-danger p-2 rounded');
    checkInitAndUpdateInLine(inLineDto).then(function (res) {

        inLine = res.result;
            console.log('alo');
        if (firstLoadInLine) {
            firstLoadInLine = false;
            $('#quantity').val(inLine.quantity);
        }
        setBtnStatus();
        renderTimeFrameCard();
        renderInLineDetailTable();
        //resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });

}

function setBtnStatus() {
    if (inLine.isFinalized) {
        $('#btnIsFinalized').removeClass('btn-success').addClass('btn-secondary');
    } else {
        $('#btnIsFinalized').removeClass('btn-secondary').addClass('btn-success');
    }
    if (inLine.isActive) {
        $('#btnIsActive').removeClass('btn-secondary').addClass('btn-danger');
        $('#btnIsFinalized').removeClass('d-none');
    } else {
        $('#btnIsActive').removeClass('btn-danger').addClass('btn-secondary');
        $('#btnIsFinalized').addClass('d-none');
    }
}

async function checkParams() {
    const params = new URLSearchParams(window.location.search);
    const inLineId = params.get("inLineId");
    if (inLineId === null) return;

    var inLineById = (await getInLineById(inLineId)).result;
    console.log(inLineById.lineId);
    var user = (await getUserById(inLineById.userId)).result;

    inLine = inLineById;
    $('#selectProductCode').val(inLineById.productId);
    $('#user').val(user.fullName);
    $('#quantity').val(inLineById.quantity);
    setSelectDropdownValue('#lineDropdown', inLineById.lineId);
}

async function initAddModal(timeFrameId) {
    if (inLine.userId !== currentUserId) {
        toastr.error("Bạn không có quyền thêm lỗi trong InLine này.");
        return;
    }

    if (inLine.id === 0) {
        toastr.warning("Bạn chưa chọn InLine");
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }

    var modal = new bootstrap.Modal(document.getElementById('addModel1'));
    modal.show();
    //render lỗi theo chủng loại sản phẩm
    $('#timeFrameId').val(timeFrameId);

    var errorGroups = (await getErrorGroupsByProduct(inLine.productId)).result;
    var html = '';
    errorGroups.forEach(item => {
        html += `
            <button type="button" class="btn btn-outline-primary error-group" onclick="renderErrorButton(${item.id})">${item.name}</button>
        `
    });
    $('#errorList').html('');
    $('#errorGroupList').html(html);

}

async function renderErrorButton(errorGroupId){
    let productCategoryId = $('#selectProductCode option:selected').data('extraField');
    var errors = (await getErrorsByErrorGroupAndProductCategory(errorGroupId, productCategoryId)).result;
    var html = '';
    console.log(errors);
    errors.forEach(item => {
        html += `
            <button type="button" class="btn btn-outline-primary error-group" onclick="setErrorId(${item.id})">${item.code}-${item.name}</button>
        `
    });
    $('#errorList').html(html);
}

function setErrorId(errorId){
    $('#errorId').val(errorId);
}
//async function initAddModal(timeFrameId) {
//    if (inLine.userId !== currentUserId) {
//        toastr.error("Bạn không có quyền thêm lỗi trong InLine này.");
//        return;
//    }

//    if (inLine.id === 0) {
//        toastr.warning("Bạn chưa chọn InLine");
//        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
//        return;
//    }

//    var modal = new bootstrap.Modal(document.getElementById('addModel'));
//    modal.show();
//    //render lỗi theo chủng loại sản phẩm
//    $('#timeFrameId').val(timeFrameId);

//    var errorGroups = (await getErrorGroupsByProduct(inLine.productId)).result;
//    var html = renderSelectOptions(errorGroups, 'Chọn nhóm lỗi');
//    $('#selectedErrorGroup').html(html);
//}

async function initEditModal(inLineDetail) {
    if (inLine.userId !== currentUserId) {
        toastr.error("Bạn không có quyền cập nhật lỗi trong InLine này.");
        return;
    }
    var modal = new bootstrap.Modal(document.getElementById('editModel'));
    modal.show();
    var errorGroups = (await getErrorGroups()).result;
    var errorGroupsHtml = renderSelectOptions(errorGroups, 'Chọn nhóm lỗi');
    var errors = (await getErrors()).result;
    var errorsHtml = renderSelectOptions(errors, 'Chọn lỗi');
    $('#editSelectedErrorGroup').html(errorGroupsHtml);
    $('#editSelectedError').html(errorsHtml);

    $('#editInLineDetailId').val(inLineDetail.id);
    $('#editInLineId').val(inLineDetail.inLineId);
    $('#editTimeFrameId').val(inLineDetail.timeFrameId);
    
    $('#editSelectedErrorGroup').val(inLineDetail.error.errorGroupId);
    $('#editSelectedError').val(inLineDetail.errorId);
    $('#editQuantityInLine').val(inLineDetail.quantity);
    $('#editCreateAt').val(inLineDetail.createAt);

}

function handleAddInLineDetail() {
    var inLineId = inLine.id;
    var errorId = $('#errorId').val();
    var timeFrameId = $('#timeFrameId').val();
    var quantity = $('#quantityInLine').val();
    var inLineDetailDto = {
        errorId, inLineId, timeFrameId, quantity,
        createAt: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString(),
        updateAt: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString()
    }
    addInLineDetail(inLineDetailDto).then(function (res) {
        renderTimeFrameCard();
        renderInLineDetailTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleEditInLineDetail() {
    var id = $('#editInLineDetailId').val();
    var errorId = $('#editSelectedError').val();
    var inLineId = $('#editInLineId').val();
    var timeFrameId = $('#editTimeFrameId').val();
    var quantity = $('#editQuantityInLine').val();
    var createAt = $('#editCreateAt').val();
    var inLineDetailDto = {
        id,
        errorId, inLineId, timeFrameId, quantity, createAt,
        updateAt: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString(),
    }

    updateInLineDetail(inLineDetailDto).then(function (res) {
        renderTimeFrameCard();
        renderInLineDetailTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleDeleteInLineDetail(id) {
    if (inLine.userId !== currentUserId) {
        toastr.error("Bạn không có quyền xóa lỗi trong InLine này.");
        return;
    }
    deleteInLineDetail(id).then(function (res) {
        renderTimeFrameCard();
        renderInLineDetailTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

//Event listeners
$('#selectProductCode, #date, #quantity').on('change keyup', function () {
    checkAndInitInLine();
});

$('#btnIsFinalized').on('click', function () {
    if (inLine.id === 0) {
        toastr.warning("Bạn chưa chọn InLine");
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
    if (inLine.isFinalized === false) {
        inLine.isFinalized = true;
    } else {
        inLine.isFinalized = false;
    }
    checkAndInitInLine();
});

$('#btnIsActive').on('click', function () {
    if (inLine.id === 0) {
        toastr.warning("Bạn chưa chọn InLine");
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
    if (inLine.isActive === false) {
        inLine.isActive = true;
    } else {
        inLine.isActive = false;
    }
    checkAndInitInLine();
});

$("#toggleFormBtn").on("click", function () {
    const wrapper = document.getElementById("formWrapper");
    const icon = document.getElementById("toggleIcon");

    wrapper.classList.toggle("d-none");

    if (wrapper.classList.contains("d-none")) {
        this.innerHTML = '<i class="fas fa-angle-double-down"></i>';
    } else {
        this.innerHTML = '<i class="fas fa-angle-double-up"></i>';
    }
});

$('#addBtnIncreases').on('click', () => {
    var quantity = $('#quantityInLine').val();
    $('#quantityInLine').val(parseInt(quantity) + 1);
})

$('#addBtnDecreases').on('click', () => {
    var quantity = $('#quantityInLine').val();
    $('#quantityInLine').val(parseInt(quantity) - 1);
})

$('#editBtnIncreases').on('click', () => {
    var quantity = $('#editQuantityInLine').val();
    $('#editQuantityInLine').val(parseInt(quantity) + 1);
})

$('#editBtnDecreases').on('click', () => {
    var quantity = $('#editQuantityInLine').val();
    $('#editQuantityInLine').val(parseInt(quantity) - 1);
})

$('#selectedErrorGroup').on('change', async function () {
    let productCategoryId = $('#selectProductCode option:selected').data('extraField');
    let errorGroupId = $(this).val();
    var errors = (await getErrorsByErrorGroupAndProductCategory(errorGroupId, productCategoryId)).result;
    var html = renderSelectErrorOptions(errors, 'Chọn lỗi');

    $('#selectedError').prop('disabled', false);
    $('#selectedError').html(html);

});

$('#editSelectedErrorGroup').on('change', async function () {
    let productCategoryId = $('#selectProductCode option:selected').data('extraField');
    let errorGroupId = $(this).val();
    var errors = (await getErrorsByErrorGroupAndProductCategory(errorGroupId, productCategoryId)).result;
    var html = renderSelectErrorOptions(errors, 'Chọn lỗi');

    $('#editSelectedError').prop('disabled', false);
    $('#editSelectedError').html(html);

});

$('#lineDropdown').on('change', '.select-input', function () {
    checkAndInitInLine();
});


