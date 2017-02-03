var Gallery = Gallery || {};


Gallery.List = (function () {
	$(function () {

		var setFileUploadGameId = function () {
			$("#fileupload").fileupload({ formData: { gameId: $("#gameSelector").val() } });
		};

		var $gameGalleryPage = $("#gameGalleryPage");
		if (!$gameGalleryPage)
			return;
	
		$('#fileupload').fileupload
			({
				url: "upload",
				downloadTemplateId: null
			});

		setFileUploadGameId();

		$gameGalleryPage.find("#gameSelector").on('change', function () {

			setFileUploadGameId();

			$.post("", { gameId: $(this).val() }, function (result) {
				$(".game-photos").html(result);
			});
		});

		$(".selectable").on("click", function () {
			$(this).toggleClass("photo-selected");

			if($(".photo-selected").length > 0)
			{
				$("#deletePhotosBtn").removeAttr("disabled");
			}
			else
			{
				$("#deletePhotosBtn").attr("disabled", "disabled");
			}
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
		
	});
})();