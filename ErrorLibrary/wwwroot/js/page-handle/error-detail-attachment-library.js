const dynamicEl = [];
let inlineGallery;
function renderErrorDetailAttachment(lineId, productId, errorId, userId) {
    getByErrorDetail(lineId, productId, errorId, userId).then(function (data) {
        let html = `<div class="p-1 upload-wrapper">
                <label for= "editErrorDetailAttachment" class= "upload-box" >
                    <i class="bx bx-plus"></i>
                </label >
                <input hidden type="text" id="editLineIdAttachment" value="${lineId}"/>
                <input hidden type="text" id="editProductIdAttachment" value="${productId}"/>
                <input hidden type="text" id="editErrorIdAttachment" value="${errorId}"/>
                <input hidden type="text" id="editUserIdAttachment" value="${userId}"/>
                <input hidden type="file" id="editErrorDetailAttachment" multiple accept="image/*">
            </div>`;
        let i = 0;
        data.result.forEach(item => {
            html += `
                <div class="p-1 image-container">
                    <img src="${item.url}" alt="${item.fileName}" class="w-px-100 h-auto" data-index="${i}"/>
                    <div class="overlay">
                        <div class="overlay-left" data-action="view" onclick="overlayLeftHandle(${i})"><i class="bx bx-bullseye"></i></div>
                        <div class="overlay-right" data-action="delete" onclick="overlayRightHandle(${item.id})"><i class="bx bx-trash"></i></div>
                    </div>
                </div>
                `;
            dynamicEl.push({
                src: `${item.url}`,
                thumb: `${item.url}`,
                subHtml: `<div class="lightGallery-captions d-flex  justify-content-center align-items-center">
                    <div class="d-flex flex-column flex-wrap p-2 rounded" style="background-color: rgba(0,0,0,0.6);">
                        <h4 class="text-white">Photo name <a href="${item.url}">${item.fileName}</a></h4>
                        <p>Published on ${item.createdAt}</p>
                    </div>
                </div>`,
            })
            i++;
        });
        $('#errorDetailAttachments').html(html);
        console.log(dynamicEl)
    });
}

//xử lý thêm ngay khi chọn ảnh
$(document).on('change', '#editErrorDetailAttachment', async function (e) {
        const lineId = $('#editLineIdAttachment').val();
        const productId = $('#editProductIdAttachment').val();
        const errorId = $('#editErrorIdAttachment').val();
        const userId = $('#editUserIdAttachment').val();
        const errorDetailAttachmentDto = {
            lineId, productId, errorId, userId
        }
        addErrorDetailAttachment(errorDetailAttachmentDto).then(function (res) {
            renderErrorDetailAttachment(lineId, productId, errorId, userId);
            resToastr(res);
        }).catch(function (err) {
            toastr.error(err);
        });
    console.log(errorDetailAttachmentDto);
    });

function overlayLeftHandle(index) {

    $('#imagePreviewModal').modal('show');

    const lgContainer = document.getElementById('inline-gallery-container');
    if (inlineGallery) {
        inlineGallery.destroy(true);
    }
    inlineGallery = lightGallery(lgContainer, {
        container: lgContainer,
        dynamic: true,
        hash: false,
        closable: true,
        showMaximizeIcon: true,
        appendSubHtmlTo: '.lg-item',
        slideDelay: 400,
        plugins: [lgZoom, lgThumbnail, lgAutoplay, lgFullscreen, lgRotate, lgShare, lgHash],
        dynamicEl: dynamicEl
    });

    inlineGallery.openGallery(index);
}

async function overlayRightHandle(id) {
    console.log(id);
    const errorDetailAttachment = await getErrorDetailAttachmentById(id);
    const result = errorDetailAttachment.result;
    console.log(errorDetailAttachment);
    deleteErrorDetailAttachment(id).then(function (res) {
        renderErrorDetailAttachment(result.lineId, result.productId, result.errorId, result.userId);
        resToastr(res);
    }).catch(function (err) {
        toastr.error(err);
    });
}




