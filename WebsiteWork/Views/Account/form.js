function validateForm() {
    // Bước 1: Lấy giá trị của username và password
    var user_email = document.getElementById('user_email').value;
    var user_pass = document.getElementById('user_pass').value;

    // Bước 2: Kiểm tra dữ liệu hợp lệ hay không
    if (user_email == '') {
        alert('Bạn chưa nhập tên đăng nhập');
    }
    else if (user_pass == '') {
        alert('Bạn chưa nhập mật khẩu');
    }
    else {
        alert('Dữ liệu hợp lệ, ta có thể chấp nhận submit form');
        return true;
    }
    return false;
}