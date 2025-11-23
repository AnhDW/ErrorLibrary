function resToastr(res) {
    if (res.isSuccess) {
        toastr.success(res.message);
    } else {
        toastr.warning(res.message);
    }
}