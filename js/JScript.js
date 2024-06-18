
$(document).ready(function () {
    //alert("in");
    BindButtonClickEvent();
    var unsaved = false;

    $(":input").change(function () { 
        unsaved = true;
    });

    function unloadPage() {
        if (unsaved) {
            return "You have unsaved changes on this page. Do you want to leave this page without saving ?";
        }
    }


});

function BindButtonClickEvent() {
    $("[id$=btnSubmit]").click(function () {
    //alert("clicked");
    unsaved = false;
});
}    

$(window).bind('beforeunload', function () {
    if (unsaved) {
        return "You have unsaved changes on this page. Do you want to leave this page without saving ?";
    }
});

$(document).on('change', ':input', function () {
    //alert("changd");
    unsaved = true;
});