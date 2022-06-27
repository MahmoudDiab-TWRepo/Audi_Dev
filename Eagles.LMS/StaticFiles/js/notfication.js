
pushNotification = (type, message) => {
    //$.notifyClose();
    $("div[data-notify=container]").remove();
    $.notify(message, {
        type: type,
        placement: {
            from: "top",
            align: "left"
        },
        offset: 20,
        spacing: 10,
        z_index: 1031,
        delay: 5000,
        timer: 1000,
        url_target: '_blank',
        mouse_over: null,
        animate: {
            enter: 'animated rollIn',
            exit: 'animated rollOut'
        }
    });
};
