window.openPdfInNewTab = function (base64Content) {
    const binaryData = atob(base64Content);
    const uint8Array = new Uint8Array(binaryData.length);
    for (let i = 0; i < binaryData.length; i++) {
        uint8Array[i] = binaryData.charCodeAt(i);
    }

    const blob = new Blob([uint8Array], { type: 'application/pdf' });
    const url = URL.createObjectURL(blob);

    const newTab = window.open(url, '_blank');
    newTab.focus();
};