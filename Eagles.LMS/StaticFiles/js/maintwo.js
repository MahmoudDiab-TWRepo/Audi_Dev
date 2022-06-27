// Hello.
//
// This is The Scripts used for ___________ Theme
//
//

function main() {

    (function () {
       'use strict';
    
       /* ==============================================
          Testimonial Slider
          =============================================== */ 
    
          $('a.page-scroll').click(function() {
                $('html,body').animate({
                  scrollTop: target.offset().top - 40
                }, 900);
       
          });
          

          /************ */

          $(".filter-button").click(function(){
            var value = $(this).attr('data-filter');
            
            if(value == "all")
            {
                //$('.filter').removeClass('hidden');
                $('.filter').show('1000');
            }
            else
            {
    //            $('.filter[filter-item="'+value+'"]').removeClass('hidden');
    //            $(".filter").not('.filter[filter-item="'+value+'"]').addClass('hidden');
                $(".filter").not('.'+value).hide('3000');
                $('.filter').filter('.'+value).show('3000');
                
            }
        });
        
        if ($(".filter-button").removeClass("active")) {
    $(this).removeClass("active");
    }
    $(this).addClass("active");
    
    }());
    
    
    }
    main();


