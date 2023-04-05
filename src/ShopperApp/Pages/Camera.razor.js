
export async function init(videoElementRef, dotnetObjectRef) {
    
    console.log("init");

    try {
        var stream = await navigator.mediaDevices.getUserMedia({ video: true });
        onSuccess(stream, videoElementRef);
        dotnetObjectRef.invokeMethodAsync("OnSuccess");
    }
    catch (e) {
        onFailure(e, dotnetObjectRef)

    }
}

function onSuccess(stream, videoElementRef) {

    videoElementRef.srcObject = stream;
    videoElementRef.play();
}

function onFailure(exception, dotnetObjectRef) {
    console.log("Exception occurred", exception);
    dotnetObjectRef.invokeMethodAsync("onFailure", exception.message);
}

export function capturePhoto(videoElement, canvasElement) {
    console.log("capturePhoto");

    const context = canvasElement.getContext('2d');
    context.drawImage(videoElement, 0, 0, canvasElement.width, canvasElement.height);
}

export function canvasToDataURL(canvasRef) {
    return canvasRef.toDataURL('image/png');
}