var treeData = [
    {
        text: "Parent 1",
        nodes: [
            {
                text: "Child 1",
                nodes: [
                    {
                        text: "Grandchild 1"
                    },
                    {
                        text: "Grandchild 2"
                    }
                ]
            },
            {
                text: "Child 2"
            }
        ]
    },
    {
        text: "Parent 2"
    },
    {
        text: "Parent 3"
    },
    {
        text: "Parent 4"
    },
    {
        text: "Parent 5"
    }
];

$('#tree').treeview({
    data: treeData,
    onNodeSelected: function (event, node) {
        // Gán giá trị node vào hidden input
        $('#selectedNode').val(node.id);
        // Hiển thị tên node trên nút dropdown
        $('#treeDropdown').text(node.text);
        // Đóng dropdown
        $('.dropdown-menu').removeClass('show');
    }
});