function renderSelectOptions(data, defaultLabel = 'Chọn mục') {
    let html = `<option value="" selected disabled>${defaultLabel}</option>`;
    data.forEach(item => {
        html += `<option value="${item.id}">${item.name}</option>`;
    });
    return html;
}
