
function filterTree(nodes, ids) {
    let result = [];

    for (const node of nodes) {
        const hasMatch = ids.includes(node.id);

        let children = [];
        if (node.nodes && node.nodes.length > 0) {
            children = filterTree(node.nodes, ids);
        }

        // Nếu node khớp ID hoặc có con khớp
        if (hasMatch || children.length > 0) {
            result.push({
                ...node,
                nodes: children.length > 0 ? children : null
            });
        }
    }

    return result;
}