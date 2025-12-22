let endLine = {
    id: 0,
    lineId: 0, productId: 0,
    acceptedQuantity: 0,
    checkQuantity: 0,
    date: new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(0, 10),
    isActive: true,
    isFinalized: false
};
let firstLoadEndLine = true;
let currentUserId = JSON.parse(localStorage.getItem('user')).id;
let errorQuantity = 0;
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

async function initialEndLineDetailPage() {
    var products = (await getProducts()).result;
    var html = renderSelectOptionsByField(products, 'Chọn sản phẩm', 'id', 'code', 'productCategoryId');
    $('#selectProductCode').html(html);
    await renderLineDropdown();
    selectFirstItem('#lineDropdown');
    checkParams();
    $('#date').val(endLine.date);
}

async function renderLineDropdown() {
    var organizations = (await getOrganizationsDisplay()).result;
    var userId = JSON.parse(localStorage.getItem('user')).id;
    var organizationIds = (await getOrganizationsByUserId(userId)).result;
    var lineIds = organizationIds.filter(x => x.organizationType == 'line').map(x => x.organizationId);
    organizations = organizations.filter(x => lineIds.includes(x.id));
    $('#lineDropdown').html(renderSelectDropdown(organizations, 'id', 'lineName'));
}
//async function initOrganizationTree() {
//    $('.dropdown-menu').on('click', function (e) {
//        e.stopPropagation();
//    });
//    var userId = JSON.parse(localStorage.getItem('user')).id;
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
//            endLine.lineId = $('#selectedOrganizationNode').val().replace("line_", "");
//            checkAndInitEndLine();

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
//    if (endLine.lineId !== 0) {
//        var lineNode = 'line_' + endLine.lineId;
//        leaf = allNodes.find(n => n.id && n.id.includes(lineNode));
//    } else {
//        leaf = allNodes.find(n => n.id && n.id.startsWith("line_"));
//    }
//    if (leaf) {
//        $('#treeOrganization').treeview('selectNode', [leaf.nodeId]);
//    }
//}

async function renderErrorGroupCard() {
    var productId = $('#selectProductCode').val();
    var errorGroups = (await getErrorGroupsByProduct(productId)).result;
    var html = '';
    errorGroups.forEach(item => {
        html += `
        <div class="col-md-6 col-xl-3">
            <div class="card card-custom bg-secondary text-white mb-3" onclick="initAddModal(${item.id})">
                <div class="card-header text-truncate">
                    ${item.code}. ${item.name}
                </div>
            </div>
        </div>`;
    });
    $('#errorGroupCard').html(html);

    $('#errorGroupCard .card').each(function (i) {
        let card = $(this);
        setTimeout(() => {
            card.addClass('show');
        }, i * 50); // delay mỗi card 50ms
    });
}

async function renderEndLineDetailTable() {
    if (endLine.id === 0) return;
    var endLineDetails = (await getEndLineDetailsByEndLine(endLine.id)).result;
    let html = '';
    errorQuantity = endLineDetails.length;
    renderTxtQuantity();
    console.log(endLineDetails);
    endLineDetails.forEach((item, index) => {
        html += `<tr>
            <td>${index + 1}</td>
            <td>${item.error.errorGroup.name}</td>
            <td>${item.error.code}</td>
            <td>${item.error.name}</td>
            <td>${item.createdAt.substring(0, 10)} ${item.createdAt.substring(11, 16)}</td>
            <td>${item.user.fullName}</td>
            <td>
                <div class="d-flex gap-2">
                    <button type="button" class="btn btn-danger btn-sm" onclick="handleDeleteEndLineDetail(${item.id})">
                        <i class="bx bx-trash"></i>
                    </button>
                </div>
            </td>
        </tr>`;
    });
    $('#endLineDetailTableBody').html(html);
}

function renderTxtQuantity() {
    var totalCheckQuantity = endLine.acceptedQuantity + errorQuantity;
    $('#txtTotalCheckQuantity').html(`${totalCheckQuantity}/${endLine.checkQuantity}`)
    $('#txtAcceptedQuantity').html(`<span class="text-success">${endLine.acceptedQuantity}</span>/${endLine.checkQuantity}`)
    $('#txtErrorQuantity').html(`<span class="text-danger">${errorQuantity}</span>/${endLine.checkQuantity}`)
}

async function initAddModal(errorGroupId) {
    var modal = new bootstrap.Modal(document.getElementById('addModel'));
    modal.show();
    var productId = $('#selectProductCode').val();
    var product = (await getProductById(productId)).result;
    var errors = (await getErrorsByErrorGroupAndProductCategory(errorGroupId, product.productCategoryId)).result;
    console.log(errors);
    var html = '';
    errors.forEach(item => {
        html += `
        <div class="col-6">
            <div class="card card-custom bg-secondary text-white mb-3" onclick="handleAddEndLineDetail(${item.id})">
                <div class="card-header text-truncate">
                    ${item.code}. ${item.name}
                </div>
            </div>
        </div>`;
    });
    $('#errorCard').html(html);
    $('#errorCard .card').each(function (i) {
        let card = $(this);
        setTimeout(() => {
            card.addClass('show');
        }, i * 50); // delay mỗi card 50ms
    });
}

