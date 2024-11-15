$(document).ready(function () {
    // Khởi tạo slider sản phẩm
    $('.product-slider').slick({
        dots: true,
        infinite: true,
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 3,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1
                }
            }
        ]
    });

    // Hiện thanh tìm kiếm
    $('#searchToggle').click(function () {
        $('#searchBar').toggle();
    });

    //// Cập nhật số sản phẩm trong giỏ hàng
    //let cartCount = 0; // Thay đổi theo logic giỏ hàng
    //$('#cartCount').text(cartCount);

    // Sticky header
    $(window).scroll(function () {
        $('#header').toggleClass('sticky', $(this).scrollTop() > 50);
    });

    // Khởi tạo carousel
    $('.carousel').carousel({
        interval: 2000
    });

    // Hiện/ẩn danh mục con
    $('.parent-category').click(function () {
        $(this).next('.nested').slideToggle();
    });

    // Hiệu ứng khi sản phẩm xuất hiện
    const products = document.querySelectorAll('.product-item');
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
            }
        });
    });
    products.forEach(product => observer.observe(product));

    // Chức năng toggle submenu
    window.toggleSubmenu = function (element) {
        const submenu = element.querySelector('.submenu');
        const symbol = element.querySelector('.toggle-symbol');
        const isVisible = submenu.style.display === 'block';
        submenu.style.display = isVisible ? 'none' : 'block';
        symbol.textContent = isVisible ? '+' : '−';
    };

    // Thay đổi số lượng sản phẩm
    window.changeQuantity = function (productId, delta) {
        const input = document.getElementById(productId);
        let currentValue = Math.max(1, parseInt(input.value) + delta); // Đảm bảo số lượng không nhỏ hơn 1
        input.value = currentValue;
    };

    // Hiện/ẩn mật khẩu
    $('#eye').click(function () {
        $(this).toggleClass('open');
        $(this).children('i').toggleClass('fa-eye-slash fa-eye');
        const inputType = $(this).hasClass('open') ? 'text' : 'password';
        $(this).prev().attr('type', inputType);
    });

   
});