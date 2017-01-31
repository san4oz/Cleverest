(function ($) {
    var options;

    var methods = {
        init: function (params) {
            if (!$('.carousel-content').length) {
                return;
            };

            var $this = $(this),
                slider = $this.find('.carousel-image-slider'),
                thumbList = slider.find('ul'),
                count = thumbList.children().length,
                thumbWidth = thumbList.children().eq(0).width(),
                current,
                previewCnt = $this.find('.carousel-image'),
                previewList = previewCnt.find('ul'),
                previewItemWidth = previewList.children().eq(0).outerWidth(true),
                nextBtn = slider.find('.btn-next-img'),
                prevBtn = slider.find('.btn-prev-img'),
                data = $this.data('scCarousel');

            var defaults = { count: count, thumbWidth: thumbWidth, previewItemWidth: previewItemWidth };
            options = $.extend({}, defaults, params);

            var selectThumbnail = function () {
                var shift = Math.min(Math.max(current - 1, 0), Math.max(options.count - options.visibleCount, 0)) * options.thumbWidth;
                thumbList.children().removeClass('selected').eq(current).addClass('selected');
                thumbList.parent().animate({ left: -shift + 'px' });
            };

            var getCurrentMediumImage = function () {
                return previewList.children().eq(current).find('img');
            };

            var selectPreview = function () {
                var shift = Math.min(Math.max(current, 0), options.count - 1) * options.previewItemWidth;
                previewList.children().removeClass('selected').eq(current).addClass('selected');

                //Zoom.disableAll(previewList.children());
                previewList.animate({ left: -shift + 'px' }, 500, function () {
                    //Zoom.init(getCurrentMediumImage());
                });
            };

            var updNavBtns = function () {
                prevBtn.toggleClass('disabled', !(current > 0));
                nextBtn.toggleClass('disabled', !(current < options.count - 1));
            };

            var select = function () {
                if ($(previewList).is(':animated')) $(previewList).stop(true);
                $this.scCarousel('setCurrentId', current);

                selectThumbnail();
                selectPreview();
                updNavBtns();
            };

            var bindEvents = function () {
                debugger;
                if (data) {
                    return;
                };

                nextBtn.click(function () {
                    if (current < options.count - 1) {
                        current++;
                        select();
                    }
                });
                prevBtn.click(function () {
                    if (current > 0) {
                        current--;
                        select();
                    }
                });
                thumbList.find('a').each(function (i) {
                    $(this).click(function () {
                        if (current != i) {
                            current = i;
                            select();
                        };
                        return false;
                    });
                });
            };

            var setWidth = function () {
                $('.carousel-centering').width(Math.min(options.count, options.visibleCount) * options.thumbWidth);
            };


            current = data ? data.current : 0;

            if (count <= 1) {
                setTimeout(function () {
                    //Zoom.init(getCurrentMediumImage());
                },250);
                $this.scCarousel('setCurrentId', 0);
            } else {
                select();
                setWidth();
                bindEvents();
            };

            return this;
        },
        goTo: function (index) {
            $('.carousel-image-slider').find('.hyp-thumbnail').eq(index).click();
        },
        setCurrentId: function (id) {
            $(this).data('scCarousel', { current: id });
        },
        getCurrentId: function () {
            var $this = $(this).data('scCarousel');
            return $this.current;
        }
    };

    $.fn.scCarousel = function (method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        }
    };

})(jQuery);