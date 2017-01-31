var Popup = {
    open:
        function (selector, options) {
            var defaults = {
                draggable: false,
                resizable: false,
                width: "100%",
                minHeight: "inherit",
                modal: true,
                appendTo: 'body',
            };
            var d = $(selector);
            var settings = $.extend({}, defaults, options);

            d.dialog(settings);
            d.dialog({
                close: function (event, ui) {
                    if ($(".ui-dialog").hasClass("ui-dialog-scroll")) {
                        $("ui-dialog-scroll").removeClass("ui-dialog-scroll");
                        $('html').removeClass('fix-dialog-scroll-helper').css('margin-right', 'initial');
                    };

                    d.dialog("destroy");

                    if (settings.afterClose) {
                        settings.afterClose();
                    };
                }
            });

            Popup.fixScroll(selector);

            d.off('click.dialog');
            d.on('click.dialog', '.btn-close-dialog', function () {
                d.dialog('instance').close();
            });
        },
    close:
        function (selector) {
            var $ins = $(selector).dialog('instance');
            if ($ins) {
                $ins.close();
            }
        },
    center:
        function (selector) {
            var $ins = $(selector).dialog('instance');
            if ($ins) {
                $ins.option("position", { my: "center", at: "center", of: window });
            }
        },
    fixScroll:
        function (selector) {
            if ($(window).height() < $(selector).parents(".ui-dialog").outerHeight()) {
                $(selector).parents(".ui-dialog").addClass("ui-dialog-scroll");
                $('html').addClass('fix-dialog-scroll-helper').css('margin-right', Sana.Utils.getScrollbarSize());
            }
        }
};