//preview Image Before Uploading
function readImageData(imgData, preview_img) {
    if (imgData.files && imgData.files[0]) {
        var readerObj = new FileReader();

        readerObj.onload = function (element) {
            $(preview_img).attr('src', element.target.result);
            $(preview_img).show();
        }

        readerObj.readAsDataURL(imgData.files[0]);
    }
}