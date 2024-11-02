$(document).ready(function () {
    $('.product-slider').slick({
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 3, // Hiển thị 3 sản phẩm cùng lúc
        slidesToScroll: 3,
        autoplay: true, // Bật chế độ tự động chạy
        autoplaySpeed: 2000, // Thời gian giữa các lần chuyển đổi (3 giây)
        responsive: [
            {
                breakpoint: 768, // Khi màn hình nhỏ hơn 768px
                settings: {
                    slidesToShow: 2 // Hiển thị 2 sản phẩm
                }
            },
            {
                breakpoint: 480, // Khi màn hình nhỏ hơn 480px
                settings: {
                    slidesToShow: 1 // Hiển thị 1 sản phẩm
                }
            }
        ]
    });
});

// Hiện thanh tìm kiếm
$('#searchToggle').click(function () {
    $('#searchBar').toggle();
});

// Cập nhật số sản phẩm trong giỏ hàng
let cartCount = 0; // Thay đổi theo logic giỏ hàng
$('#cartCount').text(cartCount);

// Sticky header
$(window).scroll(function () {
    if ($(this).scrollTop() > 50) {
        $('#header').addClass('sticky');
    } else {
        $('#header').removeClass('sticky');
    }
});

$(document).ready(function () {
    $('.carousel').carousel({
        interval: 2000 // Thay đổi thời gian chuyển đổi giữa các ảnh
    });
});
$(document).ready(function () {
    $('.parent-category').click(function () {
        $(this).next('.nested').slideToggle(); // Hiện/ẩn danh mục con
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const products = document.querySelectorAll('.product-item');

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
            }
        });
    });

    products.forEach(product => {
        observer.observe(product);
    });
});

function toggleSubmenu(element) {
    const submenu = element.querySelector('.submenu');
    const symbol = element.querySelector('.toggle-symbol');
    if (submenu.style.display === 'block') {
        submenu.style.display = 'none';
        symbol.textContent = '+'; // Dấu cộng
    } else {
        submenu.style.display = 'block';
        symbol.textContent = '−'; // Dấu trừ
    }
}
function changeQuantity(productId, delta) {
    const input = document.getElementById(productId);
    let currentValue = parseInt(input.value);
    currentValue += delta;
    if (currentValue < 1) currentValue = 1; // Đảm bảo số lượng không nhỏ hơn 1
    input.value = currentValue;
}


$(document).ready(function () {
    $('#eye').click(function () {
        $(this).toggleClass('open');
        $(this).children('i').toggleClass('fa-eye-slash fa-eye');
        if ($(this).hasClass('open')) {
            $(this).prev().attr('type', 'text');
        } else {
            $(this).prev().attr('type', 'password');
        }
    });
});