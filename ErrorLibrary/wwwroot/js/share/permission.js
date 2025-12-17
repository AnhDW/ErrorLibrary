function applyPermissions() {
    const permissions = window.AppPermissions || [];

    document.querySelectorAll('[data-permission]').forEach(el => {
        const required = el.dataset.permission;

        if (!permissions.includes(required)) {
            el.style.display = 'none';
            // hoặc el.remove();
        }
    });
}
document.addEventListener("DOMContentLoaded", applyPermissions);
