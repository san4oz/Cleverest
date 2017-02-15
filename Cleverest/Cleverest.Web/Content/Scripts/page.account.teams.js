var Account = Account || {};


Account.Team = (function () {
    var myTeamsPage = $("#myTeamsPage");
    if (!myTeamsPage)
        return;
    $(document).on('keyup', '#search', null, function (e) {
        debugger;
        var target = $(e.target);
        $.post(target.attr('data-url'), { query: target.val() }, function (result)
        {
            $("#searchSuggestions").html(result);
        });
    });
})();