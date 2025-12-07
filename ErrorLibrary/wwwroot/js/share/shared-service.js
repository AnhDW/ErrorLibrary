function renderSelectOptions(data, defaultLabel = 'Chọn mục') {
    let html = `<option value="" selected disabled>${defaultLabel}</option>`;
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    });
    return html;
}

function renderSelectOptionsByField(data, defaultLabel = 'Chọn mục', valueField = 'id', labelField = 'name', extraField = 'id') {
    let html = `<option value="" selected disabled>${defaultLabel}</option>`;

    data.forEach(item => {
        
        html += `<option value="${item[valueField]}" data-extra-field="${item[extraField]}">${item[labelField]}</option>`;
    });

    return html;
}


function renderSelectErrorOptions(error, defaultLabel = 'Chọn mục') {
    let html = `<option value="" selected disabled>${defaultLabel}</option>`;
    error.forEach(item => {
        html += `<option value="${item.id}">${item.code} - ${item.name}</option>`;
    });
    return html;
}

function renderFilterByField(data, header = 'header', idHeader = 'searchErrorGroup', valueField = 'id', labelField = 'name', classHeader = 'error-group', isSearch = false) {
    let html = `
            <div class="d-flex align-items-center">
                <span class="me-1">${header}</span>
                <button class="btn btn-sm btn-light p-0" data-bs-toggle="dropdown"><i class="fas fa-filter"></i></button>
                <ul class="dropdown-menu p-2" style="min-width:200px;">
                    <input type="text" class="form-control form-control-sm mb-2" placeholder="Tìm kiếm ${header}..." id="${idHeader}" ${isSearch ? '' : 'hidden'}/>
                    ${data.map((item, index) => `
                        <li>
                            <div class="form-check" style="font-size: 0.8rem;">
                                <input class="form-check-input ${classHeader}" value="${item[valueField]}" type="checkbox" id="eg-${index}">
                                <label class="form-check-label" for="eg-${index}">${item[labelField]}</label>
                            </div>
                        </li>
                    `).join('')}
                </ul>
            </div>
    `;

    return html;
}




