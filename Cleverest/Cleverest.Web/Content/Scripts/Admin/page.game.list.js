var Game = Game || {};


Game.List = (function () {
    var gameListPage = $("#GameListPage");
    if (!gameListPage)
        return;

    var itemToDelete, deletionUrl;

    $(".delete-btn").on("click", function (e) {
        e.preventDefault();

        var link = $(e.target);

        deletionUrl = link.attr("data-url");
        itemToDelete = link.attr("data-id");

        Popup.open($("#delete-game"));       
    });

    $("#delete-game .btn-yes").on('click', function (e) {
        e.preventDefault();

        $.post(deletionUrl, { id: itemToDelete }, function (result) {
            if (result)
                window.location.reload();
        });
    });
})();