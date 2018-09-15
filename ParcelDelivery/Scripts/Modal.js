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
                    duration: 1000
                },
                hide: {
                    effect: "explode",
                    duration: 1000
                },
                minHeight: 450,
                height: "auto",
                width: 450,
                dialogClass: 'dialogWithDropShadow',
                close: function () {$(this).remove()},
                modal: true,
                my: "center",
                at: "center",
                of: window
            })
            .load(this.href);
    });
});
