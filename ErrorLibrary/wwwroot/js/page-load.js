
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
    }
});
