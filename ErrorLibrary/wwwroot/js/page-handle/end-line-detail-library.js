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
    initOrganizationTree();
}
async function initOrganizationTree() {
    $('.dropdown-menu').on('click', function (e) {
        e.stopPropagation();
    });
    var userId = JSON.parse(localStorage.getItem('user')).id;
    var selectedIds = (await getOrganizationsByUserId(userId)).result;
    var organizationIds = selectedIds.map(x => { return x.organizationType + "_" + x.organizationId })

    const tree = (await getOrganizationTreeDropdown()).result;
    var filteredTree = filterTree(tree, organizationIds);

    $('#treeOrganization').treeview({
        data: filteredTree,
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
            //checkAndInitEndLine();

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