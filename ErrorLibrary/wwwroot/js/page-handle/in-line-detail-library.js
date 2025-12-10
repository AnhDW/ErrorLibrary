let inLine = {
    id:0,
    lineId: 0, productId: 0, userId: '', date: '0001-01-01', quantity,
};
let firstLoadInLine = true;

async function initOrganizationTree() {
    $('.dropdown-menu').on('click', function (e) {
        e.stopPropagation();
    });

    const response = await getOrganizationTreeDropdown();

    $('#treeOrganization').treeview({
        data: response.result, 
        levels: 1,                        // Thu gọn toàn bộ
        expandIcon: 'fa fa-chevron-right',
        collapseIcon: 'fa fa-chevron-down',
        showBorder: false,
        highlightSelected: true,
        onNodeSelected: function (event, node) {
            if (!node.id.startsWith("line_")) {
                $('#treeOrganization').treeview('unselectNode', [node.nodeId, { silent: true }]);
                $('#treeOrganization').treeview('toggleNodeExpanded', [node.nodeId]);
                return;
            }

            $('#selectedOrganizationNode').val(node.id);
            $('#btnTreeOrganization').text(node.text);

            checkAndInitInLine();

            let dd = bootstrap.Dropdown.getOrCreateInstance(
                document.getElementById('btnTreeOrganization')
            );
            dd.hide();
        },
        onNodeExpanded: function (event, node) {
            //console.log("Đã mở:", node.text);
        },
        onNodeCollapsed: function (event, node) {
            //console.log("Đã đóng:", node.text);
        }
    });
    let allNodes = $('#treeOrganization').treeview('getEnabled');
    let firstLeaf = allNodes.find(n => n.id && n.id.startsWith("line_"));

    if (firstLeaf) {
        $('#treeOrganization').treeview('selectNode', [firstLeaf.nodeId]);
    }
}

async function initialInLineDetailPage() {
    await Promise.all([
        initOrganizationTree(),
        renderTimeFrameCard(),
        renderInLineDetailTable()
    ]);
    var products = (await getProducts()).result;
    var user = JSON.parse(localStorage.getItem('user'));
    var html = renderSelectOptionsByField(products, 'Chọn sản phẩm', 'id', 'code', 'productCategoryId');

    $('#selectProductCode').html(html);
    $('#user').val(user.fullName);
    $('#date').val(new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(0, 10));
}

document.getElementById("toggleFormBtn").addEventListener("click", function () {
    const wrapper = document.getElementById("formWrapper");
    const icon = document.getElementById("toggleIcon");

    wrapper.classList.toggle("d-none");

    if (wrapper.classList.contains("d-none")) {
        this.innerHTML = '<i class="fas fa-angle-double-down"></i>';
    } else {
        this.innerHTML = '<i class="fas fa-angle-double-up"></i>';
    }
});

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
        console.log(timeFrameColor.hexCode);

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
                    <button type="button" class="btn btn-outline-info btn-sm" data-bs-toggle="modal"
                            data-bs-target="#editModel" onclick='initEditModal(${JSON.stringify(item).replace(/'/g, "\\'")})'>
                        <i class="bx bx-edit-alt"></i>
                    </button>
                    <button type="button" class="btn btn-danger btn-sm" data-bs-toggle="modal"
                            data-bs-target="#editModel" onclick="handleDeleteInLineDetail(${item.id})">
                        <i class="bx bx-trash"></i>
                    </button>
                </div>
            </td>
        </tr>` });
    $('#inLineDetailTableBody').html(html);
}

function checkAndInitInLine() {
    //tạo 1 in line nếu chưa có
    var lineId = ($('#selectedOrganizationNode').val().substring(5, 6));
    var productId = $('#selectProductCode').val();
    var userId = JSON.parse(localStorage.getItem('user')).id;
    var date = $('#date').val();
    var quantity = $('#quantity').val();

    console.log(lineId);
    if (!lineId || !productId || !userId || !date) {
        return;
    }

    if (lineId != inLine.lineId || productId != inLine.productId || userId != inLine.userId || date != inLine.date) {
        firstLoadInLine = true;
    }
    var inLineDto = {
        lineId, productId, userId, date, quantity, firstLoad : firstLoadInLine
    }

    $('#formWrapper').removeClass('shake border border-2 border-danger p-2 rounded');
    checkInitAndUpdate(inLineDto).then(function (res) {
        inLine = res.result;
        if (firstLoadInLine) {
            firstLoadInLine = false;
            $('#quantity').val(res.result.quantity);
        }
        renderTimeFrameCard();
        renderInLineDetailTable();
        //resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });

}

$('#selectProductCode, #date, #quantity').on('change keyup', function () {
    checkAndInitInLine();
});

async function initAddModal(timeFrameId) {
    if (inLine.id === 0) {
        toastr.warning("Bạn chưa chọn InLine");
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }

    var modal = new bootstrap.Modal(document.getElementById('addModel'));
    modal.show();
    //render lỗi theo chủng loại sản phẩm
    $('#timeFrameId').val(timeFrameId);

    var errorGroups = (await getErrorGroupsByProduct(inLine.productId)).result;
    var html = renderSelectOptions(errorGroups, 'Chọn nhóm lỗi');
    $('#selectedErrorGroup').html(html);
}

$('#selectedErrorGroup').on('change', async function () {
    let productCategoryId = $('#selectProductCode option:selected').data('extraField');
    let errorGroupId = $(this).val();
    console.log(productCategoryId, errorGroupId);
    var errors = (await getErrorsByErrorGroupAndProductCategory(errorGroupId, productCategoryId)).result;
    var html = renderSelectErrorOptions(errors, 'Chọn lỗi');
    
    $('#selectedError').prop('disabled', false);
    $('#selectedError').html(html);

});

async function initEditModal(inLineDetail) {
    console.log(inLineDetail);
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
$('#editSelectedErrorGroup').on('change', async function () {
    let productCategoryId = $('#selectProductCode option:selected').data('extraField');
    let errorGroupId = $(this).val();
    console.log(productCategoryId, errorGroupId);
    var errors = (await getErrorsByErrorGroupAndProductCategory(errorGroupId, productCategoryId)).result;
    var html = renderSelectErrorOptions(errors, 'Chọn lỗi');

    $('#editSelectedError').prop('disabled', false);
    $('#editSelectedError').html(html);

});
function handleAddInLineDetail() {
    var inLineId = inLine.id;
    var errorId = $('#selectedError').val();
    var timeFrameId = $('#timeFrameId').val();
    var quantity = $('#quantityInLine').val();
    var inLineDetailDto = {
        errorId, inLineId, timeFrameId, quantity,
        createAt: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString(),
        updateAt: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString()
    }
    console.log(inLineDetailDto);
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
        errorId, inLineId, timeFrameId, quantity, createAt, updateAt: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString()
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
    deleteInLineDetail(id).then(function (res) {
        renderTimeFrameCard();
        renderInLineDetailTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

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