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

            console.log(node.id);

            let dd = bootstrap.Dropdown.getOrCreateInstance(
                document.getElementById('btnTreeOrganization')
            );
            dd.hide();
        },
        onNodeExpanded: function (event, node) {
            console.log("Đã mở:", node.text);
        },
        onNodeCollapsed: function (event, node) {
            console.log("Đã đóng:", node.text);
        }
    });
}

async function initDropdown() {
    initOrganizationTree();

    var products = (await getProducts()).result;

    var html = renderSelectOptionsByField(products, 'Chọn sản phẩm', 'id', 'code');

    $('#selectProductCode').html(html);
}

document.getElementById("toggleFormBtn").addEventListener("click", function () {
    const wrapper = document.getElementById("formWrapper");
    const icon = document.getElementById("toggleIcon");

    wrapper.classList.toggle("d-none");

    if (wrapper.classList.contains("d-none")) {
        this.innerHTML = '<i class="fas fa-chevron-down"></i>';
    } else {
        this.innerHTML = '<i class="fas fa-chevron-up"></i>';
    }
});