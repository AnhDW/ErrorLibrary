
$(document).ready(function () {
    const path = window.location.pathname;
    //menuItemActive(path.split('/')[1]);
    console.log(path);
    if (path.includes('/ErrorLibrary')) {
        renderErrorHeadFilter();
        renderErrorTable();
    } else if (path.includes('/ProductLibrary')) {
        renderProductTable();
    } else if (path.includes('/SolutionLibrary')) {
        renderSolutionTable();
    } else if (path.includes('/UserLibrary')) {
        renderUsersTable();
    } else if (path.includes('/EnterpriseLibrary')) {
        renderEnterprisesTable();
    } else if (path.includes('/FactoryLibrary')) {
        renderFactoryTable();
    } else if (path.includes('/UnitLibrary')) {
        renderUnitTable();
    } else if (path.includes('/LineLibrary')) {
        renderLinesTable();
    } else if (path.includes('/ErrorGroupLibrary')) {
        renderErrorGroupHeadFilter();
        renderErrorGroupTable();
    } else if (path.includes('/ErrorCategoryLibrary')) {
        renderErrorCategoryHeadFilter();
        renderErrorCategoryTable();
    } else if (path.includes('/ProductCategoryLibrary')) {
        renderErrorCategoryHeadFilter();
        renderProductCategoryTable();
    } else if (path.includes('/ErrorDetailLibrary')) {
        renderErrorDetailsTable();
    } else if (path.includes('/InLineDetailLibrary')) {
        initialInLineDetailPage();
    }else if (path.includes('/InLineLibrary')) {
        initialInLinePage();
    } else if (path.includes('/EndLineDetailLibrary')) {
        initialEndLineDetailPage();
    } else if (path.includes('/EndLineLibrary')) {
        initialEndLinePage();
    } else if (path.includes('/TimeFrameLibrary')) {
        renderTimeFrameTable();
    } else if (path.includes('/RoleLibrary')) {
        renderRoleTable();
    }
});


//function menuItemActive(controller) {
//    const currentLink = $(`.menu-link[href^="/${controller}"]`);
//    const menuSub = currentLink.closest('ul.menu-sub');
//    const parentMenuItem = menuSub.closest('li.menu-item');
//    const menuItem = currentLink.closest('li.menu-item');
//    parentMenuItem.addClass('open');
//    menuItem.addClass('active');
//    console.log(menuInner.html());
//}

function renderPagination(pagination, functionName, elementId) {
    let { currentPage, totalPages, pageSize } = pagination;
    let html = '';

    // << First Page
    html += `
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="${functionName}(1)">«</a>
        </li>
    `;

    // < Previous
    html += `
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="${functionName}(${currentPage - 1})">‹</a>
        </li>
    `;

    // Dãy trang động (max 9 số)
    let startPage = Math.max(1, currentPage - 4);
    let endPage = Math.min(totalPages, currentPage + 4);

    // Nếu đầu bị cắt → thêm ...
    if (startPage > 1) {
        html += `<li class="page-item disabled"><span class="page-link">...</span></li>`;
    }

    for (let i = startPage; i <= endPage; i++) {
        html += `
            <li class="page-item ${i === currentPage ? 'active' : ''}">
                <a class="page-link" href="#" onclick="${functionName}(${i})">${i}</a>
            </li>
        `;
    }

    // Nếu cuối bị cắt → thêm ...
    if (endPage < totalPages) {
        html += `<li class="page-item disabled"><span class="page-link">...</span></li>`;
    }

    // > Next
    html += `
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="${functionName}(${currentPage + 1})">›</a>
        </li>
    `;

    // >> Last Page
    html += `
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" onclick="${functionName}(${totalPages})">»</a>
        </li>
    `;

    $(`#${elementId}`).html(html);
}


