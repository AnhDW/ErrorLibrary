const connection = new signalR.HubConnectionBuilder()
    .withUrl("/errorHub")
    .build();

// Khi có sản phẩm mới
//connection.on("errorAdded", (error) => {
//    //const tbody = document.getElementById("errorTableBody");
//    //const html = `
//    //                <tr id="row_${error.id}">
//    //                    <td>${error.errorGroup == null ? '' : error.errorGroup.name}</td>
//    //                    <td>${error.code}</td>
//    //                    <td>${error.name}</td>
//    //                    <td>${error.errorCategory ?? ''}</td>
//    //                    <td>
//    //                        <div class="dropdown">
//    //                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
//    //                                <i class="bx bx-dots-vertical-rounded"></i>
//    //                            </button>
//    //                            <div class="dropdown-menu">
//    //                                <button type="button" class="dropdown-item" data-bs-toggle="modal"
//    //                                            data-bs-target="#editModel" onclick="editShowErrorModalHandle(${error.id})">
//    //                                        <i class="bx bx-edit-alt me-1"></i> Sửa
//    //                                    </button>
//    //                                    <a class="dropdown-item" href="javascript:void(0);" onclick="handleDeleteError(${error.id})"><i class="bx bx-trash me-1"></i> Xóa</a>

//    //                            </div>
//    //                        </div>
//    //                    </td>
//    //                </tr>
//    //                `;
//    //tbody.insertAdjacentHTML("beforeend", html);
//});

//// Khi sản phẩm được cập nhật
//connection.on("errorUpdated", (error) => {
//    //const row = document.getElementById(`row_${error.id}`);

//    console.log(error);
//});

//// Khi sản phẩm bị xóa
//connection.on("errorDeleted", (id) => {
//    const li = document.getElementById("error-" + id);
//    if (li) {
//        li.remove();
//    }
//});

connection.start().catch(err => console.error(err));
