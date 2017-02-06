var Team = Team || {};


Game.List = (function () {
    var accountListPage = $("#AccountListPage");
    if (!accountListPage)
        return;

    var itemToDelete, deletionUrl;

    $(".delete-btn").on("click", function (e) {
        e.preventDefault();

        var link = $(e.target);

        deletionUrl = link.attr("data-url");
        itemToDelete = link.attr("data-id");

        Popup.open($("#delete-account"));
    });

    $("#delete-account .btn-yes").on('click', function (e) {
        e.preventDefault();

        $.post(deletionUrl, { id: itemToDelete }, function (result) {
            if (result)
                window.location.reload();
        });
    });
})();