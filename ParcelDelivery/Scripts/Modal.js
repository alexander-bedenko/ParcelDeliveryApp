$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $(".viewDialog").on("click", function (e) {
        e.preventDefault();

        $("<div></div>")
            .addClass("dialog")
            .appendTo("body")
            .dialog({
                title: $(this).attr("data-dialog-title"),
                show: {
                    effect: "blind",
                    duration: 500
                },
                hide: {
                    effect: "explode",
                    duration: 500
                },
                width: 'auto',
                dialogClass: 'dialogWithDropShadow',
                close: function () { $(this).remove() },
                modal: true,
                position: { my: "center", at: "top" }
            })
            .load(this.href);
    });
});
