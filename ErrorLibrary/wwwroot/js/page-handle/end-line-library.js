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

//            console.log($('#selectedOrganizationNode').val());
//            renderEndLineTable();

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
//    let firstLeaf = allNodes.find(n => n.id && n.id.startsWith("line_"));
//    if (firstLeaf) {
//        $('#treeOrganization').treeview('selectNode', [firstLeaf.nodeId]);
//    }
//}

async function renderLineDropdown() {
    var organizations = (await getOrganizationsDisplay()).result;
    var userId = JSON.parse(localStorage.getItem('user')).id;
    var organizationIds = (await getOrganizationsByUserId(userId)).result;
    var lineIds = organizationIds.filter(x => x.organizationType == 'line').map(x => x.organizationId);
    organizations = organizations.filter(x => lineIds.includes(x.id));
    $('#lineDropdown').html(renderSelectDropdown(organizations, 'id', 'lineName'));
}

async function initialEndLinePage() {
    $('#date').val(new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(0, 10));
    await renderLineDropdown();
    await selectFirstItem('#lineDropdown');
}

async function renderEndLineTable() {
    var lineId = $('#lineDropdown .select-input').attr('data-value');
    var date = $('#date').val();
    var endLines = (await getEndLines()).result;
    console.log(date)
    console.log(endLines)
    endLines = endLines.filter(x => x.date === date && x.lineId == lineId);
    var html = '';
    endLines.forEach((item) => {
        html += `
        <tr>
                <td>${item.product.code}</td>
                <td>${item.orderQuantity}</td>
                <td>${item.checkQuantity}</td>
                <td>${item.acceptedQuantity}</td>
                <td>${item.totalErrors}</td>
                <td><small class="${!item.isActive ? "text-danger" : item.isFinalized ? "text-success" : "text-info"}">
                        <i class="fas fa-dot-circle"></i> ${!item.isActive ? "Hủy kiểm" : item.isFinalized ? "Hoàn thành" : "Đang kiểm"} 
                    </small>
                </td>
                <td><a href="/EndLineDetailLibrary?endLineId=${item.id}"><i class="fas fa-info-circle"></i></a></td>
            </tr>
        `
    });

    $('#endLineTableBody').html(html);
}

$('#date').on('change', function () {
    renderEndLineTable();
});


$('#lineDropdown').on('change', '.select-input', function () {
    renderEndLineTable();
});