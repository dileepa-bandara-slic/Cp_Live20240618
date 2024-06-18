wow = new WOW({
    animateClass: 'animated',
    offset: 100,
    callback: function (box) {
        console.log("WOW: animating <" + box.tagName.toLowerCase() + ">")
    }
});
wow.init();
// initParalaxBg();
if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
    $(".footer-phone").css('pointer-events', 'auto');
    $(".inner-section-page").removeClass('wow');
    $(".landing-bigfont").removeClass('wow');
} else {
    $(".footer-phone").css('pointer-events', 'none');
}

$(document).ready(function () {
    try {
        $(function () {
            $(".benefits-block").slice(0, 9).show();
            $(".loadmore-benefits").on('click', function (e) {
                e.preventDefault();
                $(".benefits-block:hidden").slice(0, 9).slideDown();
                if ($(".benefits-block:hidden").length == 0) {
                    $("#loadmore-benefits").fadeOut('slow');
                }
                $('html,body').animate({
                    scrollTop: $(this).offset().top - ($(".benefits-block").height() * 3)
                }, 1500);
            });
        });
    } catch (e) { }
});



$(window).on("scroll", function () {
    if ($(window).scrollTop() > 170) {
        $(".second-header").addClass("second-header-active");
    } else {
        $(".second-header").removeClass("second-header-active");
    }
});

// $(window).on("scroll", function() {
//   var $el = $('.tab-detail-wrapper'); 
//   var bottom = $el.position().top + $el.outerHeight(true);
//   if($(window).scrollTop() > bottom){
//     $(".apply-now").addClass("fixed-apply");
//   }else{
//     $(".apply-now").removeClass("fixed-apply");
//   }
// });
$('html').mousemove(function (e) {

    var wx = $(window).width();
    var wy = $(window).height();

    var x = e.pageX - this.offsetLeft;
    var y = e.pageY - this.offsetTop;

    var newx = x - wx / 2;
    var newy = y - wy / 2;


    $('.landing-page').each(function () {
        var speed = $(this).attr('data-speed');
        if ($(this).attr('data-revert')) speed *= -1;
        TweenMax.to($(this), 1, { x: (1 - newx * speed), y: (1 - newy * speed) });

    });

});

$(window).on('load', function () {

    var quote_content = $(".quote-box-inner").html();
    // alert(quote_content);
    if (quote_content == "") {
        $(".full-width-div").addClass("expand-div");
        $(".quote-box").css('display', 'none');
    } else {
        $(".full-width-div").removeClass("expand-div");
        $(".quote-box").css('display', 'block');
    }
});
jQuery(document).on('click', '.mega-dropdown', function (e) {
    e.stopPropagation()
})


$(document).ready(function () {

});
/*Multilevel bootstrap v4.0.0 navigation menu*/
(function ($) {
    $('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
        if (!$(this).next().hasClass('show')) {
            $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
        }
        var $subMenu = $(this).next(".dropdown-menu");
        $subMenu.toggleClass('show');

        $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
            $('.dropdown-submenu .show').removeClass("show");
        });

        return false;
    });
})(jQuery)
/*Multilevel bootstrap v4.0.0 navigation menu*/
$(".home-slider").slick({
    infinite: true,
    dots: false,
    slidesToShow: 1,
    autoplay: true,
    arrows: true,
    speed: 1000,
    fade: true,
    pauseOnFocus: false,
    pauseOnHover: false,
    variableWidth: false,
    autoplaySpeed: 6000,
    adaptiveHeight: false,
    appendArrows: $(".slider_nav_001"),
    appendDots: $(".slider_nav_001"),
    nextArrow: '<div class="slick-next"></div>',
    prevArrow: '<div class="slick-prev"></div>',
    responsive: [{

        breakpoint: 1200,
        settings: {
            slidesToShow: 1
        }


    }, {

        breakpoint: 0,
        settings: {
            slidesToShow: 1
        }

    }]
});

$(".home-banner-2").slick({
    infinite: true,
    dots: false,
    slidesToShow: 1,
    autoplay: true,
    arrows: true,
    speed: 1000,
    fade: true,
    pauseOnFocus: false,
    pauseOnHover: false,
    variableWidth: false,
    autoplaySpeed: 6000,
    adaptiveHeight: false,
    appendArrows: $(".slider_nav_001"),
    appendDots: $(".slider_nav_001"),
    nextArrow: '<div class="slick-next"></div>',
    prevArrow: '<div class="slick-prev"></div>',
    responsive: [{

        breakpoint: 1200,
        settings: {
            slidesToShow: 1
        }


    }, {

        breakpoint: 0,
        settings: {
            slidesToShow: 1
        }

    }]
});


$(".top-home-slider").slick({
    infinite: true,
    dots: false,
    slidesToShow: 1,
    autoplay: true,
    arrows: true,
    speed: 1000,
    fade: true,
    pauseOnFocus: false,
    pauseOnHover: false,
    variableWidth: false,
    autoplaySpeed: 6000,
    adaptiveHeight: false,
    appendArrows: $(".slider_nav_001"),
    appendDots: $(".slider_nav_001"),
    nextArrow: '<div class="slick-next"></div>',
    prevArrow: '<div class="slick-prev"></div>',
    responsive: [{

        breakpoint: 1200,
        settings: {
            slidesToShow: 1
        }


    }, {

        breakpoint: 0,
        settings: {
            slidesToShow: 1
        }

    }]
});


