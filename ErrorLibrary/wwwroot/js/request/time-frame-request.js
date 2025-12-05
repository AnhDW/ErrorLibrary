function getTimeFrames() {
    return ajaxRequest({
        url: '/TimeFrameLibrary/GetTimeFrames',
        method: 'GET',
    })
}

function getTimeFrameById(id) {
    return ajaxRequest({
        url: '/TimeFrameLibrary/GetTimeFrameById',
        method: 'GET',
        data: { id: id }
    })
}

function generateTimeFrameTitle(startTime, endTime) {
    return ajaxRequest({
        url: '/TimeFrameLibrary/GenerateTimeFrameTitle',
        method: 'GET',
        data: {
            startTime: startTime,
            endTime: endTime
        }
    })
}

function addTimeFrame(timeFrameDto) {
    return ajaxRequest({
        url: '/TimeFrameLibrary/AddTimeFrame',
        method: 'POST',
        data: timeFrameDto,
        showLoading: true
    })
}

function updateTimeFrame(timeFrameDto) {
    return ajaxRequest({
        url: '/TimeFrameLibrary/UpdateTimeFrame',
        method: 'POST',
        data: timeFrameDto,
        showLoading: true
    })
}

function deleteTimeFrame(id) {
    return ajaxRequest({
        url: '/TimeFrameLibrary/DeleteTimeFrame',
        method: 'POST',
        data: id,
        showLoading: true
    })
}