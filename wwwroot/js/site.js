var $toast = {}
$toast.success = (body) => {
    ToastInit({
        className: 'bg-success',
        body: body ?? 'Successful implementation. This alert is dismissable.'});
} 


$toast.fail = (body) => {
    ToastInit({
        className: 'bg-danger',
        body: body ?? 'Failed implementation. This alert is dismissable.'});
}


$toast.info = (body) => {
    ToastInit({
        className: 'bg-info',
        body: body ?? 'Information. This alert is dismissable.'});
}


$toast.warning = (body) => {
    ToastInit({
        className: 'bg-warning',
        body: body ?? 'Warning. This alert is dismissable.'});
}

$toast.maroon = (body) => {
    ToastInit({
        className: 'bg-maroon',
        body: body ?? 'Maroon. This alert is dismissable.'});
}

function ToastInit(options) {
    let { 
        className, 
        body,
        title = 'Alert!',
        delay = 2500, 
        autohide = true,
    } = options;
    $(document).Toasts('create', {
        class: className,
        title,
        body,
        delay,
        autohide,
    });
}
