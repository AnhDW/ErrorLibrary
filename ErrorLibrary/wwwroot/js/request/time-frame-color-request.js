function getTimeFrameColors() {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/GetTimeFrameColors',
        method: 'GET',
    })
}

function getTimeFrameColorById(id) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/GetTimeFrameColorById',
        method: 'GET',
        data: { id: id }
    })
}

function addTimeFrameColor(timeFrameColorDetailDto) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/AddTimeFrameColor',
        method: 'POST',
        data: timeFrameColorDto,
        showLoading: true
    })
}

function updateTimeFrameColor(timeFrameColorDto) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/UpdateTimeFrameColor',
        method: 'POST',
        data: timeFrameColorDto,
        showLoading: true
    })
}

function deleteTimeFrameColor(id) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/DeleteTimeFrameColor',
        method: 'POST',
        data: id,
        showLoading: true
    })
}