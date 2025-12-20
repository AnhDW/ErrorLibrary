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

function renderSelectDropdown(data, valueField = 'id', labelField = 'name') {
    return `
        <div class="dropdown select-dropdown w-100">
            <input type="text"
                   class="form-control bg-white dropdown-toggle select-input"
                   data-bs-toggle="dropdown"
                   placeholder="Chọn..."
                   readonly />

            <ul class="dropdown-menu p-2">
                <input type="text"
                       class="form-control mb-2 select-search"
                       placeholder="Tìm kiếm..." />

                ${data.map(item => `
                    <li class="dropdown-item"
                        data-value="${item[valueField]}" data-label="${item[labelField]}">
                        <div class="d-flex flex-column">
                            <div class="d-flex flex-row  align-items-center">
                                <small class="form-text text-muted m-0 text-info">${item.unitName}</small><i class="fas fa-caret-right"></i>
                                <small class="form-text text-muted m-0">${item.factoryName}</small><i class="fas fa-caret-right"></i>
                                <small class="form-text text-muted m-0">${item.enterpriseName}</small>
                            </div>
                            ${item[labelField]}
                        </div>
                    </li>
                `).join('')}
            </ul>
        </div>
    `;
}

function setSelectDropdownValue(container, value, triggerChange = true) {
    const dropdown = typeof container === 'string'
        ? document.querySelector(container)
        : container;

    if (!dropdown) return;

    const input = dropdown.querySelector('.select-input');
    const item = dropdown.querySelector(`.dropdown-item[data-value="${value}"]`);

    if (!input || !item) return;

    const oldValue = input.getAttribute('data-value');

    input.value = item.getAttribute('data-label');
    input.setAttribute('data-value', value);

    if (triggerChange && oldValue !== value) {
        $(input).trigger('change');
    }
}

function selectFirstItem(container, triggerChange = true) {
    const dropdown = typeof container === 'string'
        ? document.querySelector(container)
        : container;

    if (!dropdown) return;

    const firstItem = dropdown.querySelector('.dropdown-item');
    if (!firstItem) return;

    setSelectDropdownValue(dropdown, firstItem.getAttribute('data-value'), triggerChange);
}

document.addEventListener('click', function (e) {

    const item = e.target.closest('.dropdown-item');
    if (!item) return;

    const dropdown = item.closest('.select-dropdown');
    const input = dropdown.querySelector('.select-input');

    const oldValue = input.getAttribute('data-value');
    const newValue = item.getAttribute('data-value');

    input.value = item.getAttribute('data-label');
    input.setAttribute('data-value', newValue);

    if (oldValue !== newValue) {
        $(input).trigger('change');
    }

    bootstrap.Dropdown.getInstance(input)?.hide();
});



document.addEventListener('input', function (e) {
    if (!e.target.classList.contains('select-search')) return;

    const keyword = e.target.value.toLowerCase();
    const dropdown = e.target.closest('.dropdown-menu');

    dropdown.querySelectorAll('.dropdown-item').forEach(item => {
        item.style.display = item.innerText.toLowerCase().includes(keyword)
            ? ''
            : 'none';
    });
});



