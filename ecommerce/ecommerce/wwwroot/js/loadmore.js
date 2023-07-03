let skipCount=3
$(document).on("click", "#loadMore", function () {
    $.ajax({
        url: "/Shops/LoadMore/",
        type: "GET",
        data: {
            "skip":skipCount
        },
        success: function (response) {
            $("#myProducts").append(response)
            skipCount += 3;
            if ($("#productCount").val() <= skipCount) {
                $("#loadMore").remove()
            }
        }
    });
});