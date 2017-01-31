var Game = Game || {};


Game.List = (function () {

    var gameListPage = $("#GameListPage");
    if (!gameListPage)
        return;

    $(".delete-btn").on("click", function (e) {
        e.preventDefault();

        var $target = $(e.target);

        $.post($target.attr("data-url"), { id: $target.attr("data-id") }, function (result) {
            if (result)
                alert("Successfully deleted")
        });
    });
})();