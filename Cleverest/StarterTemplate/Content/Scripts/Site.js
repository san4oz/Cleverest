var Site = Site || {};

Site.UI = (function () {

    var ui = {};

    ui.Init = function () {
        ui.Datepickers.init();
    };

    ui.Datepickers = {
        init: function () {
            debugger;
            $(".datepicker").each(function () {
                var self = $(this);

                self.datepicker({ showOtherMonths: true,
                    selectOtherMonths: true,
                    showButtonPanel: true});
            });
        }
    };

    return ui;
})();

$(function () {
    Site.UI.Init();
});