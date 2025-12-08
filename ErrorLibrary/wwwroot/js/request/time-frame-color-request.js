function getTimeFrameColors() {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/GetTimeFrameColors',
        method: 'GET',
    })
}

function getByTimeFrame(timeFrameId) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/GetByTimeFrame',
        method: 'GET',
        data: { timeFrameId: timeFrameId }
    })
}

function getTimeFrameColorByQuantity(timeFrameId, quantity) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/GetTimeFrameColorByQuantity',
        method: 'GET',
        data: {
            timeFrameId: timeFrameId,
            quantity: quantity
        }
    })
}

function getTimeFrameColorById(id) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/GetTimeFrameColorById',
        method: 'GET',
        data: { id: id }
    })
}

function copyAndPasteColor(copyAndPasteColorDto) {
    return ajaxRequest({
        url: '/TimeFrameColorLibrary/CopyAndPasteColor',
        method: 'POST',
        data: copyAndPasteColorDto,
        showLoading: true
    })
}

function addTimeFrameColor(timeFrameColorDto) {
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