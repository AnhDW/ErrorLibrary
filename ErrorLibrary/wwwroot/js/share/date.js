function getVietnamDateString(date = new Date()) {
    const vnOffset = 7 * 60; // phút
    const localOffset = date.getTimezoneOffset(); // phút
    const diff = (vnOffset + localOffset) * 60 * 1000;

    return new Date(date.getTime() + diff)
        .toISOString()
        .substring(0, 10);
}

function addDays(date, days) {
    const d = new Date(date);
    d.setDate(d.getDate() + days);
    return d.toISOString().substring(0, 10);
}

