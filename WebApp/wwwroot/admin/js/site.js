/////// tinymce and roxyfileman ///////

tinymce.init({
    relative_urls: false, paste_data_images: true,
    selector: ".tinymce-editor",
    language_url: '/lib/tinymce/langs/fa_IR.js',
    theme: 'modern',
    directionality: 'rtl',
    height: 250,
    setup: function (ed) {
        ed.on('init', function () {
            this.getDoc().body.style.fontSize = '16px';
            this.getDoc().body.style.fontFamily = 'tahoma';
        });
    },
    file_browser_callback: RoxyFileBrowser,
    plugins: [
        "directionality advlist autolink lists link image charmap print preview hr anchor pagebreak",
        "searchreplace wordcount visualblocks visualchars code fullscreen",
        "insertdatetime media nonbreaking save table contextmenu directionality",
        "emoticons template paste textcolor colorpicker textpattern imagetools"
    ],
    toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | rtl ltr | bullist numlist outdent indent | link image",
    toolbar2: "print preview | forecolor backcolor emoticons",
    image_advtab: true,
    templates: [
        { title: 'Test template 1', content: 'Test 1' },
        { title: 'Test template 2', content: 'Test 2' }
    ]
});

$('.sidebar-collapse li a').each(function () {
    if (location.pathname.indexOf($(this).attr('href')) !== -1) {
        $(this).parent("li").addClass('active');
        $(this).parent("li").parent("ul").parent("li").addClass('active');
        $(this).parent("li").parent("ul").parent("li").parent("ul").parent("li").addClass('active');
    }
});

function RoxyFileBrowser(field_name, url, type, win) {
    var roxyFileman = '/Assets/lib/tinymce/fileman/index.html';
    if (roxyFileman.indexOf("?") < 0) {
        roxyFileman += "?type=" + type;
    }
    else {
        roxyFileman += "&type=" + type;
    }
    roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
    if (tinyMCE.activeEditor.settings.language) {
        roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
    }
    tinyMCE.activeEditor.windowManager.open({
        file: roxyFileman,
        title: 'Roxy Fileman',
        width: 850,
        height: 650,
        resizable: "yes",
        plugins: "media",
        inline: "yes",
        close_previous: "no"
    }, { window: win, input: field_name });
    return false;
}

/////// active navigation ///////

$('.nav-collapse li a').each(function () {
    if (location.pathname.indexOf($(this).attr('href')) !== -1) {
        $(this).parent("li").addClass('active');
        $(this).parent("li").parent("ul").parent("li").addClass('active');
        $(this).parent("li").parent("ul").parent("li").parent("ul").parent("li").addClass('active');
    }
});

////// ajax loading icon //////

$.loading({
    //wrap div
    //set the loading html wrap tag id
    id: 'ajaxLoading',
    //wrap tag z-index
    zIndex: '1000',
    //wrap tag background
    background: 'rgba(0, 0, 0, 0.7)',
    //min show time
    minTime: 20000,
    //wrap tag border-radius
    radius: '4px',
    //wrap width
    width: '115px',
    height: '90px',

    //loading img/gif
    imgPath: '/images/loading.gif',
    imgWidth: '45px',
    imgHeight: '45px',

    //loading text
    tip: 'لطفا منتظر بمانید',
    //text font size
    fontSize: '14px',
    //text font color
    fontColor: '#fff'
});
var loading = $.loading();

//enable and disable listening ajax events
//loading.ajax(true);//enable
loading.ajax(false);//disable

//manual show the loading view
//loading.open();//not close
//loading.open(1000);//auto close view after 1 seconds

//manual close the loading view
//loading.close();

////// GetJson //////
function GetJsonData(url, fromControl, appendTo, type) {
    if (type == "name") {
        fromControl = "[name='" + fromControl + "']";
        appendTo = "[name='" + appendTo + "']";
    }
    else {
        fromControl = "#" + fromControl;
        appendTo = "#" + appendTo;
    }
    $(appendTo).empty();
    if ($(fromControl + " option:selected").val() != "")
        loading.open();
    $.getJSON(url, { id: $(fromControl + " option:selected").val() }, function (data) {
        $(appendTo).empty();
        $(appendTo).append('<option value>انتخاب کنید</option>')
        jQuery.each(data, function (i) {
            var option = $('<option></option>').attr("value", data[i].id).text(data[i].title);
            $(appendTo).append(option);
        }); loading.close(); $(appendTo).focus();
    });
}