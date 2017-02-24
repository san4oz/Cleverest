var Admin = Admin || {};


Admin.Question = (function () {
    var questionListPage = $("#questionListPage");
    if (!questionListPage)
        return;

    $(document).on("submit", "#questionSelectorForm", null, function (e) {
        debugger;
        e.preventDefault();

        var $form = $(e.target);

        var selectedGameId = $("#GameIdSelector").val();
        var selectedRoundNo = $("#RoundNoSelector").val();
        
        $.post($form.attr("action"), { gameId: selectedGameId, roundNo: selectedRoundNo }, function (result) {
            debugger;
            if(result)
                $(".question-editor").html(result);
        });
    });

})();