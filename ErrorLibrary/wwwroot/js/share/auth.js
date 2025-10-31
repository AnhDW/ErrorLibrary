function isTokenExpired(token) {
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const exp = payload.exp * 1000; // exp là Unix timestamp (giây)
        return Date.now() > exp;
    } catch (e) {
        return true; // token không hợp lệ
    }
}

function autoLogoutIfExpired() {
    const token = localStorage.getItem('access_token');
    if (!token) return;

    if (isTokenExpired(token)) {
        localStorage.removeItem('access_token');
        localStorage.removeItem('user');
        alert('Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.');
        window.location.href = '/Login'; // hoặc URL login của bạn
    }
}

// Kiểm tra mỗi 1 phút
setInterval(autoLogoutIfExpired, 60 * 1000);

// Kiểm tra ngay khi load trang
autoLogoutIfExpired();
