$(document).ready(function () {
    const path = window.location.pathname;

    if (path.includes('/page-a')) {
        loadDataForPageA();
    } else if (path.includes('/page-b')) {
        loadDataForPageB();
    }
});
