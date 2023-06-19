//// Left Menu
//!(function (s) {
//    "use strict";


//    s("#side-menu").metisMenu(),
//        s("#vertical-menu-btn").on("click", function (e) {
//            e.preventDefault(), s("body").toggleClass("sidebar-enable"), 992 <= s(window).width() ? s("body").toggleClass("vertical-collpsed") : s("body").removeClass("vertical-collpsed");
//        }),
//        s("#sidebar-menu a").each(function () {
//            var e = window.location.href.split(/[?#]/)[0];
//            this.href == e &&
//                (s(this).addClass("active"),
//                s(this).parent().addClass("mm-active"),
//                s(this).parent().parent().addClass("mm-show"),
//                s(this).parent().parent().prev().addClass("mm-active"),
//                s(this).parent().parent().parent().addClass("mm-active"),
//                s(this).parent().parent().parent().parent().addClass("mm-show"),
//                s(this).parent().parent().parent().parent().parent().addClass("mm-active"));
//        }),
//        s(document).ready(function () {
//            var e;
//            0 < s("#sidebar-menu").length &&
//                0 < s("#sidebar-menu .mm-active .active").length &&
//                300 < (e = s("#sidebar-menu .mm-active .active").offset().top) &&
//                ((e -= 300), s(".vertical-menu .simplebar-content-wrapper").animate({ scrollTop: e }, "slow"));
//        }),
//        s(".navbar-nav a").each(function () {
//            var e = window.location.href.split(/[?#]/)[0];
//            this.href == e &&
//                (s(this).addClass("active"),
//                s(this).parent().addClass("active"),
//                s(this).parent().parent().addClass("active"),
//                s(this).parent().parent().parent().addClass("active"),
//                s(this).parent().parent().parent().parent().addClass("active"),
//                s(this).parent().parent().parent().parent().parent().addClass("active"),
//                s(this).parent().parent().parent().parent().parent().parent().addClass("active"));
//        }),


//        [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]')).map(function (e) {
//            return new bootstrap.Tooltip(e);
//        }),
//        [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]')).map(function (e) {
//            return new bootstrap.Popover(e);
//        }),
//        [].slice.call(document.querySelectorAll(".offcanvas")).map(function (e) {
//            return new bootstrap.Offcanvas(e);
//        })



//})(jQuery);

var path = window.location.pathname;

// Add active class to the corresponding menu item
$('.menu-item').each(function () {
    if ($(this).attr('href') === path) {
        $(this).addClass('mm-active');
    }
});

// Add active class to the corresponding submenu item
$('.submenu-item').each(function () {
    if ($(this).attr('href') === path) {
        $(this).addClass('mm-active');
        $(this).parents('.collapse').addClass('show');
        $(this).parents('.li-item').addClass('mm-active');
    }
});

// Menu fixed
$(window).scroll(function(){
  if ($(window).scrollTop() ) {
	$('body').addClass('header-sticky');
   }
   else {
	$('body').removeClass('header-sticky');
   }
});