$("#education-slider").slick({
    infinite: true,
    dots: false,
    slidesToShow: 2,
    autoplay: true,
    arrows: true,
    speed: 1000,
    fade: false,
    pauseOnFocus: false,
    pauseOnHover: false,
    variableWidth: false,
    autoplaySpeed: 6000,
    adaptiveHeight: false,
    appendArrows: $(".slider-controler1"),
    nextArrow: '<div class="slick-next"></div>',
    prevArrow: '<div class="slick-prev"></div>',
    responsive: [{

        breakpoint: 1200,
        settings: {
            slidesToShow: 1
        }


    }, {

        breakpoint: 0,
        settings: {
            slidesToShow: 1
        }

    }]
});
$("#community-slider").slick({
    infinite: true,
    dots: false,
    slidesToShow: 2,
    autoplay: true,
    arrows: true,
    speed: 1000,
    fade: false,
    pauseOnFocus: false,
    pauseOnHover: false,
    variableWidth: false,
    autoplaySpeed: 6000,
    adaptiveHeight: false,
    appendArrows: $(".slider-controler2"),
    nextArrow: '<div class="slick-next"></div>',
    prevArrow: '<div class="slick-prev"></div>',
    responsive: [{

        breakpoint: 1200,
        settings: {
            slidesToShow: 1
        }


    }, {

        breakpoint: 0,
        settings: {
            slidesToShow: 1
        }

    }]
});
$("#culture-slider").slick({
    infinite: true,
    dots: false,
    slidesToShow: 3,
    autoplay: true,
    arrows: true,
    speed: 1000,
    fade: false,
    pauseOnFocus: false,
    pauseOnHover: false,
    variableWidth: false,
    autoplaySpeed: 6000,
    adaptiveHeight: false,
    appendArrows: $(".slider-controler3"),
    nextArrow: '<div class="slick-next"></div>',
    prevArrow: '<div class="slick-prev"></div>',
    responsive: [{

        breakpoint: 1200,
        settings: {
            slidesToShow: 1
        }


    }, {

        breakpoint: 0,
        settings: {
            slidesToShow: 1
        }

    }]
});

$(".news-slider").slick({
    infinite: true,
    dots: false,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: false,
    arrows: true,
    speed: 1000,
    fade: false,
    variableWidth: false,
    autoplaySpeed: 3000,
    adaptiveHeight: true,
    appendArrows: $(".slider-controler"),
    // appendDots: $(".slider_nav_001"),
    nextArrow: '<div class="slick-next"></div>',
    prevArrow: '<div class="slick-prev"></div>',
    // responsive: [{

    //     breakpoint: 1200,
    //     settings: {
    //       slidesToShow: 1,
    //       slidesToScroll: 1,
    //     }


    //   },{

    //     breakpoint: 992,
    //     settings: {
    //       slidesToShow: 2,
    //       slidesToScroll: 2,
    //     }


    //   },{

    //     breakpoint: 768,
    //     settings: {
    //       slidesToShow: 2,
    //       slidesToScroll: 2,

    //     }


    //   },{

    //     breakpoint: 480,
    //     settings: {
    //       slidesToShow: 1,
    //       slidesToScroll: 1,

    //     }


    //   }, {

    //       breakpoint: 0,
    //       settings: {
    //           slidesToShow: 1
    //       }

    //   }]
});


$(document).ready(function () {
    $(function () {
        cbpHorizontalMenu.init();
    });
});
$(".submenu_mobile").on('click', function () {
    $(".submenu_mobile").toggleClass("submenu_mobile_button_active");
    $(".cbp-hrmenu").toggleClass("submenu_active");
});
try {
    $('.marquee').marquee({
        //speed in milliseconds of the marquee
        duration: 15000,
        //gap in pixels between the tickers
        gap: 50,
        //time in milliseconds before the marquee will start animating
        delayBeforeStart: 0,
        //'left' or 'right'
        direction: 'left',
        //true or false - should the marquee be duplicated to show an effect of continues flow
        duplicated: true,
        pauseOnHover: true
    });

} catch (e) { }

$(".our-prod-box-inner").mouseenter(function () {
    var imagepath = $(this).find('img').attr('src');
    $(".prod-image-overview").find('img').attr('src', imagepath);
});

function getValue(selectObject) {
    var value = selectObject.value;
    $(".result-block").fadeOut(500);
    $("." + value).fadeIn(500);
    // console.log(value);
}

// jQuery(function($) {

// })(jQuery);

$('a[href*="#"]:not([href="#"])').click(function () {
    var target = $(this.hash);
    $('html,body').stop().animate({
        scrollTop: target.offset().top - 120
    }, 'linear');
});
if (location.hash) {
    var id = $(location.hash);
}
$(window).load(function () {
    if (location.hash) {
        $('html,body').animate({ scrollTop: id.offset().top - 120 }, 'linear')
    };
});

