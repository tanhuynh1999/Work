function valicv() {
    // Bước 1: Lấy giá trị của username và password
    var cv_fullname = document.getElementById('cv_fullname').value;
    var cv_linkfc = document.getElementById('cv_linkfc').value;
    var cv_phone = document.getElementById('cv_phone').value;
    var cv_email = document.getElementById('cv_email').value;
    //Bieu thuc chinh qui
    var linkfc = /https?:\/\/(?:[-\w]+\.)?([-\w]+)\.\w+(?:\.\w+)?\/?.*/i;
    var phone = /^(0|\+84)(\s|\.)?((3[2-9])|(5[689])|(7[06-9])|(8[1-689])|(9[0-46-9]))(\d)(\s|\.)?(\d{3})(\s|\.)?(\d{3})$/i;
    var email = /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/i;;
    // Bước 2: Kiểm tra dữ liệu hợp lệ hay không
    if (cv_fullname == '') {
        document.getElementById("testfullname").innerHTML = '<b class="text-danger" style="font-size: 12px;"><i class="fa fa-window-close">&nbsp;</i>Họ và tên không để trống.</b>';
    }
    else if (linkfc.test(cv_linkfc) == false) {
        if (cv_linkfc != '') {
            document.getElementById("testlinkfc").innerHTML = '<b class="text-danger" style="font-size: 12px;"><i class="fas fa-exclamation-circle">&nbsp</i>Link facebook của bạn không hợp lệ.Có thể bỏ trống</b>';
        }
    }
    else if (phone.test(cv_phone) == false) {
        if (cv_phone != '') {
            document.getElementById("testphone").innerHTML = '<b class="text-danger" style="font-size: 12px;"><i class="fas fa-exclamation-circle">&nbsp;</i>Số điện thoại bạn nhập không hợp lệ.</b>';
        }
        else {
            document.getElementById("testphone").innerHTML = '<b class="text-danger" style="font-size: 12px;"><i class="fa fa-window-close">&nbsp;</i>Số điện thoại là bắt buộc. NTD dựa vào số điện thoại của bạn để liên hệ, nên khai báo đúng.</b>';
        }
    }
    else if (email.test(cv_email) == false) {
        if (cv_email != '') {
            document.getElementById("testphone").innerHTML = '<b class="text-danger" style="font-size: 12px;"><i class="fas fa-exclamation-circle">&nbsp;</i>Số điện thoại bạn nhập không hợp lệ.</b>';
        }
        else {
            document.getElementById("testphone").innerHTML = '<b class="text-danger" style="font-size: 12px;"><i class="fa fa-window-close">&nbsp;</i>Email là bắt buộc. NTD dựa vào Email của bạn để liên hệ, nên khai báo đúng (Tốt nhất là email có họ và tên).</b>';
        }
    }
    else {
        return true;
    }
    return false;
}