$(document).ready(function () {

    $(".image_preview").change(function () {
        readURL(this);
    });

});

function readURL(input) {
    if (input.files && input.files[0]) {
        debugger;
        var reader = new FileReader();
        var previewID = $(input).attr("preview");

        reader.onload = function (e) {
            if (previewID) {
                $(previewID).attr('src', e.target.result);
            }
        }

        reader.readAsDataURL(input.files[0]);
    }
}
