var Site = Site || {};

Site.UI = (function () {

    var ui = {};

    ui.Init = function () {
        ui.Datepickers.init();
        ui.Carousel.init();
    };

    ui.Datepickers = {
        init: function () {
            $(".datepicker").each(function () {
                var self = $(this);

                self.datepicker({
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    showButtonPanel: true,
                    defaultDate: new Date()
                });
            });
        }
    };

    ui.Carousel = {
        init: function () {
            $(".carousel").scCarousel({ visibleCount: 1 });
        }
    };

    ui.FileUpload = {
        init: function () {
            $("#fileUpload").fileUpload();
        }
    };

    return ui;
})();

$(function () {
    Site.UI.Init();
});