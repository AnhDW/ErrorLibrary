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

            //checkAndInitInLine();

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
}
async function initialInLinePage() {
    initOrganizationTree();
    $('#date').val(new Date(Date.now() + 7 * 60 * 60 * 1000).toISOString().substring(0, 10));

    //await Promise.all([
    //    renderTimeFrameCard(),
    //    renderInLineDetailTable()
    //]);
    //var products = (await getProducts()).result;
    //var user = JSON.parse(localStorage.getItem('user'));
    //var html = renderSelectOptionsByField(products, 'Chọn sản phẩm', 'id', 'code', 'productCategoryId');

    //$('#selectProductCode').html(html);
    //$('#user').val(user.fullName);
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

//function renderInLineTable()