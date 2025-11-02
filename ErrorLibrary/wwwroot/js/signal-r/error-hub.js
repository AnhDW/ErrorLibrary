const connection = new signalR.HubConnectionBuilder()
    .withUrl("/errorHub")
    .build();

//// Khi có sản phẩm mới
//connection.on("errorAdded", (error) => {
//    const list = document.getElementById("errorList");
//    const li = document.createElement("li");
//    li.id = "error-" + error.id;
//    li.textContent = error.name + " - " + error.price;
//    list.appendChild(li);
//});

// Khi sản phẩm được cập nhật
connection.on("errorUpdated", (error) => {
    //const li = document.getElementById("error-" + error.id);
    //if (li) {
    //    li.textContent = error.name + " - " + error.price;
    //}
    console.log(error);
});

//// Khi sản phẩm bị xóa
//connection.on("errorDeleted", (id) => {
//    const li = document.getElementById("error-" + id);
//    if (li) {
//        li.remove();
//    }
//});

connection.start().catch(err => console.error(err));
