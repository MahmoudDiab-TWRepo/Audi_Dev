$(document).ready(function()
   { 

    
        // scroled navbar
        function setHeader(){

            if($(window).scrollTop() > 100)
            {
                nav.addClass('scrolled');
            }
            else
            {
                nav.removeClass('scrolled');
            };
        };

    });
      