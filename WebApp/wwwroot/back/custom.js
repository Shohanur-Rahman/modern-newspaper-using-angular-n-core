$(document).ready(function () {

    $(".image_preview").change(function () {
        readURL(this);
    });

    $(".k_switch").kendoSwitch({
        messages: {
            checked: "YES",
            unchecked: "NO"
        }
    });


    var editor = $(".k_editor").kendoEditor({
        resizable: {
            content: true,
            toolbar: true
        },
        tools: [
            "bold",
            "italic",
            "underline",
            "justifyLeft",
            "justifyCenter",
            "justifyRight",
            "insertUnorderedList",
            "createLink",
            "unlink",
            "insertImage",
            "tableWizard",
            "createTable",
            "addRowAbove",
            "addRowBelow",
            "addColumnLeft",
            "addColumnRight",
            "deleteRow",
            "deleteColumn",
            "mergeCellsHorizontally",
            "mergeCellsVertically",
            "splitCellHorizontally",
            "splitCellVertically",
            "tableAlignLeft",
            "tableAlignCenter",
            "tableAlignRight",
            "formatting",
            {
                name: "fontName",
                items: [
                    { text: "Andale Mono", value: "Andale Mono" },
                    { text: "Arial", value: "Arial" },
                    { text: "Arial Black", value: "Arial Black" },
                    { text: "Book Antiqua", value: "Book Antiqua" },
                    { text: "Comic Sans MS", value: "Comic Sans MS" },
                    { text: "Courier New", value: "Courier New" },
                    { text: "Georgia", value: "Georgia" },
                    { text: "Helvetica", value: "Helvetica" },
                    { text: "Impact", value: "Impact" },
                    { text: "Symbol", value: "Symbol" },
                    { text: "Tahoma", value: "Tahoma" },
                    { text: "Terminal", value: "Terminal" },
                    { text: "Times New Roman", value: "Times New Roman" },
                    { text: "Trebuchet MS", value: "Trebuchet MS" },
                    { text: "Verdana", value: "Verdana" },
                ]
            },
            "fontSize",
            "foreColor",
            "backColor",
        ]
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
