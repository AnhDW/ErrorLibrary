const connection = new signalR.HubConnectionBuilder()
    .withUrl("/errorHub")
    .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
    .build();

function errorRowHtml(error) {
    return `<tr id="row_${error.Id}">
                        <td>${error.ErrorGroup == null ? '' : error.ErrorGroup.Name}</td>
                        <td>${error.Code}</td>
                        <td>${error.Name}</td>
                        <td>${error.ErrorCategory ?? ''}</td>
                        <td>
                            <div class="dropdown">
                                <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                                    <i class="bx bx-dots-vertical-rounded"></i>
                                </button>
                                <div class="dropdown-menu">
                                    <button type="button" class="dropdown-item" data-bs-toggle="modal"
                                                data-bs-target="#editModel" onclick="editShowErrorModalHandle(${error.Id})">
                                            <i class="bx bx-edit-alt me-1"></i> Sửa
                                        </button>
                                        <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteError(${error.Id})"><i class="bx bx-trash me-1"></i> Xóa</a>

                                </div>
                            </div>
                        </td>
                    </tr>
                    `;
}
//Khi có sản phẩm mới
connection.on("errorAdded", (error) => {
    const tbody = document.getElementById("errorTableBody");
    tbody.insertAdjacentHTML("afterbegin", errorRowHtml(error));
});

// Khi sản phẩm được cập nhật
connection.on("errorUpdated", (error) => {
    const row = document.getElementById(`row_${error.Id}`);
    if (row) {
        row.outerHTML = errorRowHtml(error); // thay thế toàn bộ dòng
    }
});

// Khi sản phẩm bị xóa
connection.on("errorDeleted", (id) => {
    const row = document.getElementById(`row_${id}`);
    if (row) {
        row.remove();
    }
});

connection.on("Notification", (message) => {
    toastr.success(message);
});
connection.start().catch(err => console.error(err));
