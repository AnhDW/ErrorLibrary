const ajaxRequest = ({
    url,
    method = 'GET',
    data = {},
    isFormData = false,
    useToken = false,
    useAntiForgery = false,
    showLoading = false,
    onSuccess = () => { },
    onError = () => { },
    onComplete = () => { }
}) => {
    let headers = {};
    let contentType = 'application/json';
    let processData = true;
    let isGet = method.toUpperCase() === 'GET';
    // Token từ localStorage
    if (useToken) {
        const token = localStorage.getItem('access_token');
        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
    }

    // Anti-Forgery Token từ form
    if (useAntiForgery) {
        const antiToken = $('input[name="__RequestVerificationToken"]').val();
        if (antiToken) {
            headers['RequestVerificationToken'] = antiToken;
        }
    }

    // Nếu là FormData
    if (isFormData) {
        contentType = false;
        processData = false;
    }

    // Loading indicator
    if (showLoading) {
        $('#loadingSpinner').show(); // hoặc bất kỳ UI nào bạn dùng
    }

    return $.ajax({
        url: url,
        type: method,
        data: isGet ? data : (isFormData ? data : JSON.stringify(data)),
        contentType: isGet ? 'application/x-www-form-urlencoded; charset=UTF-8' : contentType,
        processData: isGet ? true : processData,
        headers: headers,
        success: function (response) {
            onSuccess(response);
        },
        error: function (xhr) {
            console.error('AJAX Error:', xhr.responseText);
            onError(xhr);
        },
        complete: function () {
            if (showLoading) {
                $('#loadingSpinner').hide();
            }
            onComplete();
        }
    });
};

