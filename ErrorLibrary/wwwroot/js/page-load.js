
$(document).ready(function () {
    const path = window.location.pathname;
    console.log(path);
    if (path.includes('/ErrorLibrary')) {
        renderErrorTable();
    } else if (path.includes('/ProductLibrary')) {
        renderProductTable();
    } else if (path.includes('/SolutionLibrary')) {
        renderSolutionTable();
    } else if (path.includes('/UserLibrary')) {
        renderUsersTable();
    } else if (path.includes('/EnterpriseLibrary')){
        renderEnterprisesTable();
    } else if (path.includes('/FactoryLibrary')){
        renderFactoryTable();
    } else if (path.includes('/UnitLibrary')) {
        renderUnitTable();
    }
});
