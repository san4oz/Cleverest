var Gallery = Gallery || {};


Gallery.List = (function () {
    $(function () {

        var $gameGalleryPage = $("#gameGalleryPage");
        if (!$gameGalleryPage)
            return;

        var $gameSelector = $("#gameSelector");
        var $photosSection = $(".game-photos");
        var $fileUpload = $("#fileupload");

        var enablePhotoSelection = function ()
        {
            $(".selectable").unbind("click");

            $(".selectable").on("click", function () {
                $(this).toggleClass("photo-selected");

                if ($(".photo-selected").length > 0) {
                    $("#deletePhotosBtn").removeAttr("disabled");
                }
                else {
                    $("#deletePhotosBtn").attr("disabled", "disabled");
                }
            });
        };

        var reloadPhotoSection = function () {
            $.post("", { gameId: $gameSelector.val() }, function (result) {
                $photosSection.html(result);
                enablePhotoSelection();
            });
        };

        var configureFileUpload = function () {

            $fileUpload.fileupload
             ({
                 url: "upload",
                 downloadTemplateId: null
             });

            $fileUpload.bind("fileuploadsubmit", function (e, data) {
                reloadPhotoSection();
            });
        };

        var setFileUploadGameId = function () {
            $fileUpload.fileupload({ formData: { gameId: $gameSelector.val() } });
        };

        var bindUIEvents = function () {
            enablePhotoSelection();
            configureFileUpload();
            setFileUploadGameId();

            $gameSelector.on('change', function () {
                setFileUploadGameId();
                reloadPhotoSection();
            });

            $("#deletePhotosBtn").on("click", function (e) {
                e.preventDefault();
                Popup.open($("#delete-photos-popup"));

            });

            $("#delete-photos-popup .btn-yes").on("click", function (e) {
                e.preventDefault();

                var url = $("#deletePhotosBtn").attr("data-url");

                var selectedPhotos = $(".photo-selected");

                var urlsToDelete = [];

                $.each(selectedPhotos, function (index, value) {
                    var url = $(value).find("img").attr("src");

                    urlsToDelete.push(url);

                });

                var selectedPhotosToDelete = selectedPhotos.find("img").attr("src");

                $.post(url, { files: urlsToDelete }, function (result) {
                    if (!result)
                        return;

                    selectedPhotos.remove();
                    $("#deletePhotosBtn").attr("disabled", "disabled");
                    Popup.close($("#delete-photos-popup"));
                });
            });

        };

        bindUIEvents();
	});
})();