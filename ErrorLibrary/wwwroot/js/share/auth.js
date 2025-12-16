function handleLogin() {
    const userName = $("#username").val();
    const password = $("#password").val();

    console.log(userName);
    ajaxRequest({
        url: '/Auth/Login',
        method: 'POST',
        data: { userName, password },
        useToken: false,
        showLoading: true,
        onSuccess: (res) => {
            localStorage.setItem('user', JSON.stringify(res.result.user));
            localStorage.setItem('access_token', res.result.token);
            window.location.href = "/Home";
        }
    });
}

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

function handleLogout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('user');
    window.location.href = '/Login'; // hoặc URL login của bạn
}

function setUserInfo() {
    const user = JSON.parse(localStorage.getItem("user"));

    if (user) {
        $("#username").text(user.fullName);
        $("#userAvatar1").attr("src", user.avatarUrl ? user.avatarUrl : "~/assets/img/avatars/1.png")
        $("#userAvatar2").attr("src", user.avatarUrl ? user.avatarUrl : "~/assets/img/avatars/1.png")
        //$("#role").text(user.roles[0])
        $("#role").text("Admin")
    }
}

// Kiểm tra mỗi 1 phút
setInterval(autoLogoutIfExpired, 60 * 1000);

// Kiểm tra ngay khi load trang
autoLogoutIfExpired();

setUserInfo();
