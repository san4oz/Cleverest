var Account = Account || {};


Account.Team = (function () {
    var myTeamsPage = $("#myTeamsPage");
    if (!myTeamsPage)
        return;
    $(document).on('keyup', '#search', null, function (e) {
        var target = $(e.target);
        $.post(target.attr('data-url'), { query: target.val() }, function (result)
        {
            $("#searchSuggestions").html(result);
        });
    });

    $(document).on('click', ".suggestion", null, function (e) {
        var target = $(e.target);
        $.post(target.attr("data-url"), { id: target.attr("data-id") }, function (result) {
            if (result) {
                $("#searchSuggestions").html(result);
            }
        });
    });

    $(document).on('click', "#sendJoinRequest", null, function (e) {
        debugger;
        e.preventDefault();
        var target = $(e.target);
        $.post(target.attr("data-url"), { teamId: target.attr("data-id") }, function(result){
            if(result)
            {
                $(".suggestion-details").hide();
                $(".suggestion-sent").show();
            }
        });
    });

})();