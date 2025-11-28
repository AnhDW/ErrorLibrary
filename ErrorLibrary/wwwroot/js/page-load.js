
$(document).ready(function () {
    const path = window.location.pathname;
    //menuItemActive(path.split('/')[1]);
    console.log(path);
    if (path.includes('/ErrorLibrary')) {
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
        renderErrorGroupTable();
    } else if (path.includes('/ErrorCategoryLibrary')) {
        renderErrorCategoryTable();
    } else if (path.includes('/ProductCategoryLibrary')) {
        renderProductCategoryTable();
    } else if (path.includes('/ErrorDetailLibrary')) {
        renderErrorDetailsTable();
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


