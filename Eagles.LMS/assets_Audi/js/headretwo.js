(function($){
	"use strict";
	jQuery(document).on('ready', function () {
		
        
        // hidesearch section Sticky
		$(window).on('scroll',function() {
            if ($(this).scrollTop() > 550){  
                $('.otherheader').addClass("fixedhadertwo animated slideInDown");
            }
            else{
                $('.otherheader').removeClass("fixedhadertwo animated slideInDown");
            }
        });



        $(window).on('scroll', function () {
            if ($(this).scrollTop() > 150) {
                $('.boxSall').addClass("fixedtwobox animated slideInDown");
            }
            else {
                $('.boxSall').removeClass("fixedtwobox animated slideInDown");
            }
        });


        
        // scroll nice
          $('a.page-scroll').click(function() {
            if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
              var target = $(this.hash);
              target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
              if (target.length) {
                $('html,body').animate({
                  scrollTop: target.offset().top - 40
                }, 900);
                return false;
              }
            }
          });
        
	});
	



}(jQuery));