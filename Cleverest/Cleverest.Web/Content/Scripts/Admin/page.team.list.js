var Team = Team || {};


Game.List = (function () {
    var teamListPage = $("#TeamListPage");
    if (!teamListPage)
        return;

    var itemToDelete, deletionUrl;

    $(".delete-btn").on("click", function (e) {
        e.preventDefault();

        var link = $(e.target);

        deletionUrl = link.attr("data-url");
        itemToDelete = link.attr("data-id");

        Popup.open($("#delete-team"));
    });

    $("#delete-team .btn-yes").on('click', function (e) {
        e.preventDefault();

        $.post(deletionUrl, { id: itemToDelete }, function (result) {
            if (result)
                window.location.reload();
        });
    });
})();