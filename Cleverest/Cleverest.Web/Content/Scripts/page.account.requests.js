var Account = Account || {};


Account.Request = (function () {
    var myRequestsPage = $("#myRequestsPage");
    if (!myRequestsPage)
        return;
    
    $(document).on('click', ".request-action > button", null, function (e) {
        debugger;
        var target = $(e.target).closest("button");
        var $requestElement = target.closest(".request");

        var requestId = $requestElement.attr("data-id");
        var url = $requestElement.attr("data-url");

        var isApproved = target.hasClass("approve");
        
        $.post(url, { requestId: requestId, approved: isApproved }, function (result) {
            if(result)
                $requestElement.remove();
        });
    });

})();