function handleAddEndLineDetail(errorId) {
    // cần endLineId, errorId, userId, createdAt
    var endLineId = endLine.id;
    var userId = currentUserId;
    var createdAt = new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString();
    if (errorQuantity >= endLine.checkQuantity) {
        $('#addModel').modal('hide');
        toastr.warning('Lỗi ko thể vượt quá số lượng kiểm');
        $('#errorQuantityWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
    if (errorQuantity + endLine.acceptedQuantity >= endLine.checkQuantity) {
        endLine.acceptedQuantity = endLine.checkQuantity - (errorQuantity + 1);
        checkAndInitEndLine();
    }

    var endLineDetailDto = {
        endLineId, errorId, userId, createdAt
    }

    addEndLineDetail(endLineDetailDto).then(function (res) {
        renderEndLineDetailTable();
        $('#addModel').modal('hide');
        //resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function handleDeleteEndLineDetail(id) {
    deleteEndLineDetail(id).then(function (res) {
        $('#errorQuantityWrapper').removeClass('shake border border-2 border-danger p-2 rounded');
        renderEndLineDetailTable();
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}

function checkAndInitEndLine() {
    //tạo 1 in line nếu chưa có
    var lineId = $('#lineDropdown .select-input').attr('data-value');
    var productId = $('#selectProductCode').val();
    var orderQuantity = $('#orderQuantity').val();
    var checkQuantity = $('#checkQuantity').val();
    var date = $('#date').val();

    var isActive = endLine.isActive;
    var isFinalized = endLine.isFinalized;
    var acceptedQuantity = endLine.acceptedQuantity;
    if (!lineId || !productId || !date) {
        return;
    }

    if (lineId != endLine.lineId || productId != endLine.productId || date != endLine.date) {
        firstLoadEndLine = true;
    }

    var endLineDto = {
        lineId, productId, date, orderQuantity, checkQuantity, acceptedQuantity, firstLoad: firstLoadEndLine, isActive, isFinalized
    }

    $('#formWrapper').removeClass('shake border border-2 border-danger p-2 rounded');
    $('#errorQuantityWrapper').removeClass('shake border border-2 border-danger p-2 rounded');
    checkInitAndUpdateEndLine(endLineDto).then(function (res) {
        endLine = res.result;
        console.log(res.result);
        if (firstLoadEndLine) {
            firstLoadEndLine = false;
            $('#orderQuantity').val(endLine.orderQuantity);
            $('#checkQuantity').val(endLine.checkQuantity);
            renderErrorGroupCard();
        }
        setBtnStatus();
        renderEndLineDetailTable();
        //resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });

}

async function checkParams() {
    const params = new URLSearchParams(window.location.search);
    const endLineId = params.get("endLineId");
    if (endLineId === null) return;

    var endLineById = (await getEndLineById(endLineId)).result;
    console.log(endLineById);
    endLine = endLineById;
    console.log(endLine);

    $('#selectProductCode').val(endLine.productId);
    //$('#quantity').val(endLineById.quantity);
    setSelectDropdownValue('#lineDropdown', endLineById.lineId);
}
function setBtnStatus() {
    if (endLine.isFinalized) {
        $('#btnIsFinalized').removeClass('btn-success').addClass('btn-secondary');
    } else {
        $('#btnIsFinalized').removeClass('btn-secondary').addClass('btn-success');
    }
    if (endLine.isActive) {
        $('#btnIsActive').removeClass('btn-secondary').addClass('btn-danger');
        $('#btnIsFinalized').removeClass('d-none');
    } else {
        $('#btnIsActive').removeClass('btn-danger').addClass('btn-secondary');
        $('#btnIsFinalized').addClass('d-none');
    }
}

$('#selectProductCode, #orderQuantity, #checkQuantity, #date').on('change keyup', function () {
    checkAndInitEndLine();
});

$('#btnIsFinalized').on('click', function () {
    if (endLine.id === 0) {
        toastr.warning("Bạn chưa chọn EndLine");
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
    if (endLine.isFinalized === false) {
        endLine.isFinalized = true;
    } else {
        endLine.isFinalized = false;
    }
    checkAndInitEndLine();
});

$('#btnIsActive').on('click', function () {
    if (endLine.id === 0) {
        toastr.warning("Bạn chưa chọn EndLine");
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
    if (endLine.isActive === false) {
        endLine.isActive = true;
    } else {
        endLine.isActive = false;
    }
    checkAndInitEndLine();
});

$('#btnAcceptedQuantity').on('click', function () {
    if (endLine.id === 0) {
        toastr.warning("Bạn chưa chọn EndLine");
        $('#formWrapper').addClass('shake border border-2 border-danger p-2 rounded');
        return;
    }
    endLine.acceptedQuantity++;
    checkAndInitEndLine();
});

$('#lineDropdown').on('change', '.select-input', function () {
    checkAndInitEndLine();
});